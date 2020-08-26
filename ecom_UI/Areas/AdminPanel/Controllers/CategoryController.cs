using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class CategoryController : Controller
    {
        private bll_category objcategory;
        public CategoryController()
        {
            objcategory = new bll_category();
        }
        public ActionResult get_cate()
        {
            return View();
        }
        public void get_parent_category()
        {
            var result = objcategory.getcategory();
            ViewBag.parent_category = new SelectList(result,"category_id","category_name");
        }
       
       
        public JsonResult get_category()
        {
            
            var category = objcategory.getcategory();
            
            return Json(category, JsonRequestBehavior.AllowGet);
           
        }
       
       
        public ActionResult add_categoy()
        {
            return View();
        }
       
      
        [HttpPost]
        public JsonResult create_category(category_tbl catename)
        {
            objcategory.create_category(catename);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult delete_category(int id)
        {
            objcategory.delete_category(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult update_category(category_tbl catename)
        {
         
            objcategory.update_category(catename);
            return Json("", JsonRequestBehavior.AllowGet);
        }
       
       
    }
}