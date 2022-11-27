using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Meeting
    {
        [Key]
        public int MeetingID { get; set; }
        public string MeetingName { get; set; }
        public DateTime PlanningDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Location { get; set; }
        public int MeetingDuration { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
