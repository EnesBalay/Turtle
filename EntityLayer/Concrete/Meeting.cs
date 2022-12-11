using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Meeting
    {
        [Key]
        public int MeetingID { get; set; }
        public string MeetingName { get; set; }
        public string? Description { get; set; }
        public DateTime PlanningDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Location { get; set; }
        public int MeetingDuration { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
