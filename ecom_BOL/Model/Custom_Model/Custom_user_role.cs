using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecom_BOL.Model.Custom_Model
{
     public class Custom_user_role
    {
        public user_tbl user { get; set; }
        public user_img_tbl user_image { get; set; }
        public userrole_tbl user_role { get; set; }
    }
}
