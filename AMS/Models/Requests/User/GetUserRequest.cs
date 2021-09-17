using AMS.Model.Model;
using AMS.Models.Enums;
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
		public string DepartmentEnum { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Contact1 { get; set; }
		public string Contact2 { get; set; }
		public string Email { get; set; }
		public bool? HasSupervisor { get; set; }
		public int? SupervisorId { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
		public int? RemainingLeaves { get; set; }
		public int? AnnualLeaves { get; set; }
		public int? MedicalLeaves { get; set; }
		public bool? IsLate { get; set; }
		public bool? IsPresent { get; set; }
		public bool? IsAbsent{ get; set; }
		public bool? IsExcused{ get; set; }
		public DateTime? TodayClockIn { get; set; }
		public int? AbsentCount { get; set; }
		public int? ExcusedCount { get; set; }
		public int? PresentCount { get; set; }
		public int? LateCount { get; set; }
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
				response.RemainingLeaves = Data.RemainingLeaves;
				response.AnnualLeaves = Data.AnnualLeaves;
				response.MedicalLeaves = Data.MedicalLeaves;
                response.Gender = Data.Gender;
				var MonthAttendance = Data.AgentAttendance.Where(x => x.CreatedAt.Value.Date.Month == DateTime.Today.Month && x.IsAttendanceMarked == true).ToList();
				response.AbsentCount = MonthAttendance.Where(x => x.IsAbsent == true).Count();
				response.PresentCount = MonthAttendance.Where(x => x.IsPresent == true).Count();
				response.LateCount = MonthAttendance.Where(x => x.IsLate == true).Count();
				response.ExcusedCount = MonthAttendance.Where(x => x.IsExcused == true).Count();
;				var TodaysAttendance = Data.AgentAttendance.Where(x => x.CreatedAt.Value.Date == DateTime.Today.Date).FirstOrDefault();
				if (TodaysAttendance.IsAbsent == false)
					response.TodayClockIn = TodaysAttendance.StartDateTime;
				if (response.DepartmentId.HasValue) {
					response.DepartmentEnum = ((Departments)Data.DepartmentId.Value).ToString();
				}
				response.IsLate = TodaysAttendance.IsLate;
				response.IsPresent = TodaysAttendance.IsPresent;
				response.IsAbsent = TodaysAttendance.IsAbsent;
				response.IsExcused = TodaysAttendance.IsExcused;
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
				response.RemainingLeaves = Data.RemainingLeaves;
				response.AnnualLeaves = Data.AnnualLeaves;
				response.MedicalLeaves = Data.MedicalLeaves;
				response.Gender = Data.Gender;
				var MonthAttendance = Data.AgentAttendance.Where(x => x.CreatedAt.Value.Date.Month == DateTime.Today.Month && x.IsAttendanceMarked == true).ToList();
				response.AbsentCount = MonthAttendance.Where(x => x.IsAbsent == true).Count();
				response.PresentCount = MonthAttendance.Where(x => x.IsPresent == true).Count();
				response.LateCount = MonthAttendance.Where(x => x.IsLate == true).Count();
				response.ExcusedCount = MonthAttendance.Where(x => x.IsExcused == true).Count();
				var TodaysAttendance = Data.AgentAttendance.Where(x => x.CreatedAt.Value.Date == DateTime.Today.Date).FirstOrDefault();
				if (TodaysAttendance.IsAbsent == false)
					response.TodayClockIn = Data.AgentAttendance.Where(x => x.StartDateTime.Value.Date == DateTime.Today.Date).FirstOrDefault().StartDateTime;
				if (response.DepartmentId.HasValue)
				{
					response.DepartmentEnum = ((Departments)Data.DepartmentId.Value).ToString();
				}
				response.IsLate = TodaysAttendance.IsLate;
				response.IsPresent = TodaysAttendance.IsPresent;
				response.IsAbsent = TodaysAttendance.IsAbsent;
				response.IsExcused = TodaysAttendance.IsExcused;
			}

			return response;
		}
	}
}