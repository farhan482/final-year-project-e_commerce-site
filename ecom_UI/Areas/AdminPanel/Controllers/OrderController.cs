using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class OrderController : Controller
    {
        private bll_order objbllorder;
        public OrderController()
        {
            objbllorder = new bll_order();
        }
        public void orderstatus()
        {
            ViewBag.orderstatus = new SelectList
                (
                new List<SelectListItem>
                {
                    new SelectListItem{Text="Pending...",Value="Pending..." },
                    new SelectListItem{Text="Confirmed" , Value="Confirmed" }
                }, "Value","Text");
                
        }
        public ActionResult order()
        {
            orderstatus();
            return View();
        }
        
        public JsonResult get_order()
        {
            orderstatus();
            var result = objbllorder.get_order();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult get_order_by_id(int id)
        {
            orderstatus();
            var result = objbllorder.get_order_by_id(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult order_update(order_amount_details_tbl order)
        {

            objbllorder.order_update(order);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult delete_order(int id,int addrsid)
        {

            objbllorder.delete_order(id);
            objbllorder.delete_addrs(addrsid);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}