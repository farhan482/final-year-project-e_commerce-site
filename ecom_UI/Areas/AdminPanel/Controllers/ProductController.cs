using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Unity.Injection;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class ProductController : Controller
    {
        private bll_product objproduct;
        private bll_category objcategory;
        private bll_sub_cate objsubcate;
       
        public ProductController()
        {
            objproduct = new bll_product();
            objcategory = new bll_category();
            objsubcate = new bll_sub_cate();

        }
        public void product_active_deactive_code_category_get_code()
        {
            ViewBag.status = new SelectList(
              new List<SelectListItem>
              {
                new SelectListItem{Text="Active",Value="1"},
                new SelectListItem{Text="Deactive",Value="0" },

              }, "Value", "Text");
            var sub_category = objsubcate.get_sub_cate().ToList();
            ViewBag.category = new SelectList(sub_category, "sub_cate_id", "sub_category_name");
               
        }
        public ActionResult Create_Product()
        {
            product_active_deactive_code_category_get_code();
            return View();
        }
        [HttpPost]
        public JsonResult Create_Product(product_and_image producttbl)
        {
            product_active_deactive_code_category_get_code();
            if (ModelState.IsValid)
            {
               
               objproduct.create_product(producttbl);
            }
           
            return Json("",JsonRequestBehavior.AllowGet);
        }
        public ActionResult get_product()
        {
            product_active_deactive_code_category_get_code();       
            return View();
        }

        public JsonResult get_product_details()
        {
            var result = objproduct.get_product();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult get_product_detail_by_id(int id)
        {
            var result = objproduct.get_product_detail_by_id(id);
           
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult updateproduct(product_and_image updateproduct, List<int> id)
        {
           
              objproduct.updateproduct(updateproduct,id);
        
            return Json("",JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult deleteproduct(int id)
        {
            objproduct.deleteproduct(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}