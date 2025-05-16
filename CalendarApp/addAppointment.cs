using System;
using System.Windows.Forms;
using CalendarApp.Models;
using CalendarApp.Services;

namespace CalendarApp
{
    public partial class addAppointment : Form 
    {
        private User _loggedInUser;
        private CalendarManager _calendarManager;

        public addAppointment(User loggedInUser, DateTime initialStartTime)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;

            if (_loggedInUser == null || string.IsNullOrEmpty(_loggedInUser.Username))
            {
                MessageBox.Show("Logged in user information is missing. Cannot proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
                this.Load += (s, e) => Close(); 
                return;
            }
            _calendarManager = new CalendarManager(_loggedInUser.Username);

            dtpStartTime.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpReminderTime.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpReminderTime.Format = DateTimePickerFormat.Custom;

            InitializeInputFields(initialStartTime);
        }

        private void InitializeInputFields(DateTime initialStartTime)
        {
            txtName.Clear();
            txtLocation.Clear();

            var now = DateTime.Now;
            if (initialStartTime < now) 
            {
                var nextQuarterHour = now.AddMinutes(15 - (now.Minute % 15)).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
                if (nextQuarterHour <= now) nextQuarterHour = nextQuarterHour.AddMinutes(15);
                initialStartTime = nextQuarterHour;
            }

            dtpStartTime.Value = initialStartTime;
            dtpEndTime.Value = initialStartTime.AddHours(1); 
            chkEnableReminder.Checked = false;
            chkEnableReminder_CheckedChanged(null, null);
        }


        private void chkEnableReminder_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkEnableReminder.Checked;

            if (isChecked)
            {
                dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-15);
                dtpReminderTime.MaxDate = dtpStartTime.Value.AddMinutes(-1);
            }
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndTime.Value <= dtpStartTime.Value)
            {
                dtpEndTime.Value = dtpStartTime.Value.AddHours(1);
            }

            dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-15);
            if (dtpReminderTime.Value >= dtpStartTime.Value)
            {
                dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-1);
            }
        }

        private bool ValidateInput(out string name, out string location, out DateTime startTime, out DateTime endTime, out DateTime? reminderTime)
        {
            name = txtName.Text.Trim();
            location = txtLocation.Text.Trim();
            startTime = dtpStartTime.Value;
            endTime = dtpEndTime.Value;
            reminderTime = null;

            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Name/Meeting Title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtName.Focus(); return false; }
            if (endTime <= startTime) { MessageBox.Show("End time must be after start time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtpEndTime.Focus(); return false; }

            if (chkEnableReminder.Checked)
            {
                if (dtpReminderTime.Value >= dtpStartTime.Value)
                {
                    MessageBox.Show("Reminder time must be before the start time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpReminderTime.Focus();
                    return false;
                }
                reminderTime = dtpReminderTime.Value;
            }
            return true;
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (_calendarManager == null) return;
            if (!ValidateInput(out string name, out string location, out DateTime startTime, out DateTime endTime, out DateTime? reminderTime))
            {
                return; 
            }

            var result = _calendarManager.AddAppointment(name, location, startTime, endTime, reminderTime, forceIndividual: false);
            HandleManagerResultForAppointment(result, name, location, startTime, endTime, reminderTime);
        }

        private void btnAddGroupMeeting_Click(object sender, EventArgs e)
        {
            if (_calendarManager == null) return;
            if (!ValidateInput(out string name, out string location, out DateTime startTime, out DateTime endTime, out DateTime? reminderTime))
            {
                return; 
            }

            var result = _calendarManager.AddGroupMeeting(name, location, startTime, endTime, reminderTime);
            HandleManagerResultForGroupMeeting(result);
        }

        private void HandleManagerResultForAppointment((AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                        string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            switch (result.Status)
            {
                case AppointmentActionResult.Success:
                    MessageBox.Show(result.Message, "Appointment Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case AppointmentActionResult.ConflictDetected:
                    HandleConflict(result, name, location, startTime, endTime, reminderTime);
                    break;
                case AppointmentActionResult.AskToJoinGroupMeeting:
                    HandleAskToJoin(result, name, location, startTime, endTime, reminderTime);
                    break;
                case AppointmentActionResult.ValidationError:
                case AppointmentActionResult.DatabaseError:
                case AppointmentActionResult.UnknownError:
                default:
                    MessageBox.Show(result.Message, "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void HandleManagerResultForGroupMeeting((AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result)
        {
            switch (result.Status)
            {
                case AppointmentActionResult.Success:
                    MessageBox.Show(result.Message, "Group Meeting Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                    break;
                case AppointmentActionResult.ConflictDetected:
                    MessageBox.Show(result.Message + "\nPlease choose a different time or resolve the conflict.", "Scheduling Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpStartTime.Focus(); 
                    break;
                case AppointmentActionResult.ValidationError:
                case AppointmentActionResult.DatabaseError:
                case AppointmentActionResult.UnknownError:
                default:
                    MessageBox.Show(result.Message, "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void HandleAskToJoin((AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                     string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            string[] messageParts = result.Message.Split(':');
            string displayMessage = messageParts[0];
            if (messageParts.Length < 2 || !int.TryParse(messageParts[messageParts.Length - 1], out int meetingId))
            {
                MessageBox.Show("Error parsing meeting ID.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            string meetingName = (result.RelatedAppointment as GroupMeeting)?.Name ?? $"Meeting ID {meetingId}";
            var joinDialogResult = MessageBox.Show($"{displayMessage}\nJoin meeting '{meetingName}' instead?", "Join Meeting?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (joinDialogResult == DialogResult.Yes)
            {
                bool joined = _calendarManager.ConfirmJoinGroupMeeting(meetingId, _calendarManager.GetCurrentUser());
                if (joined) { MessageBox.Show($"Joined: {meetingName}", "Joined", MessageBoxButtons.OK, MessageBoxIcon.Information); this.DialogResult = DialogResult.OK; this.Close(); }
                else { MessageBox.Show("Failed to join meeting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (joinDialogResult == DialogResult.No)
            {
                MessageBox.Show("Creating new individual appointment.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var individualResult = _calendarManager.AddAppointment(name, location, startTime, endTime, reminderTime, forceIndividual: true);
                if (individualResult.Status == AppointmentActionResult.Success)
                {
                    MessageBox.Show("Individual appointment created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); this.DialogResult = DialogResult.OK; this.Close();
                }
                else if (individualResult.Status == AppointmentActionResult.ConflictDetected)
                {
                    HandleConflict(individualResult, name, location, startTime, endTime, reminderTime); // Re-enter conflict logic
                }
                else
                { MessageBox.Show($"Failed to create individual appointment: {individualResult.Message}", "Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void HandleConflict((AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                    string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            var conflictDialogResult = MessageBox.Show(result.Message + "\n\nReplace existing item?", "Conflict", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (conflictDialogResult == DialogResult.Yes)
            {
                var replaceResult = _calendarManager.PerformReplaceAppointment(result.RelatedAppointment, name, location, startTime, endTime, reminderTime);
                if (replaceResult.success) { MessageBox.Show(replaceResult.message, "Replaced", MessageBoxButtons.OK, MessageBoxIcon.Information); this.DialogResult = DialogResult.OK; this.Close(); }
                else { MessageBox.Show("Replace failed: " + replaceResult.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (conflictDialogResult == DialogResult.No)
            {
                MessageBox.Show("Modify start/end time and try again.", "Reschedule", MessageBoxButtons.OK, MessageBoxIcon.Information); dtpStartTime.Focus();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }
    }
}