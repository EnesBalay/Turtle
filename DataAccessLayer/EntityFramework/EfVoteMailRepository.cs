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
    public class EfVoteMailRepository : GenericRepository<VoteMail>, IVoteMailDal
    {
        public List<VoteMail> GetVoteMailsByMeetingId(int meetingId)
        {
            using var c = new Context();
            return c.VoteMails.Where(x => x.MeetingId == meetingId).ToList();
        }
    }
}
