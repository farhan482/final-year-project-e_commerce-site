using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ecom_BOL.Model
{
    public class product_img_valid
    {
    }
    [MetadataTypeAttribute(typeof(product_img_valid))]
    public partial class product_img
    {
        public List<HttpPostedFileBase> img { get; set; }
    }
    }
