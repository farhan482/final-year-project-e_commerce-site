using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ecom_DAL.Repository_Pattern
{
    public interface DAL_IRepository_Pattern<T> where T:class
    {
        IEnumerable<T> getmodel();
        T getmodelbyid(int id);
        void insertmodel(T model);
        void updatemodel(T model);
        void deletemodel(int id);
        IEnumerable<T> Get(string includeProperties = "");
        IEnumerable<T> Getbyid(Expression<Func<T, bool>> filter = null, string includeProperties = "");

    }
}
