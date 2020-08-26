using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ecom_BOL.Model
{
    public class user_img_valid
    {
    }
    [MetadataTypeAttribute(typeof(user_img_valid))]
    public partial class user_img_tbl
    {
        public HttpPostedFileBase userimg { get; set; }
    }
    }
