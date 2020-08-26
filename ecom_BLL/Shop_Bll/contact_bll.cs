using ecom_BOL.Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BLL.Shop_Bll
{
   public class contact_bll
    {
        private DAL_IRepository_Pattern<contact> objcon;
        public contact_bll()
        {
            objcon = new DAL_Repository_Pattern<contact>();
        }
        public void savemsg(contact contact)
        {
            if (contact!=null)
            {
                contact.status = "Unreplied";
                objcon.insertmodel(contact);
            }
        }
        public dynamic getcontact()
        {
            var result = objcon.getmodel().ToList();
            return result;
        }
        public dynamic getconbyid(int id)
        {
            var result = objcon.getmodelbyid(id);
            return result;
        }
        public void updatecon(contact con)
        {
            if (con != null)
            {
                objcon.updatemodel(con);
            }
        }
    }
}
