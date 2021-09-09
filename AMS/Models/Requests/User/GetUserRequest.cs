using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.User
{
	public class GetUserResponse
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
        public string ImageUrl { get; set; }

		
	}

	public class GetUserRequest
	{
		private AMSEntities _dbContext = new AMSEntities();
		public int Id { get; set; }
        public string UserId { get; set; }
        public object RunRequest(GetUserRequest req)
		{
			var response = new GetUserResponse();

			//TODO set data  accorvar Data = .ToList();
			if (req.Id != null && req.Id != 0)
            {
				var Data = _dbContext.Agent.Where(x => x.Id == req.Id).FirstOrDefault();
				response.Id = Data.Id;
				response.FirstName = Data.FisrtName;
				response.Address = Data.Address;
				response.AgentId = Data.Id;
				response.Contact1 = Data.Contact1;
				response.Contact2 = Data.Contact2;
				response.DepartmentId = Data.DepartmentId;
				response.Email = Data.Email;
				response.HasSupervisor = Data.HasSupervisor;
				response.LastName = Data.LastName;
				response.SupervisorId = Data.SuperVisorId;
				response.ImageUrl = Data.ImageUrl;
			}
            else
            {
				var Data = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
				response.Id = Data.Id;
				response.FirstName = Data.FisrtName;
				response.Address = Data.Address;
				response.AgentId = Data.Id;
				response.Contact1 = Data.Contact1;
				response.Contact2 = Data.Contact2;
				response.DepartmentId = Data.DepartmentId;
				response.Email = Data.Email;
				response.HasSupervisor = Data.HasSupervisor;
				response.LastName = Data.LastName;
				response.SupervisorId = Data.SuperVisorId;
				response.ImageUrl = Data.ImageUrl;
				
			}
			
			return response;
		}
	}
}