// Appointment.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CalendarApp.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Khóa ngoại tới User sở hữu Appointment này
        public int OwnerId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [NotMapped]
        public TimeSpan Duration => EndTime - StartTime;

        // Navigation property tới người sở hữu (1 Appointment chỉ có 1 Owner)
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        // Constructor
        public Appointment() { }

        public Appointment(string name, string location, DateTime startTime, DateTime endTime, int ownerId)
        {
            Name = name;
            Location = location;
            StartTime = startTime;
            EndTime = endTime;
            OwnerId = ownerId;
        }

        public virtual string GetDetails()
        {
            return $"Appointment: {Name} at {Location} from {StartTime:g} to {EndTime:g}";
        }

        public override string ToString()
        {
            return $"{Name} ({StartTime:g} - {EndTime:g}) at {Location}";
        }
    }
}