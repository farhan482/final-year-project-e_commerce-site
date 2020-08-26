using ecom_BOL.Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BLL.Admin_Panel_Bll
{
    public class bll_role
    {
        private DAL_IRepository_Pattern<C_role_tbl> objrole;
        public bll_role()
        {
            objrole = new DAL_Repository_Pattern<C_role_tbl>();
        }
        public IEnumerable<C_role_tbl> get_role()
        {
            var result =(from rolelist in objrole.getmodel() orderby rolelist.role_id descending select rolelist).ToList();
            return result;
        }
        public void insert_role(C_role_tbl role)
        {
            if (role!=null)
            {
                objrole.insertmodel(role);
            }
        }
        public void delete_role(int? id)
        {
            if(id != null)
            {
                objrole.deletemodel(Convert.ToInt32(id));
            }
        }
        public void update_role(C_role_tbl role)
        {
            if (role!=null)
            {
                objrole.updatemodel(role);
            }
        }
        
    }
}
