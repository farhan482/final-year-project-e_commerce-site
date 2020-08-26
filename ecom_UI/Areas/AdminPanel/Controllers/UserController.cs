using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model.Custom_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class UserController : Controller
    {
        private bll_role objbllrole;
        private bll_user objblluser;
        public UserController()
        {
            objbllrole = new bll_role();
            objblluser = new bll_user();
        }
        public void user_active_deactive_code() 
        {
            ViewBag.userstatus = new SelectList(
               new List<SelectListItem>
               {
                    new SelectListItem{Text="Active",Value="1"},
                    new SelectListItem{Text="Deactive",Value="0"}
               }, "Value", "Text");
            ViewBag.role = new SelectList(objbllrole.get_role(), "role_id", "user_role");
        }
        public ActionResult adduser()
        {
            user_active_deactive_code();
            return View();
        }
       
        public ActionResult userlist()
        {
            user_active_deactive_code();
            return View();
        }
        public JsonResult get_user_list()
        {
            var user = objblluser.get_user_list();
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult full_detail_of_user(int id)
        {
          
            var result=objblluser.full_detail_of_user(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult insert_user(Custom_user_role cus_user_role_img)
        {
           
            objblluser.insert_user(cus_user_role_img);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete_user(int id)
        {
            objblluser.delete_user(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult update_user(Custom_user_role cus_user_role_img)
        {
            objblluser.update_user(cus_user_role_img);
            return Json("", JsonRequestBehavior.AllowGet);
        }
       
    }
}