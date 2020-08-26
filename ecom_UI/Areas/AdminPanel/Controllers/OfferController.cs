using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class OfferController : Controller
    {
        private bll_offer objblloffer;
        public OfferController()
        {
            objblloffer = new bll_offer();
        }
        public ActionResult offer()
        {
            return View();
        }
       
        public ActionResult offerlist()
        {
            return View();
        }
        [HttpPost]
        public JsonResult addoffer(offer ofr)
        {
            objblloffer.addoffer(ofr);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult updateoffer(offer ofr)
        {
          
            objblloffer.updateoffer(ofr);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult deleteoffer(int id)
        {
            objblloffer.deleteoffer(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult selectoffer()
        {
            var result = objblloffer.getoffer().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getofferbyid(int id)
        {
            var result = objblloffer.getofferbyid(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}