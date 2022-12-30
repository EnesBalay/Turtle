using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMeetingRepository : GenericRepository<Meeting>, IMeetingDal
    {
        public List<Meeting> GetMeetingsBySearch(string search)
        {
            using var c = new Context();
            return c.Meetings.Where(x => x.MeetingName.ToLower().Contains(search.ToLower())).ToList();
        }

        public int AddReturnId(Meeting meeting)
        {
            using var c = new Context();
            c.Add(meeting);
            c.SaveChanges();
            return meeting.MeetingID;
        }


        public Meeting GetMeetingWithIdentityName(string meetingName)
        {
            using var c = new Context();
            return c.Meetings.FirstOrDefault(x => x.MeetingName == meetingName);
        }

        public List<Meeting> GetMeetingsByUserId(int userId)
        {
            using var c = new Context();
            return c.Meetings.Where(x => x.UserID == userId).ToList();
        }
    }
}
