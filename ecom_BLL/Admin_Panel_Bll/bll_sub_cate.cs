using ecom_BOL.Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Cryptography;
using ecom_BOL.Model.Custom_Model;

namespace ecom_BLL.Admin_Panel_Bll
{
   public class bll_sub_cate
    {
        private DAL_IRepository_Pattern<sub_category_tbl> objsubcate;
        private DAL_IRepository_Pattern<category_tbl> objparentcate;
        private DAL_IRepository_Pattern<product_tbl> pro;

        public bll_sub_cate()
        {
            objsubcate = new DAL_Repository_Pattern<sub_category_tbl>();
            objparentcate = new DAL_Repository_Pattern<category_tbl>();
            pro = new DAL_Repository_Pattern<product_tbl>();
        }
        public IEnumerable<sub_category_tbl> get_sub_category_by_id(int id)
        {

            var result = (from cate in objsubcate.getmodel() where cate.sub_cate_id == id select cate).ToList();
            return result;
        }
        public void create_sub_category(sub_category_tbl sub_catename)
        {
            if (sub_catename != null)
            {
                objsubcate.insertmodel(sub_catename);
            }
        }
        public dynamic get_sub_cate_list_with_join()
        {



            var result = (from subcate in objsubcate.getmodel()
                          join parentcate in objparentcate.getmodel() on subcate.parent_category_id equals parentcate.category_id
                          select new
                          {
                             subcate.sub_category_name,
                             parentcate.category_name,
                             subcate.sub_cate_id


                          }).ToList();
            return result;
        }

        public dynamic get_sub_cate_list_with_join2()
        {


            //var result = db.category_tbl.Include(c => c.sub_category_tbl).ToList();

            var result = objparentcate.Get("sub_category_tbl");
          
            
            return (result);
        }
      
        public IEnumerable<sub_category_tbl> get_sub_cate()
        {
            var result = objsubcate.getmodel().ToList();
            return result;
        }
        public void update_sub_category(sub_category_tbl sub_catename)
        {
            if (sub_catename != null)
            {
                objsubcate.updatemodel(sub_catename);
            }
        }
        public void delete_sub_category(int? id)
        {
            if (id != null)
            {
                objsubcate.deletemodel(Convert.ToInt32(id));
            }
        }

    }
}
