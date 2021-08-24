using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.AgentAttendance
{
    public class GetAgentAttendanceResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAttendanceMarked { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLate { get; set; }
        public bool IsExcused { get; set; }
    }
    public class GetAgentAttendanceRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public object RunRequest(GetAgentAttendanceRequest request)
        {
            var response = new GetAgentAttendanceResponse();
            var Agent = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault();
            response.FirstName = Agent.FisrtName;
            response.LastName = Agent.LastName;
            var Attendance = Agent.AgentAttendance.Where(x=>x.Date == DateTime.Today).FirstOrDefault();
            response.ImageUrl = Agent.ImageUrl;
            response.StartDate = Attendance.StartDate;
            response.EndDate = Attendance.EndDate;
            response.IsAttendanceMarked = Attendance.IsAttendanceMarked;
            response.IsPresent = Attendance.IsPresent;
            response.IsLate = Attendance.IsLate;
            response.IsExcused = Attendance.IsExcused;
            return response;
        }
    }
}