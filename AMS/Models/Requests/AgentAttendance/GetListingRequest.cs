using AMS.Model.Model;
using AMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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
		public bool? IsAbsent { get; set; }
		public bool? IsHoliday { get; set; }
		public int RemainingLeaves { get; set; }
		public int ConsecutiveLateCounter { get; set; }
		public int DeductionInDays { get; set; }
		public string Remarks { get; set; }
		public int Type { get; set; }
		public string TypeEnum { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
        public string WorkingHours { get; set; }
		public int ShiftId { get; set; }
		public string ShiftName { get; set; }

	}
	public class GetListingRequest
	{
		private AMSEntities _dbContext = new AMSEntities();
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public object RunRequest(GetListingRequest req)
		{
			var response = new GetListingResponse();
			response.Data = new List<AgentAttendanceData>();
			var tomorrow = DateTime.Today.AddDays(1);
			var Attendances = _dbContext.AgentAttendance.Where(x => x.Date < tomorrow).OrderBy(x => x.CreatedAt).ToList();
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
				if (attendance.CreatedAt.Value.Date == DateTime.Today && !attendance.IsAttendanceMarked) { }
				else
				{
					var AgentAttendance = new AgentAttendanceData();
					AgentAttendance.Id = attendance.Id;
					AgentAttendance.AgentId = attendance.AgentId;
					AgentAttendance.AgentName = attendance.Agent.FisrtName + " " + attendance.Agent.LastName;
					AgentAttendance.CreatedAt = attendance.CreatedAt;
					AgentAttendance.CreatedBy = attendance.CreatedBy;
					AgentAttendance.Date = attendance.Date;
					AgentAttendance.EndDate = attendance.EndDateTime;
					AgentAttendance.IsLate = attendance.IsLate;
					AgentAttendance.IsExcused = attendance.IsExcused;
					AgentAttendance.IsPresent = attendance.IsPresent;
					AgentAttendance.IsAbsent = attendance.IsAbsent;
					AgentAttendance.IsHoliday = attendance.IsHoliday;
					AgentAttendance.StartDate = attendance.StartDateTime;
					AgentAttendance.StartDateTime = attendance.StartDateTime;
					AgentAttendance.ShiftId = attendance.ShiftId;
					AgentAttendance.ShiftName = attendance.Shifts.Name;
					if (attendance.StartDateTime.HasValue)
					{
						if (attendance.EndDateTime != null)
						{
							AgentAttendance.EndDateTime = attendance.EndDateTime;
							TimeSpan duration = AgentAttendance.EndDate.Value.TimeOfDay - AgentAttendance.StartDate.Value.TimeOfDay;
							AgentAttendance.WorkingHours = duration.TotalHours.ToString("#.##");
                            if (AgentAttendance.WorkingHours != "")
                            {
								var time = TimeSpan.FromHours(Convert.ToDouble(AgentAttendance.WorkingHours));
								AgentAttendance.WorkingHours = time.Hours + "h " + time.Minutes + "m";
							}
						}
						else if (attendance.StartDateTime.Value.Date == DateTime.Now.Date && DateTime.Now.TimeOfDay < attendance.Shifts.EndTime.Value.TimeOfDay)
						{
							AgentAttendance.WorkingHours = "Currently Working";
						}
						else
						{
							TimeSpan ts = new TimeSpan(17, 30, 0);
							AgentAttendance.EndDateTime = attendance.StartDateTime.Value.Date + ts;
							TimeSpan duration = AgentAttendance.EndDateTime.Value.TimeOfDay - AgentAttendance.StartDateTime.Value.TimeOfDay;
							AgentAttendance.WorkingHours = duration.TotalHours.ToString("#.##");
							var time = TimeSpan.FromHours(Convert.ToDouble(AgentAttendance.WorkingHours));
							AgentAttendance.WorkingHours = time.Hours + "h " + time.Minutes + "m";
						}
					}
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
			}
			return response;
		}
	}
}
