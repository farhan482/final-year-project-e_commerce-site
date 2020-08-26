using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BLL.Admin_Panel_Bll
{
   
    public class bll_user
    {
        private DAL_IRepository_Pattern<user_tbl> objuser;
        private DAL_IRepository_Pattern<userrole_tbl> objuser_role;
        private DAL_IRepository_Pattern<user_img_tbl> objuser_img;
        private DAL_IRepository_Pattern<C_role_tbl> obj_role;
        public bll_user()
        {
            objuser = new DAL_Repository_Pattern<user_tbl>();
            objuser_role = new DAL_Repository_Pattern<userrole_tbl>();
            objuser_img = new DAL_Repository_Pattern<user_img_tbl>();
            obj_role = new DAL_Repository_Pattern<C_role_tbl>();
        }
        public IEnumerable<user_tbl> get_user_list()
        {
            return (from user in objuser.getmodel() orderby user.user_id descending select user).ToList();
        }
        public dynamic chkuser(Custom_user_role userchk)
        {
            var result= (from userrole in objuser_role.getmodel()
                         join user in objuser.getmodel()
                         on userrole.user_id equals user.user_id
                         join role in obj_role.getmodel() on userrole.role_id equals role.role_id
                         join userimg in objuser_img.getmodel() on user.user_id equals userimg.user_id                     
                         select new Custom_user_role
                         {
                           
                             user=user,
                             user_image=userimg,
                             user_role=userrole
                          


                         }).Where(x => x.user.user_name == userchk.user.user_name && x.user.password == userchk.user.password && x.user_role.role_id==userchk.user_role.role_id);
          
            return result;
        }
        public dynamic full_detail_of_user(int? id)
        {
            if (id!=null)
            {
                var userinfo = (from userrole in objuser_role.getmodel()
                                join user in objuser.getmodel()
                                on userrole.user_id equals user.user_id
                                join role in obj_role.getmodel() on userrole.role_id equals role.role_id
                                join userimg in objuser_img.getmodel() on user.user_id equals userimg.user_id
                                where user.user_id == id
                                select new
                                {
                                    user.user_id,
                                    user.user_name,
                                    user.password,
                                    user.confirm_password,
                                    user.email_address,
                                    user.contact_no,
                                    user.user_status,
                                    role.user_role,
                                    role.role_id,
                                    userimg.image_path,
                                    userimg.user_img_id,
                                    userrole.userrole_id


                                }).ToList();
                return userinfo;

            }
            return "";
        }
        public void insert_user(Custom_user_role cus_user_role_img)
        {
            user_tbl usertbl = cus_user_role_img.user;
            userrole_tbl user_role_tbl = cus_user_role_img.user_role;
            user_img_tbl userimg = cus_user_role_img.user_image;
            string filename = Path.GetFileNameWithoutExtension(userimg.userimg.FileName);
            string exten = Path.GetExtension(userimg.userimg.FileName);
            filename = filename + Guid.NewGuid() + exten;
            string path = "/images/userimage/"+filename;
            userimg.image_path = path;
            filename = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("/images/userimage/"), filename);
            userimg.userimg.SaveAs(filename);
            if (usertbl != null)
            {
                objuser.insertmodel(usertbl);
                user_role_tbl.user_id = usertbl.user_id;
            }
            if (userimg!=null)
            {
                userimg.user_id = usertbl.user_id;
                objuser_img.insertmodel(userimg);

            }
            if (user_role_tbl!=null)
            {
                objuser_role.insertmodel(user_role_tbl);
            }
        }
        public void delete_user(int? id)
        {
            if (id!=null)
            {
                objuser.deletemodel(Convert.ToInt32(id));

            }
        }
        public void update_user(Custom_user_role cus_user_role_img)
        {
            user_tbl usertbl = cus_user_role_img.user;
            user_img_tbl userimg = cus_user_role_img.user_image;
            userimg.user_id = usertbl.user_id;
            userrole_tbl user_role_tbl = cus_user_role_img.user_role;
            user_role_tbl.user_id = usertbl.user_id;
          
            if (userimg.userimg!=null)
            {
                string filename = Path.GetFileNameWithoutExtension(userimg.userimg.FileName);
                string exten = Path.GetExtension(userimg.userimg.FileName);
                filename = filename + Guid.NewGuid() + exten;
                string path = "/images/userimage/" + filename;
                userimg.image_path = path;
                filename = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("/images/userimage/"), filename);
                userimg.userimg.SaveAs(filename);
                objuser_img.updatemodel(userimg);
            }
           
            if (usertbl != null)
            {
                objuser.updatemodel(usertbl);
            }
            if (user_role_tbl != null)
            {
                objuser_role.updatemodel(user_role_tbl);
            }

        }
       
    }
}
