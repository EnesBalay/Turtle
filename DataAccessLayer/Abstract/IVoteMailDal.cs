using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IVoteMailDal:IGenericDal<VoteMail>
    {
        public List<VoteMail> GetVoteMailsByMeetingId(int meetingId);
    }
}
