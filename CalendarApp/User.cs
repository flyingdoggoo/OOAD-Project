// User.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalendarApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        public string Password { get; set; } // Reminder: Hash this in a real app!

        // 1 User -> Nhiều Appointment (User là Owner)
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // 1 User -> Nhiều Reminder (User sở hữu Reminder)
        public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

        // 1 User -> Nhiều GroupMeetingParticipant (User tham gia nhiều meeting qua bảng join)
        public virtual ICollection<GroupMeetingParticipant> MeetingParticipations { get; set; } = new List<GroupMeetingParticipant>();

        // Constructor
        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string GetUserInfo()
        {
            return $"User: {Username}, ID: {Id}";
        }

        public override string ToString()
        {
            return Username ?? $"User ID {Id}";
        }
    }
}