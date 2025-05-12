// GroupMeeting.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CalendarApp.Models
{
    public class GroupMeeting : Appointment
    {
        // 1 GroupMeeting -> Nhiều GroupMeetingParticipant (Nhiều người tham gia qua bảng join)
        public virtual ICollection<GroupMeetingParticipant> MeetingParticipants { get; set; } = new List<GroupMeetingParticipant>();

        // Constructors gọi base constructor
        public GroupMeeting() : base() { }

        public GroupMeeting(string name, string location, DateTime startTime, DateTime endTime, int ownerId)
            : base(name, location, startTime, endTime, ownerId) { }

        // Logic AddParticipant nên nằm trong CalendarManager để tương tác với DbContext

        public override string GetDetails()
        {
            return $"Group Meeting: {Name} at {Location} from {StartTime:g} to {EndTime:g}";
        }
    }
}