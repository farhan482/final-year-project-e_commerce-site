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
    
    public partial class userrole_tbl
    {
        public int userrole_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> role_id { get; set; }
    
        public virtual C_role_tbl C_role_tbl { get; set; }
        public virtual user_tbl user_tbl { get; set; }
    }
}
