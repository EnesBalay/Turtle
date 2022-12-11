using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IMeetingDal : IGenericDal<Meeting>
    {
        public Meeting GetMeetingWithIdentityName(string meetingID);
        public List<Meeting> GetMeetingsBySearch(string search);
    }
}
