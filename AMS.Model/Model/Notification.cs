//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Notification
    {
        public int Id { get; set; }
        public Nullable<int> AgentId { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public bool IsRead { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    }
}
