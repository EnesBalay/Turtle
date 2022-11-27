using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Abstract
{
    public interface IUserService:IGenericService<User>
    {
        public User GetUserByIdentityName(String userName);
        public List<User> GetUserBySearch(String search);
    }
}
