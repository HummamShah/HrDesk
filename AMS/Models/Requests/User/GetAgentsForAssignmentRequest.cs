using AMS.Model.Model;
using AMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.User
{
	public class GetAgentsForAssignmentResponse
	{
		public List<AgentsDropdownData> Data { get; set; }
	}
	public class AgentsDropdownData
	{
		public int Id { get; set; }
		public string Name { get; set; }

	}
	public class GetAgentsForAssignmentRequest
	{
		private AMSEntities _dbContext = new AMSEntities();
		public string Type { get; set; }
		public object RunRequest(GetAgentsForAssignmentRequest req)
		{
			var response = new GetAgentsForAssignmentResponse();
			response.Data = new List<AgentsDropdownData>();
			//TODO set data  according to type = pmd=2,presale=3,lead =1
			//Todo Fix the deparment id check need to send departmentid from angular

			var Data = _dbContext.Agent.ToList();
			if (req.Type == "Lead")
			{
				Data = Data.Where(x => x.DepartmentId == (int)Departments.Sales_Lead).ToList();
			}
			if (req.Type == "PMD")
			{
				Data = Data.Where(x => x.DepartmentId == (int)Departments.PMD).ToList();
			}
			if (req.Type == "PreSale")
			{
				Data = Data.Where(x => x.DepartmentId == (int)Departments.Pre_Sales).ToList();
			}
			if (req.Type == "SD")
			{
				Data = Data.Where(x => x.DepartmentId == (int)Departments.SD).ToList();
			}
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