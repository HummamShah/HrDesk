using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.User
{
	public class UserIdentityResponse
	{
		public int Id { get; set; }
		public int AgentId { get; set; }
		public string UpdatedBy { get; set; }
		public int? DepartmentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Contact1 { get; set; }
		public string Contact2 { get; set; }
		public string Email { get; set; }
		public bool? HasSupervisor { get; set; }
		public int? SupervisorId { get; set; }
		public int RemainingLeaves { get; set; }
		public int AnnualLeaves { get; set; }
		public int MedicalLeaves { get; set; }
		public string ImageUrl { get; set; }
        public List<string> Role { get; set; }
    }

	public class UserIdentityRequest
	{
		private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; internal set; }

        public object RunRequest(UserIdentityRequest req)
		{
			var response = new UserIdentityResponse();

			//TODO set data  according to type = pmd,presale,lead
			var Data = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
            response.Id = Data.Id;
            response.FirstName = Data.FisrtName;
			response.LastName = Data.LastName;
			response.ImageUrl = Data.ImageUrl;
			response.Role = Data.AspNetUsers.AspNetRoles.Select(x => x.Name).ToList();
			response.RemainingLeaves = Data.RemainingLeaves;
			response.AnnualLeaves = Data.AnnualLeaves;
			response.MedicalLeaves = Data.MedicalLeaves;
			return response;
		}
	}
}