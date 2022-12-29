using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IVoteMeetingDal:IGenericDal<VoteMail>
    {
        public VoteMail GetVoteWithIdentityName(string Id);
        public List<VoteMail> GetVotesBySearch(string search);
    }
}
