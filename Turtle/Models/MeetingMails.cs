using EntityLayer.Concrete;

namespace Turtle.Models
{
    public class MeetingMails
    {
        public int MeetingID { get; set; }
        public string MeetingName { get; set; }
        public string? Description { get; set; }
        public DateTime PlanningDate { get; set; }
        public DateTime PlanningDate2 { get; set; }
        public DateTime PlanningDate3 { get; set; }

        public DateTime? CreateDate { get; set; }
        public string Location { get; set; }
        public int MeetingDuration { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<string> Mails { get; set; }
    }
}
