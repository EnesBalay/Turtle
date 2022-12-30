using BussinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class MeetingManager: IMeetingService
    {
        IMeetingDal _meetingDal;
        public MeetingManager(IMeetingDal meetingDal)
        {
            _meetingDal = meetingDal;
        }
        public void Add(Meeting t)
        {
            _meetingDal.Add(t);
        }

        public int AddReturnId(Meeting meeting)
        {
            return _meetingDal.AddReturnId(meeting);
        }

        public Meeting GetById(int id)
        {
            return _meetingDal.GetByID(id);
        }

        public List<Meeting> GetListAll()
        {
            return _meetingDal.GetAll();
        }

        public Meeting GetMeetingByIdentityName(string meetingID)
        {
            return _meetingDal.GetMeetingWithIdentityName(meetingID);
        }

        public List<Meeting> GetMeetingBySearch(string search)
        {
            return _meetingDal.GetMeetingsBySearch(search);
        }

        public List<Meeting> GetMeetingsByUserId(int userId)
        {
            return _meetingDal.GetMeetingsByUserId(userId);
        }

        public void Remove(Meeting t)
        {
            _meetingDal.Delete(t);
        }

        public void Update(Meeting t)
        {
            _meetingDal.Update(t);
        }

       
    }
}
