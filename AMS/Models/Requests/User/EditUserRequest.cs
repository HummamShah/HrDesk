using AMS.Models.Requests.FileUpload;
using AMS.Models.Requests.Incentive;
using AMS.Models.Requests.Tax;
using System;
using System.Collections.Generic;

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

        public string EmpCode { get; set; }
        public string Location { get; set; }
        public string Section { get; set; }
        public string Grade { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string WWID { get; set; }
        public string EmployementType { get; set; }
        public string PaidBy { get; set; }
        public string CNIC { get; set; }
        public int AgentId { get; set; }
        public string UpdatedBy { get; set; }
        public int DepartmentId { get; set; }
        public string PositionName { get; set; }
        public Position Position { get; set; }
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
        public decimal Salary { get; set; }
        public decimal HouseRentAllowance { get; set; }
        public decimal Utility { get; set; }
        public decimal VehicleAllowance { get; set; }
        public decimal FuelAllowance { get; set; }
        public decimal EOBIDeduction { get; set; }
        public decimal LoanDeduction { get; set; }
        public decimal OtherDeduction { get; set; }
        public string OtherDeductionDesc { get; set; }
        public List<Document> Docs { get; set; }
        public List<Document> EducationalDocs { get; set; }
        public List<Document> Certificates { get; set; }
        public List<GetTaxResponse> Taxes{ get; set; }
        public List<GetTaxResponse> Deductions{ get; set; }
        public List<GetIncentiveResponse> Incentives { get; set; }
    }
}