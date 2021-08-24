using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.User
{
	public class GetAllUsersDropdownResponse
	{
		public List<AgentsDropdownData> Data { get; set; }
	}

	public class GetAllUsersDropdownRequest
	{
		private AMSEntities _dbContext = new AMSEntities();
		public object RunRequest(GetAllUsersDropdownRequest req)
		{
			var response = new GetAllUsersDropdownResponse();
			response.Data = new List<AgentsDropdownData>();
			//TODO set data  according to type = pmd,presale,lead
			var Data = _dbContext.Agent;
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