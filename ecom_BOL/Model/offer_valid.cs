using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ecom_BOL.Model
{
    public class offer_valid
    {
    }
    [MetadataTypeAttribute(typeof(offer_valid))]
    public partial class offer
    {
        public HttpPostedFileBase offerimg { get; set; }
    }
    }
