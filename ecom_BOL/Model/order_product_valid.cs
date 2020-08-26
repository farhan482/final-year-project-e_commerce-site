using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BOL.Model
{
    public class order_product_valid
    {
    }
    [MetadataTypeAttribute(typeof(order_product_valid))]
    public partial class order_product_detaills_tbl
    {
        public string productname { get; set; }
        public string proimg_path { get; set; }
    }
    }
