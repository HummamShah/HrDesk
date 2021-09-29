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
    
    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.Agent1 = new HashSet<Agent>();
            this.AgentAttendance = new HashSet<AgentAttendance>();
            this.Documents = new HashSet<Documents>();
            this.Documents1 = new HashSet<Documents>();
            this.Leaves = new HashSet<Leaves>();
            this.Pay = new HashSet<Pay>();
        }
    
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Address { get; set; }
        public Nullable<int> Designation { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public int ShiftId { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> SuperVisorId { get; set; }
        public Nullable<bool> IsSupervisor { get; set; }
        public Nullable<bool> HasSupervisor { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int DeductionInDays { get; set; }
        public int ConsecutiveLateCounter { get; set; }
        public int RemainingLeaves { get; set; }
        public int AnnualLeaves { get; set; }
        public int MedicalLeaves { get; set; }
        public string FirstInterviewBy { get; set; }
        public string SecondInterviewBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Remarks { get; set; }
        public string Gender { get; set; }
        public string ApprovalImageUrl { get; set; }
        public string DocumentsImageUrl { get; set; }
        public string CompanyBelonging { get; set; }
        public string ExitEmployeeFormImageUrl { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agent> Agent1 { get; set; }
        public virtual Agent Agent2 { get; set; }
        public virtual Shifts Shifts { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentAttendance> AgentAttendance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documents> Documents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documents> Documents1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leaves> Leaves { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pay> Pay { get; set; }
    }
}
