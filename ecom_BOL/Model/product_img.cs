//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ecom_BOL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class product_img
    {
        public int image_id { get; set; }
        public string image_path { get; set; }
        public Nullable<int> p_id { get; set; }
    
        public virtual product_tbl product_tbl { get; set; }
    }
}
