﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ecommerce_site_version_v_19_0Entities : DbContext
    {
        public ecommerce_site_version_v_19_0Entities()
            : base("name=ecommerce_site_version_v_19_0Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C_role_tbl> C_role_tbl { get; set; }
        public virtual DbSet<category_tbl> category_tbl { get; set; }
        public virtual DbSet<order_address_details_tbl> order_address_details_tbl { get; set; }
        public virtual DbSet<order_amount_details_tbl> order_amount_details_tbl { get; set; }
        public virtual DbSet<order_product_detaills_tbl> order_product_detaills_tbl { get; set; }
        public virtual DbSet<product_img> product_img { get; set; }
        public virtual DbSet<product_tbl> product_tbl { get; set; }
        public virtual DbSet<sub_category_tbl> sub_category_tbl { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<user_img_tbl> user_img_tbl { get; set; }
        public virtual DbSet<user_tbl> user_tbl { get; set; }
        public virtual DbSet<userrole_tbl> userrole_tbl { get; set; }
        public virtual DbSet<offer> offers { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
    }
}
