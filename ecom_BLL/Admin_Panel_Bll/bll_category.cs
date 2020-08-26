using ecom_BOL.Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BLL.Admin_Panel_Bll
{
    public class bll_category
    {
        private DAL_IRepository_Pattern<category_tbl> objcategory;
        private DAL_IRepository_Pattern<sub_category_tbl> objsubcate;
        public bll_category()
        {
            objcategory = new DAL_Repository_Pattern<category_tbl>();
            objsubcate = new DAL_Repository_Pattern<sub_category_tbl>();
        }
        public IEnumerable<category_tbl> getcategory()
        {
           
            var result = (from cate in objcategory.getmodel() orderby cate.category_id descending select cate).ToList();
            return result;
        }
       
        public void create_category(category_tbl catename)
        {
            if (catename != null)
            {
                objcategory.insertmodel(catename);
            }
        }
        public void delete_category(int? id)
        {
            if (id != null)
            {
                objcategory.deletemodel(Convert.ToInt32(id));
            }
        }
        public void update_category(category_tbl cate)
        {
            if (cate != null)
            {
                objcategory.updatemodel(cate);
            }
        }
       
      
       
      
     
    }
}
