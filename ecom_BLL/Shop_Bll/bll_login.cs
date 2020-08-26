using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BLL.Shop_Bll
{
    public class bll_login
    {
        private DAL_IRepository_Pattern<user_tbl> objuser;
        private DAL_IRepository_Pattern<user_img_tbl> objuserimg;
        private DAL_IRepository_Pattern<userrole_tbl> objuserrole;
        public bll_login()
        {
            objuser = new DAL_Repository_Pattern<user_tbl>();
            objuserimg = new DAL_Repository_Pattern<user_img_tbl>();
            objuserrole = new DAL_Repository_Pattern<userrole_tbl>();
        }
        public void adduser(Custom_user_role user)
        {
            if (user!=null)
            {
                user_tbl usertbl = user.user;
                user_img_tbl userimgtbl = user.user_image;
                userrole_tbl roletbl = user.user_role;
                usertbl.user_status = 1;
                objuser.insertmodel(usertbl);
              
                string filename = Path.GetFileNameWithoutExtension(userimgtbl.userimg.FileName);
                string exten = Path.GetExtension(userimgtbl.userimg.FileName);
                filename = filename + Guid.NewGuid() + exten;
                string path = "/images/userimage/" + filename;
                userimgtbl.image_path = path;
                filename = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("/images/userimage/"), filename);
                userimgtbl.userimg.SaveAs(filename);
                userimgtbl.user_id = usertbl.user_id;
                objuserimg.insertmodel(userimgtbl);
             
                roletbl.user_id=usertbl.user_id;
                roletbl.role_id = 2;
                objuserrole.insertmodel(roletbl);

            }
        }
        public void updateuser(user_tbl user)
        {
            if (user!=null)
            {
                objuser.updatemodel(user);
            }
        }
        public void deleteuser(int? id)
        {
            if (id!=null)
            {
                objuser.deletemodel(Convert.ToInt32(id));
            }
        }
        public dynamic loggeduser(user_tbl user)
        {
            var isvalid = (from userimg in objuserimg.getmodel()
                           join usertbl in objuser.getmodel() on userimg.user_id equals usertbl.user_id
                           select new Custom_user_role
                           {
                               user=usertbl,
                               user_image=userimg

                           }).Where(x=>x.user.user_name==user.user_name && x.user.password==user.password);
            return isvalid;
        }
        
    }
}