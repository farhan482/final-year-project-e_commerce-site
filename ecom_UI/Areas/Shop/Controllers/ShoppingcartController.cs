using ecom_BLL.Admin_Panel_Bll;
using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecom_UI.Areas.Shop.Controllers
{
    public class ShoppingcartController : Controller
    {
        private bll_product objbllproduct;
        private List<order_product_detaills_tbl> cartlist;
        private order_product_detaills_tbl cartpro;
        private bll_order objbllorder;
        public ShoppingcartController()
        {
            objbllproduct = new bll_product();
            cartlist = new List<order_product_detaills_tbl>();
            cartpro = new order_product_detaills_tbl();
            objbllorder = new bll_order();
        }
        public ActionResult cart()
        {
            
            return View();
        }
       [Authorize]

        public ActionResult checkout()
        {
         
            return View();
        }
        public JsonResult cartitem()
        {
            var data = Session["cartitem"];
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult findproduct(int id,int? qty)
        {
            if (User.Identity.IsAuthenticated)
            {
                var product = objbllproduct.getproductbyid(id);
                product = product[0];
                if (Session["cartcounter"] != null)
                {
                    cartlist = Session["cartitem"] as List<order_product_detaills_tbl>;
                }
               
                if (cartlist.Any(x => x.p_id == id))
                {
                    cartpro = cartlist.Single(x => x.p_id == id);
                    if (qty != null)
                    {
                        cartpro.qty = qty;
                        cartpro.total = cartpro.qty * cartpro.rate;
                    }
                    else
                    {
                        cartpro.qty += 1;
                        cartpro.total = cartpro.qty * cartpro.rate;
                    }
                    
                }
                else
                {
                    cartpro.p_id = product.product.p_id;
                    cartpro.productname = product.product.product_name;
                    cartpro.proimg_path = product.productimage.image_path;
                    if (qty != null)
                    {
                        cartpro.qty = qty;
                        cartpro.rate = product.product.net_price;
                        cartpro.total = product.product.net_price * cartpro.qty;
                        cartlist.Add(cartpro);
                    }
                    else
                    {
                        cartpro.qty = 1;
                        cartpro.rate = product.product.net_price;
                        cartpro.total = product.product.net_price * cartpro.qty;
                        cartlist.Add(cartpro);
                    }
                   
                }
                Session["cartcounter"] = cartlist.Count;
                Session["cartitem"] = cartlist;
                return Json(new { success = true, counter = cartlist.Count }, JsonRequestBehavior.AllowGet);
            }
            else
            {
             
                return Json("logininvalid", JsonRequestBehavior.AllowGet);
            }
          
        }
        public JsonResult loadcartitem()
        {
            if (Session["cartcounter"] != null)
            {
                cartlist = Session["cartitem"] as List<order_product_detaills_tbl>;
            }

            Session["cartcounter"] = cartlist.Count;
            Session["cartitem"] = cartlist;
            return Json(new { success = true, counter = cartlist.Count,item=cartlist }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult updateqty(int id,string at)
        {

            if (Session["cartcounter"] != null)
            {
                cartlist = Session["cartitem"] as List<order_product_detaills_tbl>;
            }
            if (cartlist.Any(x => x.p_id == id))
            {
                cartpro = cartlist.Single(x => x.p_id == id);
                if (at == "cart_quantity_down")
                {
                    if (cartpro.qty > 1)
                    {
                        cartpro = cartlist.Single(x => x.p_id == id);
                        cartpro.qty -= 1;
                        cartpro.total = cartpro.qty * cartpro.rate;
                        
                    }
                }
                else
                {
                   
                        cartpro = cartlist.Single(x => x.p_id == id);
                        cartpro.qty += 1;
                        cartpro.total = cartpro.qty * cartpro.rate;
                    

                }
            }
          
            Session["cartitem"] = cartlist;
            return Json(new { success = true, item = cartlist }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult deleteproduct(int id)
        {
            if (Session["cartcounter"] != null)
            {
                cartlist = Session["cartitem"] as List<order_product_detaills_tbl>;
            }
            if (cartlist.Any(x => x.p_id == id))
            {
                cartpro = cartlist.FirstOrDefault(x=>x.p_id==id);
                cartlist.Remove(cartpro);
            }
            return Json(cartlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ordersave(custom_order order)
            {
            if (Session["cartcounter"] != null)
            {
                cartlist = Session["cartitem"] as List<order_product_detaills_tbl>;
                objbllorder.saveorder(order,cartlist);
                Session["cartcounter"] = null;
            }
     
            return Json("null", JsonRequestBehavior.AllowGet);
        }
    }
}