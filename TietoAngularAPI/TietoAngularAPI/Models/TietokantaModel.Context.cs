﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TietoAngularAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JohaMeriSQL1Entities : DbContext
    {
        public JohaMeriSQL1Entities()
            : base("name=JohaMeriSQL1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Henkilot> Henkilot { get; set; }
        public virtual DbSet<Projektit> Projektit { get; set; }
        public virtual DbSet<Tunnit> Tunnit { get; set; }
    }
}
