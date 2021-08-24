using AMS.Model.Model;
using AMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Requests.AgentAttendance
{
	public class GetListingResponse
	{
		public List<AgentAttendanceData> Data { get; set; }
	}
	public class AgentAttendanceData
	{
		public int Id { get; set; }
		public int AgentId { get; set; }
		public string AgentName { get; set; }
		public DateTime? StartDateTime { get; set; }
		public DateTime? EndDate { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDateTime { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? Date { get; set; }
		public bool IsPresent { get; set; }
		public bool IsLate { get; set; }
		public bool? IsExcused { get; set; }
		public int RemainingLeaves { get; set; }
		public int ConsecutiveLateCounter { get; set; }
		public int DeductionInDays { get; set; }
		public string Remarks { get; set; }
		public int Type { get; set; }
		public string TypeEnum { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }

	}
	public class GetListingRequest
	{
		private AMSEntities _dbContext = new AMSEntities();
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		//Pagination Fields
		public object RunRequest(GetListingRequest req)
		{
			var response = new GetListingResponse();
			response.Data = new List<AgentAttendanceData>();
			//var Companies = _dbContext.Company.Where(x=>x.BillingInformationId != null);
			var Attendances = _dbContext.AgentAttendance.ToList();
			if (req.DateFrom.HasValue)
			{
				Attendances = Attendances.Where(x => x.Date >= req.DateFrom.Value).ToList();
			}
			if (req.DateTo.HasValue)
			{
				Attendances = Attendances.Where(x => x.Date <= req.DateTo.Value).ToList();
			}
			foreach (var attendance in Attendances)
			{
				var AgentAttendance = new AgentAttendanceData();
				AgentAttendance.Id = attendance.Id;
				AgentAttendance.AgentId = attendance.AgentId;
				AgentAttendance.AgentName = attendance.Agent.FisrtName + " " + attendance.Agent.LastName;
				AgentAttendance.CreatedAt = attendance.CreatedAt;
				AgentAttendance.CreatedBy = attendance.CreatedBy;
				AgentAttendance.Date = attendance.Date;
				AgentAttendance.EndDate = attendance.EndDate;
				AgentAttendance.EndDateTime = attendance.EndDateTime;
				AgentAttendance.IsLate = attendance.IsLate;
				AgentAttendance.IsExcused = attendance.IsExcused;
				AgentAttendance.IsPresent = attendance.IsPresent;
				AgentAttendance.StartDate = attendance.StartDate;
				AgentAttendance.StartDateTime = attendance.StartDateTime;
				AgentAttendance.UpdatedAt = attendance.UpdatedAt;
				AgentAttendance.UpdatedBy = attendance.UpdatedBy;
				AgentAttendance.RemainingLeaves = attendance.Agent.RemainingLeaves;
				AgentAttendance.ConsecutiveLateCounter = attendance.Agent.ConsecutiveLateCounter;
				AgentAttendance.DeductionInDays = attendance.Agent.DeductionInDays;
				AgentAttendance.Remarks = attendance.Remarks;
				AgentAttendance.Latitude = attendance.Latitude;
				AgentAttendance.Longitude = attendance.Longitude;
				AgentAttendance.Type = attendance.Type;
				AgentAttendance.TypeEnum = ((AttendanceType)attendance.Type).ToString();
				response.Data.Add(AgentAttendance);
			}
			return response;
		}
	}
}
