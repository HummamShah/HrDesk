﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMS.Model.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AMSEntities : DbContext
    {
        public AMSEntities()
            : base("name=AMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Leaves> Leaves { get; set; }
        public virtual DbSet<Pay> Pay { get; set; }
        public virtual DbSet<LeaveDates> LeaveDates { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Shifts> Shifts { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentAttendance> AgentAttendance { get; set; }
    }
}
