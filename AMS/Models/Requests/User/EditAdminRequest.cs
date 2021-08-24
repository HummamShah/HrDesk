using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Web;

namespace AMS.Model.Requests.User
{
    public class EditAdminResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool IsRoleAdded { get; set; }
        public bool Success { get; set; }
    }
    public class EditAdminRequest
    {
        private ApplicationUserManager _userManager;
        private AMSEntities _dbContext = new AMSEntities();
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

        public EditAdminRequest()
        {
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        }
        public async Task<object> RunRequest(EditAdminRequest request)
        {
            var response = new EditAdminResponse();
            var AgentData = _dbContext.Agent.Where(x => x.Id == request.AgentId).FirstOrDefault();
            AgentData.DepartmentId = request.DepartmentId;
            AgentData.FisrtName = request.FirstName;
            AgentData.LastName = request.LastName;
            AgentData.Address = request.Address;
            AgentData.Contact1 = request.Contact1;
            AgentData.Contact2 = request.Contact2;
            AgentData.Email = request.Email;
            AgentData.CreatedAt = DateTime.Now;
            AgentData.UpdatedBy = request.UpdatedBy;
            if (request.HasSupervisor.HasValue)
            {
                AgentData.HasSupervisor = request.HasSupervisor;
                AgentData.SuperVisorId = request.SupervisorId;
            }
            _dbContext.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
