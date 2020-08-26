using ecom_BLL.Shop_Bll;
using ecom_BOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class ContactController : Controller
    {
        private contact_bll objbllcon;
        public ContactController()
        {
            objbllcon = new contact_bll();

        }
        public void msgstatus()
        {
            ViewBag.msgstatus = new SelectList
                (
                new List<SelectListItem>
                {
                    new SelectListItem{Text="Unreplied",Value="Unreplied" },
                    new SelectListItem{Text="Replied" , Value="Replied" }
                }, "Value", "Text");

        }
        public ActionResult contact()
        {
            return View();
        }
        public ActionResult contactlist()
        {
            msgstatus();
            return View();
        }
        [HttpPost]
        public JsonResult savecontact(contact contact)
        {
            objbllcon.savemsg(contact);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult showmsg()
        {
            var result = objbllcon.getcontact();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getconbyid(int id)
        {
            var result = objbllcon.getconbyid(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult update(contact con)
        {
            objbllcon.updatecon(con);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}