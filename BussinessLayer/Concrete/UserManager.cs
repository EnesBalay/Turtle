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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public void Add(User t)
        {
            _userDal.Add(t);
        }

        public User GetById(int id)
        {
            return _userDal.GetByID(id);
        }

        public List<User> GetListAll()
        {
            return _userDal.GetAll();
        }

        public User GetUserByIdentityName(string userName)
        {
            return _userDal.GetUserWithIdentityName(userName);
        }

        public List<User> GetUserBySearch(string search)
        {
            return _userDal.GetUsersBySearch(search);
        }

        public void Remove(User t)
        {
            _userDal.Delete(t);
        }

        public void Update(User t)
        {
            _userDal.Update(t);
        }
    }
}
