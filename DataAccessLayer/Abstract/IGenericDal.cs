using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Add(T t);
        void Update(T t);
        void Delete(T t);
        List<T> GetAll();
        T GetByID(int id);
        List<T> GetAll(Expression<Func<T, bool>> filter);
    }
}
