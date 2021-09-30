using AMS.Models.Requests.FileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.User
{
    public class EditUserResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool IsRoleAdded { get; set; }
        public bool Success { get; set; }
    }
    public class EditUserRequest
    {
        public int AgentId { get; set; }
        public string UpdatedBy { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Email { get; set; }
        public bool? HasSupervisor { get; set; }
        public int? SupervisorId { get; set; }
        public int RemainingLeaves { get; set; }
        public int MedicalLeaves { get; set; }
        public int AnnualLeaves { get; set; }
        public int ShiftId { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
        public List<Document> Docs { get; set; }
        public List<Document> EducationalDocs { get; set; }
        public List<Document> Certificates { get; set; }

    }
}