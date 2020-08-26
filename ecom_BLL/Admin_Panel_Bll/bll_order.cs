using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BLL.Admin_Panel_Bll
{
    public class bll_order
    {
        private DAL_IRepository_Pattern<order_product_detaills_tbl> oobjproduct;
        private DAL_IRepository_Pattern<order_amount_details_tbl> objamount;
        private DAL_IRepository_Pattern<order_address_details_tbl> objaddrs;
        private DAL_IRepository_Pattern<product_tbl> objproduct;
        public bll_order()
        {
            oobjproduct = new DAL_Repository_Pattern<order_product_detaills_tbl>();
            objamount = new DAL_Repository_Pattern<order_amount_details_tbl>();
            objaddrs = new DAL_Repository_Pattern<order_address_details_tbl>();
            objproduct = new DAL_Repository_Pattern<product_tbl>();
        }
        public void saveorder(custom_order order,dynamic cartlist)
        {
            order_address_details_tbl oaddress = order.order_addrss;
            order_amount_details_tbl oamt = order.order_amt;
          
            if (oamt!=null && oaddress != null)
            {
                oamt.order_date_and_time=DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                oamt.order_status ="Pending...";
                objamount.insertmodel(oamt);
                objaddrs.insertmodel(oaddress);
             
                foreach (var item in cartlist)
                {
                   
                    order.order_product = item;
                    order_product_detaills_tbl opro = order.order_product;
                    opro.order_no = oamt.order_no;
                    opro.o_addrs_id = oaddress.o_addrs_id;
                    oobjproduct.insertmodel(opro);
                }
               
            }
        }
        public dynamic get_order_by_id(int? id)
        {
            if (id != null)
            {
                var result = (from product in objproduct.getmodel()
                              join orderpro in oobjproduct.getmodel()
                              on product.p_id equals orderpro.p_id
                              join orderamt in objamount.getmodel()
                              on orderpro.order_no equals orderamt.order_no
                              join orderaddrs in objaddrs.getmodel()
                              on orderpro.o_addrs_id equals orderaddrs.o_addrs_id
                              where orderamt.order_no == id
                              select new
                              {
                                  orderamt.order_no,
                                  orderamt.order_date_and_time,
                                  product.product_name,
                                  orderpro.qty,
                                  orderpro.rate,
                                  orderpro.total,
                               
                                  orderamt.net_total,
                                  orderamt.payment_type,
                                  orderamt.order_status,
                                  orderaddrs.country,
                                  orderaddrs.city,
                                  orderaddrs.address,
                                  orderaddrs.zip_or_postal_code,
                                  orderaddrs.order_person_name,
                                  orderaddrs.contact_no,
                                  orderaddrs.o_addrs_id
                              }).ToList();


                return result;
            }
            return "";
        }
        public dynamic get_order()
        {
            var result = (from order in objamount.getmodel() orderby order.order_no descending select order).ToList();


            return result;
        }
        public void order_update(order_amount_details_tbl order)
        {
            if (order!=null)
            {
                objamount.updatemodel(order);
            }

           
        }
        public void delete_order(int? id)
        {
            if (id!=null)
            {
                objamount.deletemodel(Convert.ToInt32(id));
            }
        }
        public void delete_addrs(int? id)
        {
            if (id != null)
            {
                objaddrs.deletemodel(Convert.ToInt32(id));
            }
        }
    }
}