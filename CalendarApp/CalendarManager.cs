using System;
using System.Collections.Generic;
using System.Linq;
using CalendarApp.Models;
using CalendarApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Services
{
    public class CalendarManager : IDisposable
    {
        private readonly CalendarDbContext _db;
        private User _currentUser;

        public CalendarDbContext Context => _db;

        public CalendarManager(string currentUsername)
        {
            _db = new CalendarDbContext();
            _currentUser = _db.Users.FirstOrDefault(u => u.Username == currentUsername);
            if (_currentUser == null)
            {
                throw new InvalidOperationException($"User '{currentUsername}' not found.");
            }
        }

        public User GetCurrentUser() => _currentUser;

        public (AppointmentActionResult Status, string Message, Appointment RelatedAppointment) AddAppointment(
    string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime, bool forceIndividual = false)
        {
            // --- Bước 1: Validation cơ bản ---
            if (string.IsNullOrWhiteSpace(name)) return (AppointmentActionResult.ValidationError, "Name is required.", null);
            if (endTime <= startTime) return (AppointmentActionResult.ValidationError, "End time must be after start time.", null);
            if (reminderTime.HasValue && !IsValidReminderTime(reminderTime.Value, startTime))
            {
                return (AppointmentActionResult.ValidationError, "Reminder time must be before the appointment start time.", null);
            }

            TimeSpan inputDuration = endTime - startTime;

            if (!forceIndividual)
            {
                string nameUpper = name.ToUpper();

                var potentialGroupMeetingToJoin = _db.Appointments.OfType<GroupMeeting>()
                   .Where(gm => gm.Name.ToUpper() == nameUpper && 
                                (gm.StartTime < endTime && gm.EndTime > startTime)) 
                   .Include(gm => gm.MeetingParticipants)
                   .AsEnumerable() 
                   .FirstOrDefault(gm =>
                       (gm.EndTime - gm.StartTime) == inputDuration &&
                      // gm.Location.Equals(location, StringComparison.OrdinalIgnoreCase) && // If you add this, also convert gm.Location.ToUpper() == location.ToUpper()
                       !gm.MeetingParticipants.Any(mp => mp.ParticipantId == _currentUser.Id) // User is not already a participant
                   );

                if (potentialGroupMeetingToJoin != null)
                {
                    string joinMessage = $"A group meeting '{potentialGroupMeetingToJoin.Name}' with the same name and duration exists at this time. Join it?:{potentialGroupMeetingToJoin.Id}";
                    return (AppointmentActionResult.AskToJoinGroupMeeting, joinMessage, potentialGroupMeetingToJoin);
                }
            }

            var conflictingAppointment = _db.Appointments
                .Include(a => (a as GroupMeeting).MeetingParticipants)
                .Where(a =>
                    (a.OwnerId == _currentUser.Id || (a is GroupMeeting && ((GroupMeeting)a).MeetingParticipants.Any(mp => mp.ParticipantId == _currentUser.Id))) &&
                    (startTime < a.EndTime && endTime > a.StartTime)
                   )
                .FirstOrDefault();

            if (conflictingAppointment != null)
            {
                string conflictMessage = conflictingAppointment is GroupMeeting
                    ? $"You are already scheduled for a group meeting '{conflictingAppointment.Name}' during this time."
                    : $"You already have an appointment '{conflictingAppointment.Name}' scheduled during this time.";
                return (AppointmentActionResult.ConflictDetected, conflictMessage, conflictingAppointment);
            }

            // --- Bước 4: Tạo Appointment mới ---
            try
            {
                Appointment newAppt = new Appointment(name, location, startTime, endTime, _currentUser.Id) { Owner = _currentUser };
                _db.Appointments.Add(newAppt);

                if (reminderTime.HasValue)
                {
                    Reminder newReminder = new Reminder(reminderTime.Value, 0, _currentUser.Id)
                    { User = _currentUser, RelatedAppointment = newAppt };
                    _db.Reminders.Add(newReminder);
                }

                _db.SaveChanges();
                return (AppointmentActionResult.Success, "Appointment added successfully.", newAppt);
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"DB Save Error in AddAppointment: {dbEx.InnerException?.Message ?? dbEx.Message}");
                return (AppointmentActionResult.DatabaseError, $"Error saving appointment: {dbEx.InnerException?.Message ?? dbEx.Message}", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generic Save Error in AddAppointment: {ex.Message}");
                return (AppointmentActionResult.UnknownError, $"An unexpected error occurred: {ex.Message}", null);
            }
        }


        public (AppointmentActionResult Status, string Message, Appointment RelatedAppointment) AddGroupMeeting(
           string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            if (string.IsNullOrWhiteSpace(name)) return (AppointmentActionResult.ValidationError, "Meeting name is required.", null);
            if (endTime <= startTime) return (AppointmentActionResult.ValidationError, "End time must be after start time.", null);
            if (reminderTime.HasValue && !IsValidReminderTime(reminderTime.Value, startTime))
            {
                return (AppointmentActionResult.ValidationError, "Reminder time must be before the meeting start time.", null);
            }

            var conflictingAppointmentForCreator = _db.Appointments
                .Include(a => (a as GroupMeeting).MeetingParticipants)
                .Where(a =>
                    (a.OwnerId == _currentUser.Id || (a is GroupMeeting && ((GroupMeeting)a).MeetingParticipants.Any(mp => mp.ParticipantId == _currentUser.Id))) &&
                    (startTime < a.EndTime && endTime > a.StartTime)
                   )
                .FirstOrDefault();

            if (conflictingAppointmentForCreator != null)
            {
                string conflictMessage = conflictingAppointmentForCreator is GroupMeeting
                    ? $"This new group meeting conflicts with your existing group meeting: '{conflictingAppointmentForCreator.Name}'."
                    : $"This new group meeting conflicts with your existing appointment: '{conflictingAppointmentForCreator.Name}'.";
                return (AppointmentActionResult.ConflictDetected, conflictMessage, conflictingAppointmentForCreator);
            }

            try
            {
                var newGroupMeeting = new GroupMeeting 
                {
                    Name = name,
                    Location = location,
                    StartTime = startTime,
                    EndTime = endTime,
                    OwnerId = _currentUser.Id,
                    Owner = _currentUser
                };
                _db.Appointments.Add(newGroupMeeting); 

                var initialParticipant = new GroupMeetingParticipant
                {
                    GroupMeeting = newGroupMeeting,
                    ParticipantId = _currentUser.Id,
                    Participant = _currentUser
                };
                _db.GroupMeetingParticipants.Add(initialParticipant);

                if (reminderTime.HasValue)
                {
                    Reminder newReminder = new Reminder(reminderTime.Value, 0, _currentUser.Id)
                    { User = _currentUser, RelatedAppointment = newGroupMeeting };
                    _db.Reminders.Add(newReminder);
                }

                _db.SaveChanges();
                return (AppointmentActionResult.Success, "Group meeting created successfully.", newGroupMeeting);
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"DB Error creating group meeting: {dbEx.InnerException?.Message ?? dbEx.Message}");
                return (AppointmentActionResult.DatabaseError, $"Error saving group meeting: {dbEx.InnerException?.Message ?? dbEx.Message}", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generic Error creating group meeting: {ex.Message}");
                return (AppointmentActionResult.UnknownError, $"An unexpected error occurred: {ex.Message}", null);
            }
        }

        public (bool success, string message, Appointment newAppointment) PerformReplaceAppointment(
            Appointment appointmentToReplace, string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            if (appointmentToReplace == null) return (false, "Original appointment to replace is null.", null);

            var oldAppointmentInDb = _db.Appointments
                                        .Include(a => (a as GroupMeeting).MeetingParticipants) 
                                        .FirstOrDefault(a => a.Id == appointmentToReplace.Id);

            if (oldAppointmentInDb == null) return (false, "Could not find appointment to replace in database.", null);

            if (oldAppointmentInDb.OwnerId != _currentUser.Id)
            {
                return (false, "Permission denied: Only the owner can replace this appointment.", null);
            }

            if (reminderTime.HasValue && !IsValidReminderTime(reminderTime.Value, startTime))
            {
                return (false, "New reminder time must be before the new start time. Replacement aborted.", null);
            }

            if (oldAppointmentInDb is GroupMeeting oldGroupMeeting)
            {
                if (oldGroupMeeting.MeetingParticipants != null && oldGroupMeeting.MeetingParticipants.Any())
                {
                    _db.GroupMeetingParticipants.RemoveRange(oldGroupMeeting.MeetingParticipants);
                }
            }

            var remindersToDelete = _db.Reminders
                                       .Where(r => r.RelatedAppointmentId == oldAppointmentInDb.Id)
                                       .ToList();
            if (remindersToDelete.Any())
            {
                _db.Reminders.RemoveRange(remindersToDelete);
            }

            _db.Appointments.Remove(oldAppointmentInDb);

            Appointment newAppt = new Appointment(name, location, startTime, endTime, _currentUser.Id) { Owner = _currentUser };
            _db.Appointments.Add(newAppt);

            if (reminderTime.HasValue) 
            {
                Reminder newReminder = new Reminder(reminderTime.Value, 0, _currentUser.Id)
                { User = _currentUser, RelatedAppointment = newAppt };
                _db.Reminders.Add(newReminder);
            }

            try
            {
                _db.SaveChanges();
                return (true, "Appointment replaced successfully.", newAppt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generic Error replacing appointment: {ex.Message}");
                return (false, $"An unexpected error occurred during replacement: {ex.Message}", null);
            }
        }

        public bool ConfirmJoinGroupMeeting(int meetingId, User userJoining)
        {
            var meeting = _db.Appointments.OfType<GroupMeeting>().FirstOrDefault(m => m.Id == meetingId);
            var userInDb = _db.Users.Find(userJoining.Id);
            if (meeting == null || userInDb == null) return false;

            bool alreadyExists = _db.GroupMeetingParticipants.Any(gmp => gmp.GroupMeetingId == meetingId && gmp.ParticipantId == userInDb.Id);
            if (alreadyExists) return true; 

            var joinEntry = new GroupMeetingParticipant { GroupMeetingId = meeting.Id, ParticipantId = userInDb.Id };
            _db.GroupMeetingParticipants.Add(joinEntry);
            try
            {
                _db.SaveChanges(); return true;
            }
            catch (Exception ex) { Console.WriteLine($"Error saving join entry: {ex.InnerException?.Message ?? ex.Message}"); return false; }
        }

        public bool IsValidReminderTime(DateTime reminderDateTime, DateTime appointmentStartDateTime)
        {
            return reminderDateTime < appointmentStartDateTime;
        }

        public List<Appointment> GetUserAppointments(User user) 
        {
            return _db.Appointments
                .Include(a => (a as GroupMeeting).MeetingParticipants)
                .Where(a =>
                    a.OwnerId == user.Id ||

                    (
                        a is GroupMeeting && 
                        ((GroupMeeting)a).MeetingParticipants.Any(mp => mp.ParticipantId == user.Id) 
                    )
                )
                .OrderBy(a => a.StartTime)
                .ToList(); 
        }
        public List<Reminder> GetUserReminders(User user)
        {
            if (user == null) return new List<Reminder>();
            return _db.Reminders.Where(r => r.UserId == user.Id).Include(r => r.RelatedAppointment).OrderBy(r => r.TriggerTime).ToList();
        }
        public void Dispose() { _db?.Dispose(); GC.SuppressFinalize(this); }
    }

    public enum AppointmentActionResult
    {
        Success,
        ValidationError,
        ConflictDetected,
        AskToJoinGroupMeeting,
        DatabaseError,
        UnknownError
    }
}