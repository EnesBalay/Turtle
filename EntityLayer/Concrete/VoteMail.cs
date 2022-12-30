using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class VoteMail
    {
        [Key]
        public int Id { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
        public int ChoosedDate { get; set; }
        public int MeetingId { get; set; }
        public Meeting meeting { get; set; }
        
    }
}
