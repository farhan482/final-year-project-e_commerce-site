using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class RoleController : Controller
    {
        private bll_role objbllrole;
        public RoleController()
        {
            objbllrole = new bll_role();
        }
        public ActionResult addrole()
        {
            return View();
        }
       public ActionResult rolelist()
        {
            return View();
        }
        
        public JsonResult get_role()
        {
            var result = objbllrole.get_role();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult insert_role(C_role_tbl role)
        {
            objbllrole.insert_role(role);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult update_role(C_role_tbl role)
        {
            objbllrole.update_role(role);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult delete_role(int id)
        {
            objbllrole.delete_role(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}