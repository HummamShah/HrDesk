using AMS.Model.Enum;
using AMS.Model.Model;
using AMS.Model.Requests.User;
using AMS.Models;
using AMS.Models.Enums;
using AMS.Models.Requests.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AMS.Models.Requests.FileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AMS.Models.Requests.Tax;

namespace AMS.Controllers.Api
{
    public class UserApiController : ApiController
    {
        private ApplicationUserManager _userManager;
        public UserApiController()
        {
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        }
        private AMSEntities db = new AMSEntities();
        [System.Web.Http.Authorize(Roles = "SuperAdmin,Admin,HR")]
        [System.Web.Http.HttpPost]
        public object GetListData(GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetAgentsForAssignment([FromUri] GetAgentsForAssignmentRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetUsersByDepartment([FromUri] GetUsersByDepartmentRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [System.Web.Http.HttpGet]
        public object GetUser([FromUri] GetUserRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetUserIdentity([FromUri] UserIdentityRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [System.Web.Http.HttpGet]
        public object GetAllUsersDropdown([FromUri] GetAllUsersDropdownRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [System.Web.Http.HttpPost]
        public object EditAdmin([FromBody] EditAdminRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        [System.Web.Http.HttpPost]
        public object Terminate([FromBody] TerminateUserRequest req)
        {
            req.UpdatedBy = User.Identity.Name;
            var result = req.RunRequest(req);
            return result;
        }

        // GET api/<controller>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize(Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<object> RegisterAdmin(RegisterUserViewModel model)
        {
            var response = new RegisterUserResponse();
            var RolesToBeAdded = new List<string>();
            if (ModelState.IsValid && model.ConfirmPassword == model.Password)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email }; //We can put username field instead of email
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var CurrentUserName = User.Identity.Name;
                    var AgentData = new Agent();
                    AgentData.UserId = user.Id;
                    AgentData.Designation = (int)Designation.HR;// model.Designation;
                    AgentData.FisrtName = model.FirstName;
                    AgentData.LastName = model.LastName;
                    AgentData.Address = model.Address;
                    AgentData.Contact1 = model.Contact1;
                    AgentData.Contact2 = model.Contact2;
                    AgentData.CreatedAt = DateTime.Now;
                    AgentData.CreatedBy = CurrentUserName;
                    AgentData.Email = model.Email;
                    AgentData.ImageUrl = model.ImageUrl;
                    var AgentResult = db.Agent.Add(AgentData);
                    var RoleResult = await _userManager.AddToRoleAsync(user.Id, Roles.HR);
                    if (RoleResult.Succeeded)
                    {
                        response.IsRoleAdded = true;
                    }
                    else
                    {
                        response.IsRoleAdded = false;
                    }

                    db.SaveChanges();
                    response.Success = true;
                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return response;
                }
                if (model.ConfirmPassword != model.Password)
                {
                    response.ValidationErrors.ToList().Add("Password and confirm password does not match");
                }
                response.Success = false;
                response.ValidationErrors = (result.Errors);
            }

            // If we got this far, something failed, redisplay form
            return response;
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize(Roles = "SuperAdmin,HR")]
        [ValidateAntiForgeryToken]
        public async Task<object> RegisterUser(RegisterUserViewModel model)
        {
            var response = new RegisterUserResponse();
            var RolesToBeAdded = new List<string>();
            /*if (model.UserId == null)
            {
                var User = db.AspNetUsers.Where(x => x.Email == model.Email).FirstOrDefault();
                var RemovePassword = _userManager.RemovePassword(User.Id);
                var AddNewPassword = await _userManager.AddPasswordAsync(User.Id, "Password123$");
                response.Success = true;
            }*/
            if (ModelState.IsValid && model.ConfirmPassword == model.Password)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email }; //We can put username field instead of email
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    model.UserId = User.Identity.GetUserId();
                    var CurrentUserAgentId = db.Agent.Where(x => x.UserId == model.UserId).FirstOrDefault().Id;
                    var CurrentUserName = User.Identity.Name;
                    var AgentData = new Agent();
                    AgentData.UserId = user.Id;
                    AgentData.IsActive = true;
                    AgentData.FisrtName = model.FirstName;
                    AgentData.LastName = model.LastName;
                    AgentData.Address = model.Address;
                    AgentData.Contact1 = model.Contact1;
                    AgentData.Contact2 = model.Contact2;
                    AgentData.Email = model.Email;
                    AgentData.CreatedAt = DateTime.Now;
                    AgentData.CreatedBy = CurrentUserName;
                    AgentData.ImageUrl = model.ImageUrl;
                    AgentData.RemainingLeaves = model.RemainingLeaves;
                    AgentData.MedicalLeaves = model.MedicalLeaves;
                    AgentData.AnnualLeaves = model.AnnualLeaves;
                    AgentData.Gender = model.Gender;
                    AgentData.DepartmentId = model.DepartmentId;
                    AgentData.PositionName = "Employee";
                    AgentData.JobDescription = "Just Employee";
                    AgentData.ShiftId = model.ShiftId;
                    AgentData.Salary = model.Salary;
                    if (model.HasSupervisor.HasValue)
                    {
                        AgentData.HasSupervisor = model.HasSupervisor;
                        AgentData.SuperVisorId = model.SupervisorId;
                    }
                    var AgentResult = db.Agent.Add(AgentData);
                    db.SaveChanges();

                    if (model.Taxes.Count > 0)
                    {
                        foreach (var tax in model.Taxes)
                        {
                            var agentTax = new AgentTaxes();
                            agentTax.AgentId = AgentData.Id;
                            agentTax.AgentName = AgentData.FisrtName + " " + AgentData.LastName;
                            agentTax.TaxId = tax.Id;
                            agentTax.TaxName = tax.Name;
                            db.AgentTaxes.Add(agentTax);
                            db.SaveChanges();
                        }
                    }
                    if (model.Deductions.Count > 0)
                    {
                        foreach (var deduction in model.Deductions)
                        {
                            var agentDeduction = new AgentDeductions();
                            agentDeduction.AgentId = AgentData.Id;
                            agentDeduction.AgentName = AgentData.FisrtName + " " + AgentData.LastName;
                            agentDeduction.DeductionId = deduction.Id;
                            agentDeduction.DeductionName = deduction.Name;
                            db.AgentDeductions.Add(agentDeduction);
                            db.SaveChanges();
                        }
                    }
                    if (model.Incentives.Count > 0)
                    {
                        foreach (var incentive in model.Incentives)
                        {
                            var agentIncentive = new AgentIncentives();
                            agentIncentive.AgentId = AgentData.Id;
                            agentIncentive.AgentName = AgentData.FisrtName + " " + AgentData.LastName; ;
                            agentIncentive.IncentiveId = incentive.Id;
                            agentIncentive.IncentiveName = incentive.Name;
                            db.AgentIncentives.Add(agentIncentive);
                            db.SaveChanges();
                        }
                    }

                    foreach (var Doc in model.Docs) {
                        var docs = new Documents();
                        docs.AgentId = AgentResult.Id;
                        docs.Title = Doc.Title;
                        docs.SubTitle = Doc.SubTitle;
                        docs.UploadedBy = CurrentUserAgentId;
                        docs.UploadedOn = DateTime.Now;
                        docs.DocumentUrl = Doc.Url;
                        AgentData.Documents.Add(docs);
                        db.SaveChanges();
                    }

                    foreach (var Doc in model.EducationalDocs)
                    {
                        if (Doc.Title != null && Doc.Title != "")
                        {
                            var docs = new Documents();
                            docs.AgentId = AgentResult.Id;
                            docs.Title = Doc.Title;
                            docs.SubTitle = Doc.SubTitle;
                            docs.UploadedBy = CurrentUserAgentId;
                            docs.UploadedOn = DateTime.Now;
                            docs.DocumentUrl = Doc.Url;
                            AgentData.Documents.Add(docs);
                            db.SaveChanges();
                        }
                    }

                    foreach (var Doc in model.Certificates)
                    {
                        if (Doc.Title != null && Doc.Title != "")
                        {
                            var docs = new Documents();
                            docs.AgentId = AgentResult.Id;
                            docs.Title = Doc.Title;
                            docs.SubTitle = Doc.SubTitle;
                            docs.UploadedBy = CurrentUserAgentId;
                            docs.UploadedOn = DateTime.Now;
                            docs.DocumentUrl = Doc.Url;
                            AgentData.Documents.Add(docs);
                            db.SaveChanges();
                        }
                    }

                    var RoleResult = await _userManager.AddToRoleAsync(user.Id, Roles.Employee);
                    if (RoleResult.Succeeded)
                    {
                        response.IsRoleAdded = true;
                    }
                    else
                    {
                        response.IsRoleAdded = false;
                    }

                    db.SaveChanges();
                    response.Success = true;
                    // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return response;
                }
                if (model.ConfirmPassword != model.Password)
                {
                    response.ValidationErrors.ToList().Add("Password and confirm password does not match");
                }
                response.Success = false;
                response.ValidationErrors = (result.Errors);
            }

            // If we got this far, something failed, redisplay form
            return response;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize(Roles = "SuperAdmin,HR,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<object> EditUser(EditUserRequest request)
        {
            var response = new EditUserResponse();
            var UserId = User.Identity.GetUserId();
            var uploadedBy = db.Agent.Where(x => x.UserId == UserId).FirstOrDefault().Id;
            var AgentData = db.Agent.Where(x => x.Id == request.AgentId).FirstOrDefault();
            var DepartId = AgentData.DepartmentId;
            AgentData.DepartmentId = request.DepartmentId;
            AgentData.PositionName = request.PositionName;
            if (request.Position !=null)
            {
                AgentData.JobDescription = request.Position.JobDescription;
            }
            AgentData.FisrtName = request.FirstName;
            AgentData.LastName = request.LastName;
            AgentData.Address = request.Address;
            AgentData.Contact1 = request.Contact1;
            AgentData.Contact2 = request.Contact2;
            AgentData.Email = request.Email;
            AgentData.CreatedAt = DateTime.Now;
            AgentData.UpdatedBy = request.UpdatedBy;
            AgentData.RemainingLeaves = request.RemainingLeaves;
            AgentData.AnnualLeaves = request.AnnualLeaves;
            AgentData.MedicalLeaves = request.MedicalLeaves;
            AgentData.Gender = request.Gender;
            AgentData.ImageUrl = request.ImageUrl;
            AgentData.ShiftId = request.ShiftId;
            AgentData.Salary = request.Salary;

            AgentData.HouseRentAllowance = request.HouseRentAllowance;
            AgentData.Utility = request.Utility;
            AgentData.VehicleAllowance = request.VehicleAllowance;
            AgentData.FuelAllowance = request.FuelAllowance;
            AgentData.EOBIDeduction = request.EOBIDeduction;
            AgentData.LoanDeduction = request.LoanDeduction;
            AgentData.OtherDeduction = request.OtherDeduction;
            AgentData.OtherDeductionDesc = request.OtherDeductionDesc;

            AgentData.EmpCode = request.EmpCode;
            AgentData.Location = request.Location;
            AgentData.Section = request.Section;
            AgentData.Grade = request.Grade;
            AgentData.DateOfJoining = request.DateOfJoining;
            AgentData.WWID = request.WWID;
            AgentData.EmployementType = request.EmployementType;
            AgentData.PaidBy = request.PaidBy;
            AgentData.CNIC = request.CNIC;
            // removing previously selected taxes
            var AgentTaxes = db.AgentTaxes.Where(x => x.AgentId == AgentData.Id).ToList();
            foreach (var tax in AgentTaxes) {
                db.AgentTaxes.Remove(tax);
                db.SaveChanges();
            }

            // adding updated taxes applied on employee
            foreach (var tax in request.Taxes) {
                var agentTax = new AgentTaxes();
                agentTax.AgentId = AgentData.Id;
                agentTax.AgentName = AgentData.FisrtName + " " + AgentData.LastName;
                agentTax.TaxId = tax.Id;
                agentTax.TaxName = tax.Name;
                db.AgentTaxes.Add(agentTax);
                db.SaveChanges();
            }

            // removing previously selected deductions
            var AgentDeductions = db.AgentDeductions.Where(x => x.AgentId == AgentData.Id).ToList();
            foreach (var deduction in AgentDeductions)
            {
                db.AgentDeductions.Remove(deduction);
                db.SaveChanges();
            }

            // adding updated deductions applied on employee
            foreach (var deduction in request.Deductions)
            {
                var agentDeduction = new AgentDeductions();
                agentDeduction.AgentId = AgentData.Id;
                agentDeduction.AgentName = AgentData.FisrtName + " " + AgentData.LastName;
                agentDeduction.DeductionId = deduction.Id;
                agentDeduction.DeductionName = deduction.Name;
                db.AgentDeductions.Add(agentDeduction);
                db.SaveChanges();
            }


            // removing previously selected incentives
            var AgentIncentives = db.AgentIncentives.Where(x => x.AgentId == AgentData.Id).ToList();
            foreach (var incentive in AgentIncentives)
            {
                db.AgentIncentives.Remove(incentive);
                db.SaveChanges();
            }

            // adding updated incentives applied on employee
            foreach (var incentive in request.Incentives)
            {
                var agentIncentives = new AgentIncentives();
                agentIncentives.AgentId = AgentData.Id;
                agentIncentives.AgentName = AgentData.FisrtName + " " + AgentData.LastName;
                agentIncentives.IncentiveId = incentive.Id;
                agentIncentives.IncentiveName = incentive.Name;
                db.AgentIncentives.Add(agentIncentives);
                db.SaveChanges();
            }

            UpdateDocumentInDB("Resume", AgentData.Id, uploadedBy, request.Docs, AgentData);
            UpdateDocumentInDB("CNIC back", AgentData.Id, uploadedBy, request.Docs, AgentData);
            UpdateDocumentInDB("CNIC front", AgentData.Id, uploadedBy, request.Docs, AgentData);
            UpdateDocumentInDB("Appointment Letter", AgentData.Id, uploadedBy, request.Docs, AgentData);

            var EducationalDocs = AgentData.Documents.Where(x => x.Title == "Educational").ToList();
            foreach (var doc in EducationalDocs) {
                db.Documents.Remove(doc);
                db.SaveChanges();
            }

            foreach (var doc in request.EducationalDocs) {
                if (doc.Title != null && doc.Title != "")
                {
                    var Doc = new Documents();
                    Doc.AgentId = AgentData.Id;
                    Doc.UploadedBy = uploadedBy;
                    Doc.UploadedOn = DateTime.Now;
                    Doc.DocumentUrl = doc.Url;
                    Doc.Title = doc.Title;
                    Doc.SubTitle = doc.SubTitle;
                    db.Documents.Add(Doc);
                    db.SaveChanges();
                }
            }

            var Certificates = AgentData.Documents.Where(x => x.Title == "Certifications").ToList();
            foreach (var doc in Certificates)
            {
                db.Documents.Remove(doc);
                db.SaveChanges();
            }

            foreach (var doc in request.Certificates)
            {
                if (doc.Title != null && doc.Title != "")
                {
                    var Doc = new Documents();
                    Doc.AgentId = AgentData.Id;
                    Doc.Title = doc.Title;
                    Doc.SubTitle = doc.SubTitle;
                    Doc.UploadedBy = uploadedBy;
                    Doc.UploadedOn = DateTime.Now;
                    Doc.DocumentUrl = doc.Url;
                    AgentData.Documents.Add(Doc);
                    db.SaveChanges();
                }
            }

            if (request.HasSupervisor.HasValue)
            {
                AgentData.HasSupervisor = request.HasSupervisor;
                AgentData.SuperVisorId = request.SupervisorId;
            }

            db.SaveChanges();
            /*if (request.DepartmentId != DepartId)
            {
                var RoleToRemove = ((Departments)AgentData.DepartmentId.Value).ToString();
                var RoleRemovalResult = await _userManager.RemoveFromRoleAsync(AgentData.UserId, RoleToRemove);
                var RoleToAdd = "";
                if (request.DepartmentId == (int)Departments.Sales_Lead)
                {
                    RoleToAdd = Roles.Lead_Sales;
                }
                if (request.DepartmentId == (int)Departments.Closer)
                {
                    RoleToAdd = Roles.Closer;
                }
                if (request.DepartmentId == (int)Departments.Pre_Sales)
                {
                    RoleToAdd = Roles.PreSale;
                }
                if (request.DepartmentId == (int)Departments.PMD)
                {
                    RoleToAdd = Roles.Pmd_Feasibility;
                }
                if (request.DepartmentId == (int)Departments.Accounts)
                {
                    RoleToAdd = Roles.Accounts;
                }
                if (request.DepartmentId == (int)Departments.Core)
                {
                    RoleToAdd = Roles.Core;
                }
                if (request.DepartmentId == (int)Departments.SD)
                {
                    RoleToAdd = Roles.SD;
                }
                if (request.DepartmentId == (int)Departments.HR)
                {
                    RoleToAdd = Roles.HR;
                }
                var RoleResult = await _userManager.AddToRoleAsync(AgentData.UserId, RoleToAdd);
                if (RoleResult.Succeeded)
                {
                    response.IsRoleAdded = true;
                }
                else
                {
                    response.IsRoleAdded = false;
                }
            }*/
            return response;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize(Roles = "SuperAdmin,HR,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<object> EditUserByUser(EditUserRequest request)
        {
            var response = new EditUserResponse();

            var AgentData = db.Agent.Where(x => x.Id == request.AgentId).FirstOrDefault();
            AgentData.Address = request.Address;
            AgentData.Contact1 = request.Contact1;
            AgentData.Contact2 = request.Contact2;
            AgentData.ImageUrl = request.ImageUrl;
            db.SaveChanges();
            return response;
        }

        public bool UpdateDocumentInDB(string Title, int AgentId, int uploadedBy, List<Document> Docs, Agent AgentData) {
            var flag = false;
            var DbRow = AgentData.Documents.Where(x => x.Title == Title && x.AgentId == AgentId).FirstOrDefault();
            DbRow.SubTitle = Docs.Where(x => x.Title == Title).FirstOrDefault().SubTitle;
            DbRow.DocumentUrl = Docs.Where(x => x.Title == Title).FirstOrDefault().Url;
            if (DbRow.DocumentUrl != "" && DbRow.DocumentUrl != null)
            {
                DbRow.UploadedBy = uploadedBy;
                DbRow.UploadedOn = DateTime.Now;
            }
            db.SaveChanges();
            flag = true;
            return flag;
        }
    }
}