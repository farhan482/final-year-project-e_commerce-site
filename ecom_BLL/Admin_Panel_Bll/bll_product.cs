using ecom_BOL.Model;
using ecom_BOL.Model.Custom_Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ecom_BOL;
using System.Collections;
using System.Security.Cryptography;
using System.Data;
using System.Web.ModelBinding;
using System.Data.SqlClient;
using System.Transactions;
using System.Data.Entity;
using System.Web;

namespace ecom_BLL.Admin_Panel_Bll
{

    public class bll_product
    {
        private DAL_IRepository_Pattern<product_tbl> objproduct;
        private DAL_IRepository_Pattern<product_img> objproductimg;
        private DAL_IRepository_Pattern<sub_category_tbl> objsubcategory;
        private product_and_image cusproimg;
        private List<string> img;


        public bll_product()
        {
            objproduct = new DAL_Repository_Pattern<product_tbl>();
            objproductimg = new DAL_Repository_Pattern<product_img>();
            objsubcategory = new DAL_Repository_Pattern<sub_category_tbl>();
            cusproimg = new product_and_image();
            img = new List<string>();

        }
        public void create_product(product_and_image product)
        {
            product_tbl pro = product.product;
            sub_category_tbl sub_cate = product.sub_category;
          
            pro.sub_category_id = sub_cate.sub_cate_id;
            objproduct.insertmodel(pro);
            product_img proimg = product.productimage;
          
            proimg.p_id = pro.p_id;
            foreach (var item in proimg.img)
            {
               
                    string filename = Path.GetFileNameWithoutExtension(item.FileName);
                    string exten = Path.GetExtension(item.FileName);
                    filename = filename + Guid.NewGuid() + exten;
                    string path = "/productimages/" + filename;
                    product.productimage.image_path = path;
                    filename = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("/productimages/"), filename);
                    item.SaveAs(filename);
                    objproductimg.insertmodel(proimg);
             
            }
          
        }
        public IEnumerable<product_tbl> get_product()
        {
            var result =from product in objproduct.getmodel().ToList() orderby product.p_id descending select product;
            return result;
        }
        public dynamic getallinfoproduct()
        {
            var result = objproduct.Get("product_img");
            
            return result;
        }
        public dynamic getproductbyid(int id)
        {
            var pro = (from prod in objproduct.getmodel()
                           join proimg in objproductimg.getmodel() on prod.p_id
                           equals proimg.p_id
                           where prod.p_id == id
                           select new product_and_image
                           {
                              product=prod,
                              productimage=proimg

                           }).ToList();
            return pro;
        }
        public dynamic get_product_detail_by_id(int id)
        {
            var result = (from product in objproduct.getmodel() join proimg in objproductimg.getmodel() on product.p_id
                         equals proimg.p_id join cate in objsubcategory.getmodel() on product.sub_category_id equals cate.sub_cate_id where product.p_id==id select new
                         {
                          product.p_id,
                          product.product_name,
                          product.product_description,
                          product.brand_manfacturer,
                          product.price,
                          product.discount_percentage,
                          product.discounted_price,
                          product.net_price,
                          product.status,
                          cate.sub_cate_id,
                          cate.sub_category_name,
                          proimg.image_path,
                          proimg.image_id

                         }).ToList();
            if (result.Count == 0)
            {
                var result2 = (from product in objproduct.getmodel() join cate in objsubcategory.getmodel() on product.sub_category_id equals cate.sub_cate_id
                         where product.p_id == id
                         select new
                         {
                             product.p_id,
                             product.product_name,
                             product.product_description,
                             product.brand_manfacturer,
                             product.price,
                             product.discount_percentage,
                             product.discounted_price,
                             product.net_price,
                             product.status,
                             cate.sub_cate_id,
                             cate.sub_category_name
                           

                         }).ToList();
                return result2;
            }
            return result;
        }
        public dynamic get_product_detail_by_cate_id(int id)
        {
            var result = (from product in objproduct.getmodel()
                          join proimg in objproductimg.getmodel() on product.p_id
equals proimg.p_id
                          join cate in objsubcategory.getmodel() on product.sub_category_id equals cate.sub_cate_id
                          where product.sub_category_id == id
                          select new
                          {
                              product.p_id,
                              product.product_name,
                              product.product_description,
                              product.brand_manfacturer,
                              product.price,
                              product.discount_percentage,
                              product.discounted_price,
                              product.net_price,
                              product.status,
                              cate.sub_cate_id,
                              cate.sub_category_name,
                              proimg.image_path,
                              proimg.image_id

                          }).ToList();
            if (result.Count == 0)
            {
                var result2 = (from product in objproduct.getmodel()
                               join cate in objsubcategory.getmodel() on product.sub_category_id equals cate.sub_cate_id
                               where product.p_id == id
                               select new
                               {
                                   product.p_id,
                                   product.product_name,
                                   product.product_description,
                                   product.brand_manfacturer,
                                   product.price,
                                   product.discount_percentage,
                                   product.discounted_price,
                                   product.net_price,
                                   product.status,
                                   cate.sub_cate_id,
                                   cate.sub_category_name


                               }).ToList();
                return result2;
            }
            return result;
        }
        public void updateproduct(product_and_image updateproduct, List<int> id)
        {

            product_tbl product = updateproduct.product;
            product_img proimg = updateproduct.productimage;
           
            if (product != null)
            {
                objproduct.updatemodel(product);
            }
            if(proimg!=null)
            {
                foreach (var item in proimg.img)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.FileName);
                    string exten = Path.GetExtension(item.FileName);
                    filename = filename + Guid.NewGuid() + exten;
                    string path = "/productimages/" + filename;
                    proimg.image_path = path;
                    filename = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("/productimages/" + filename));
                    item.SaveAs(filename);
                    objproductimg.insertmodel(proimg);
                }
               
            }
            if(id != null)
            {
                foreach (var item in id)
                {
                    objproductimg.deletemodel(item);
                }
            }

        }
        public void deleteproduct(int? id)
        {
            if(id !=null)
            {
                objproduct.deletemodel(Convert.ToInt32(id));
            }
            
        }

    }
}
