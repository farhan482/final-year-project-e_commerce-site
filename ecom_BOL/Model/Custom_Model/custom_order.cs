using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BOL.Model.Custom_Model
{
    public class custom_order
    {
        public order_product_detaills_tbl order_product { get; set; }
        public order_amount_details_tbl order_amt { get; set; }
        public order_address_details_tbl order_addrss { get; set; }
        public product_tbl product { get; set; }
    }
}
