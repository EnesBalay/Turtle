using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface IMeetingService : IGenericService<Meeting>
    {
        public Meeting GetMeetingByIdentityName(String meetingName);
        public List<Meeting> GetMeetingBySearch(String search);
        public int AddReturnId(Meeting meeting);
        public List<Meeting> GetMeetingsByUserId(int userId);
    }
}
