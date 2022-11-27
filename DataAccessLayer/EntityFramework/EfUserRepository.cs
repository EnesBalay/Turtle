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
    public class EfUserRepository : GenericRepository<User>, IUserDal
    {
        public List<User> GetUsersBySearch(string search)
        {
            using var c = new Context();
            return c.Users.Where(x => x.UserName.ToLower().Contains(search.ToLower())).ToList();
        }

        public User GetUserWithIdentityName(string userName)
        {
            using var c = new Context();
            return c.Users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
