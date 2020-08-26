using ecom_BLL.Admin_Panel_Bll;
using ecom_BLL.Shop_Bll;
using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ecom_UI.Areas.Shop.Controllers
{
    public class LoginController : Controller
    {
        private bll_login objblllogin;
        public LoginController()
        {
            objblllogin = new bll_login();
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult userregister(Custom_user_role user)
        {  
           
            objblllogin.adduser(user);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult updateuser(user_tbl user)
        {
            objblllogin.updateuser(user);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult deleteuser(int id)
        {
            objblllogin.deleteuser(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult loggeduser(user_tbl user)
        {
           
             
            var isvalid = objblllogin.loggeduser(user);
            foreach (var item in isvalid)
            {
              Session["img"]= item.user_image.image_path;
                if (item != null)
                {
                    FormsAuthentication.SetAuthCookie(user.user_name, false);
                    return Json(isvalid, JsonRequestBehavior.AllowGet);
                }
               
                  
                
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }
        public ActionResult logout()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Home","Home","Shop");
        
            
        }
    }
}