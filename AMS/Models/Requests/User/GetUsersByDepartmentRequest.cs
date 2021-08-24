using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.User
{
	public class GetUsersByDepartmentResponse
	{
		public List<AgentsDropdownData> Data { get; set; }
	}

	public class GetUsersByDepartmentRequest
	{
		private AMSEntities _dbContext = new AMSEntities();
		public int Id { get; set; }
		public object RunRequest(GetUsersByDepartmentRequest req)
		{
			var response = new GetUsersByDepartmentResponse();
			response.Data = new List<AgentsDropdownData>();
			//TODO set data  according to type = pmd,presale,lead
			var Data = _dbContext.Agent.Where(x => x.DepartmentId == req.Id);
			foreach (var d in Data)
			{
				var temp = new AgentsDropdownData();
				temp.Id = d.Id;
				temp.Name = d.FisrtName + " " + d.LastName;
				response.Data.Add(temp);
			}
			return response;
		}
	}
}