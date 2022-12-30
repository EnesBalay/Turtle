using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface IVoteMailService:IGenericService<VoteMail>
    {
        public List<VoteMail> GetVoteMailsByMeetingId(int id);
    }
}
