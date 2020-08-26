using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecom_UI.Areas.AdminPanel.Controllers
{
    public class Sub_CategoryController : Controller
    {
        private bll_category objcate;
        private bll_sub_cate objsubcate;
        public Sub_CategoryController()
        {
            objcate = new bll_category();
            objsubcate = new bll_sub_cate();
        }
        public void get_parent_category()
        {
            var result = objcate.getcategory();
            ViewBag.parent_category = new SelectList(result, "category_id", "category_name");
        }
        public ActionResult sub_cate_view()
        {
            get_parent_category();
            return View();
        }
        public ActionResult sub_cate_list_view()
        {
            get_parent_category();
            return View();
        }
        public JsonResult get_sub_category_by_id(int id)
        {
            get_parent_category();
            var sub_category = objsubcate.get_sub_category_by_id(id);

            return Json(sub_category, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult create_sub_category(sub_category_tbl sub_catename)
        {

            objsubcate.create_sub_category(sub_catename);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_sub_cate_list()
        {
            var result = objsubcate.get_sub_cate_list_with_join();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult update_sub_category(sub_category_tbl sub_catename)
        {

            objsubcate.update_sub_category(sub_catename);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult delete_sub_category(int id)
        {

            objsubcate.delete_sub_category(id);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}