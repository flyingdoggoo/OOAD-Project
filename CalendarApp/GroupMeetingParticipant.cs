// GroupMeetingParticipant.cs
namespace CalendarApp.Models
{
    public class GroupMeetingParticipant
    {
        // Khóa ngoại và Navigation property tới GroupMeeting
        public int GroupMeetingId { get; set; }
        public virtual GroupMeeting GroupMeeting { get; set; }

        // Khóa ngoại và Navigation property tới User (Participant)
        public int ParticipantId { get; set; }
        public virtual User Participant { get; set; }
    }
}