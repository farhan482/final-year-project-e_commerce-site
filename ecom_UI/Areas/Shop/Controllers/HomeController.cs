using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Sql;
using ecom_BLL.Shop_Bll;

namespace ecom_UI.Areas.Shop.Controllers
{
    public class HomeController : Controller
    {
        private bll_sub_cate objbllsubcate;
        private bll_category objbllcate;
        private bll_product objbllproduct;
        private bll_offer objblloffer;
      
        
        public HomeController()
        {
            objbllsubcate = new bll_sub_cate();
            objbllcate = new bll_category();
            objbllproduct = new bll_product();
            objblloffer = new bll_offer();
           
        }
        public ActionResult Home()
        {
           ViewBag.category = objbllsubcate.get_sub_cate_list_with_join2();
           ViewBag.product =objbllproduct.getallinfoproduct();
           ViewBag.offer = objblloffer.getoffer();
            return View();
        }
       
        public ActionResult About()
        {
           
            return View();
        }
      
        [HttpPost]
        public JsonResult searchprobycate(int id)
        {
            var result = objbllproduct.get_product_detail_by_cate_id(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       

    }
}