using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BOL.Model.Custom_Model
{
   public class product_and_image
    {
        public product_tbl product { get; set; }
        public product_img productimage { get; set; }
        public sub_category_tbl sub_category { get; set; }
    }
}
