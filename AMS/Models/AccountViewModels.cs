using AMS.Models.Requests.Deduction;
using AMS.Models.Requests.FileUpload;
using AMS.Models.Requests.Incentive;
using AMS.Models.Requests.Tax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Address { get; set; }
        public int? Designation { get; set; }
        public int? DepartmentId { get; set; }
        public Position Position { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
        public bool? HasSupervisor { get; set; }
        public int? SupervisorId { get; set; }
        public int RemainingLeaves { get; set; }
        public int AnnualLeaves { get; set; }
        public int MedicalLeaves { get; set; }
        public int ShiftId { get; set; }
        public decimal Salary { get; set; }
        public List<Document> Docs{ get; set; }
        public List<Document> EducationalDocs { get; set; }
        public List<Document> Certificates { get; set; }
        public List<GetTaxResponse> Taxes { get; set; }
        public List<GetDeductionResponse> Deductions { get; set; }
        public List<GetIncentiveResponse> Incentives { get; set; }
        public string UserId{ get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class RegisterUserResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool IsRoleAdded { get; set; }
    }
    public class Position {
        public int Id { get; set; }
        public int DeptId{ get; set; }
        public string DeptName{ get; set; }
        public int PositionOrder { get; set; }
        public string PositionName { get; set; }
        public string JobDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Department{ get; set; }
    }
}
