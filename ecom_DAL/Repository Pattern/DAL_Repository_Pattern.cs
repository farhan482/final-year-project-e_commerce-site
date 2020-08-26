using ecom_BOL;
using ecom_BOL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ecom_DAL.Repository_Pattern
{
    public class DAL_Repository_Pattern<T> : DAL_IRepository_Pattern<T> where T : class
    {

        private ecommerce_site_version_v_19_0Entities _context;
        private IDbSet<T> dbentity;
        
      
        public DAL_Repository_Pattern()
        {
            _context = new ecommerce_site_version_v_19_0Entities();
            dbentity = _context.Set<T>();
            _context.Configuration.ProxyCreationEnabled = false;
        }

        public virtual IEnumerable<T> Get(
       
        
         string includeProperties = "")
        {
            IQueryable<T> query = dbentity;

          

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


          
                return query.ToList();
            
        }
        public virtual IEnumerable<T> Getbyid(

             Expression<Func<T, bool>> filter = null,
 string includeProperties = "")
        {
            IQueryable<T> query = dbentity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }



            return query.ToList();

        }
        public void deletemodel(int id)
        {

         
            var result = dbentity.Find(id);
            dbentity.Remove(result);
            _context.SaveChanges();
        }

        public IEnumerable<T> getmodel()
        {
            
            return dbentity.ToList();
        }

        public T getmodelbyid(int id)
        {
            return dbentity.Find(id);
            
        }

        public void insertmodel(T model)
        {
            dbentity.Add(model);
           _context.SaveChanges();

        }

       

        public void updatemodel(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}