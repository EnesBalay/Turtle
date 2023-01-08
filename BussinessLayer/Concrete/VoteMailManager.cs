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
    public class VoteMailManager : IVoteMailService
    {
        IVoteMailDal _voteMailDal;
        public VoteMailManager(IVoteMailDal voteMailDal)
        {
            _voteMailDal = voteMailDal;
        }
        public void Add(VoteMail t)
        {
            _voteMailDal.Add(t);
        }

        public VoteMail GetById(int id)
        {
            return _voteMailDal.GetByID(id);
        }

        public List<VoteMail> GetListAll()
        {
            return _voteMailDal.GetAll();
        }

        public VoteMail GetVoteMailByMail(string mail, int meetingId)
        {
            return _voteMailDal.GetVoteMailByMail(mail,meetingId);
        }

        public List<VoteMail> GetVoteMailsByMeetingId(int id)
        {
            return _voteMailDal.GetVoteMailsByMeetingId(id);
        }

        public void Remove(VoteMail t)
        {
            _voteMailDal.Delete(t);
        }

        public void Update(VoteMail t)
        {
            _voteMailDal.Update(t);
        }
    }
}
