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
    public class TerminateUserResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
        public bool IsRoleAdded { get; set; }
        public bool Success { get; set; }
    }
    public class TerminateUserRequest
    {
        private ApplicationUserManager _userManager;
        private AMSEntities _dbContext = new AMSEntities();
        public int AgentId { get; set; }
        public string UpdatedBy { get; set; }
        public TerminateUserRequest()
        {
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

        }
        public  object RunRequest(TerminateUserRequest request)
        {
            var response = new EditAdminResponse();
            var AgentData = _dbContext.Agent.Where(x => x.Id == request.AgentId).FirstOrDefault();
            AgentData.IsActive = false;
            AgentData.UpdatedAt = DateTime.Now;
            AgentData.UpdatedBy = request.UpdatedBy;
            _dbContext.SaveChanges();
            response.Success = true;
            return response;
        }
    }
}
