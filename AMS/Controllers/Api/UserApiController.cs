using AMS.Model.Enum;
using AMS.Model.Model;
using AMS.Model.Requests.User;
using AMS.Models;
using AMS.Models.Enums;
using AMS.Models.Requests.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


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
            if (ModelState.IsValid && model.ConfirmPassword == model.Password)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email }; //We can put username field instead of email
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var CurrentUserName = User.Identity.Name;
                    var AgentData = new Agent();
                    AgentData.UserId = user.Id;
                    AgentData.FisrtName = model.FirstName;
                    AgentData.LastName = model.LastName;
                    AgentData.Address = model.Address;
                    AgentData.Contact1 = model.Contact1;
                    AgentData.Contact2 = model.Contact2;
                    AgentData.Email = model.Email;
                    AgentData.CreatedAt = DateTime.Now;
                    AgentData.CreatedBy = CurrentUserName;
                    AgentData.ImageUrl = model.ImageUrl;
                    if (model.HasSupervisor.HasValue)
                    {
                        AgentData.HasSupervisor = model.HasSupervisor;
                        AgentData.SuperVisorId = model.SupervisorId;
                    }
                    var AgentResult = db.Agent.Add(AgentData);
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
        [System.Web.Http.Authorize(Roles = "SuperAdmin,HR")]
        [ValidateAntiForgeryToken]
        public async Task<object> EditUser(EditUserRequest request)
        {
            var response = new EditUserResponse();

            var AgentData = db.Agent.Where(x => x.Id == request.AgentId).FirstOrDefault();
            var DepartId = AgentData.DepartmentId;
            AgentData.DepartmentId = request.DepartmentId;
            AgentData.FisrtName = request.FirstName;
            AgentData.LastName = request.LastName;
            AgentData.Address = request.Address;
            AgentData.Contact1 = request.Contact1;
            AgentData.Contact2 = request.Contact2;
            AgentData.Email = request.Email;
            AgentData.CreatedAt = DateTime.Now;
            AgentData.UpdatedBy = request.UpdatedBy;
            AgentData.RemainingLeaves = request.RemainingLeaves;
            AgentData.ImageUrl = request.ImageUrl;
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

    }
}