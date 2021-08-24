using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Controllers.Api
{
    public class AddAgentAttendanceResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class AddAgentAttendanceRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public object RunRequest(AddAgentAttendanceRequest request)
        {
            var response = new AddAgentAttendanceResponse();
            var Attendance = new AMS.Model.Model.AgentAttendance();
            Attendance.AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).First().Id;
            Attendance.CreatedAt = DateTime.Now;
            Attendance.Date = DateTime.Now.Date;
            Attendance.IsPresent = true;
            var result = _dbContext.AgentAttendance.Add(Attendance);
            _dbContext.SaveChanges();
            return response;
        }
    }
}