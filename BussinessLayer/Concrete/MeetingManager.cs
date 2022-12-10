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

        public Meeting GetById(int id)
        {
            return _meetingDal.GetByID(id);
        }

        public List<Meeting> GetListAll()
        {
            return _meetingDal.GetAll();
        }

        public Meeting GetMeetingByIdentityName(string meetingName)
        {
            return _meetingDal.GetMeetingWithIdentityName(meetingName);
        }

        public List<Meeting> GetMeetingBySearch(string search)
        {
            return _meetingDal.GetMeetingsBySearch(search);
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
