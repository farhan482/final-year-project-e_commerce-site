using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class AdminLoginController : Controller
    {
        private bll_user objuser;
        private bll_role objrole;
        public AdminLoginController()
        {
            objuser = new bll_user();
            objrole = new bll_role();
        }
        public void usertype()
        {
            var result = objrole.get_role();
            ViewBag.userrole = new SelectList(result, "role_id", "user_role");
        }
        public ActionResult adminlogin()
        {
            usertype();
            return View();
        }
        [HttpPost]
        public ActionResult userchk(Custom_user_role userchk)
        {
           
        
            var result = objuser.chkuser(userchk);
            foreach (var item in result)
            {
                Session["userimg"] = item.user_image.image_path;
                if(item!=null)
                {
                    FormsAuthentication.SetAuthCookie(item.user.user_name,false);
                }
              
            }
       
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult logoutuser()
        {
            FormsAuthentication.SignOut();
            return Redirect("/AdminPanel/AdminLogin/adminlogin");
        }
    }
}