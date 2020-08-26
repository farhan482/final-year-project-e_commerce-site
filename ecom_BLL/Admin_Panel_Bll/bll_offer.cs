using ecom_BOL.Model;
using ecom_DAL.Repository_Pattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BLL.Admin_Panel_Bll
{
    public class bll_offer
    {
        private DAL_IRepository_Pattern<offer> objoffer;
        public bll_offer()
        {
            objoffer = new DAL_Repository_Pattern<offer>();
        }
        public void addoffer(offer ofr)
        {
            if (ofr!=null)
            {
                string filename = Path.GetFileNameWithoutExtension(ofr.offerimg.FileName);
                string exten = Path.GetExtension(ofr.offerimg.FileName);
                filename = filename + Guid.NewGuid() + exten;
                string path = "/images/offerimage/";
                path = path + filename;
                ofr.image_path = path;
                filename = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("/images/offerimage/"+ filename ));
                ofr.offerimg.SaveAs(filename);
                objoffer.insertmodel(ofr);
                
            }
        }
       public IEnumerable<offer> getoffer()
        {
            var result = objoffer.getmodel();
            return result;
        }
        public void updateoffer(offer ofr)
        {
            if (ofr!=null)
            {
                if (ofr.offerimg!=null)
                {
                    string filename = Path.GetFileNameWithoutExtension(ofr.offerimg.FileName);
                    string exten = Path.GetExtension(ofr.offerimg.FileName);
                    filename = filename + Guid.NewGuid() + exten;
                    string path = "/images/offerimage/";
                    path = path + filename;
                    ofr.image_path = path;
                    filename = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("/images/offerimage/" + filename));
                    ofr.offerimg.SaveAs(filename);
                }
              
                objoffer.updatemodel(ofr);
            }
            
        }
        public void deleteoffer(int? id)
        {
            if (id != null)
            {
                objoffer.deletemodel(Convert.ToInt32(id));
            }
           
        }
        public dynamic getofferbyid(int? id)
        {
          var result=objoffer.getmodelbyid(Convert.ToInt32(id));
          return result;
        }
    }
}
