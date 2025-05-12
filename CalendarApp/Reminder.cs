// Reminder.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalendarApp.Models
{
    public class Reminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TriggerTime { get; set; }

        // Khóa ngoại tới Appointment mà Reminder này liên quan
        public int RelatedAppointmentId { get; set; }

        // Khóa ngoại tới User sở hữu Reminder này
        public int UserId { get; set; }

        // Navigation property tới Appointment liên quan (1 Reminder chỉ cho 1 Appointment)
        [ForeignKey("RelatedAppointmentId")]
        public virtual Appointment RelatedAppointment { get; set; }

        // Navigation property tới User sở hữu (1 Reminder chỉ có 1 User sở hữu)
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        // Constructor
        public Reminder() { }

        public Reminder(DateTime triggerTime, int relatedAppointmentId, int userId)
        {
            TriggerTime = triggerTime;
            RelatedAppointmentId = relatedAppointmentId;
            UserId = userId;
        }

        public string GetReminderInfo()
        {
            string apptName = RelatedAppointment?.Name ?? $"ID {RelatedAppointmentId}";
            return $"Reminder for '{apptName}' at {TriggerTime:g}";
        }

        public override string ToString()
        {
            string forAppt = RelatedAppointment != null ? $" for: {RelatedAppointment.Name}" : "";
            return $"Remind at {TriggerTime:g}{forAppt}";
        }
    }
}