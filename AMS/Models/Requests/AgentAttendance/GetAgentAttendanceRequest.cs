using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.AgentAttendance
{
    public class GetAgentAttendanceRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public object RunRequest(GetAgentAttendanceRequest request)
        {
            var response = new GetAgentAttendanceResponse();
            var Agent = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault();
            try
            {
                response.FirstName = Agent.FisrtName;
                response.LastName = Agent.LastName;
                response.ImageUrl = Agent.ImageUrl;
                response.AgentAttandanceList = new List<AgentAttendanceData>();

                var Attendances = _dbContext.AgentAttendance.Where(x => x.Agent.UserId == request.UserId).ToList();
                if (request.DateFrom.HasValue)
                {
                    Attendances = Attendances.Where(x => x.Date >= request.DateFrom.Value).ToList();
                }
                if (request.DateTo.HasValue)
                {
                    Attendances = Attendances.Where(x => x.Date <= request.DateTo.Value).ToList();
                }
                response.AbsentCount = Attendances.Where(x => x.IsAbsent == true).Count();
                response.PresentCount = Attendances.Where(x => x.IsPresent == true).Count();
                response.LateCount = Attendances.Where(x => x.IsLate == true).Count();
                foreach (var Attendance in Attendances)
                {
                    var AgentAttendance = new AgentAttendanceData();
                    AgentAttendance.Id = Attendance.Id;
                    AgentAttendance.Date = Attendance.Date;
                    AgentAttendance.StartDate = Attendance.StartDateTime;
                
                    if (Attendance.StartDateTime.HasValue)
                    {
                        if (Attendance.EndDateTime != null)
                        {
                            AgentAttendance.EndDate = Attendance.EndDateTime;
                            TimeSpan duration = AgentAttendance.EndDate.Value.TimeOfDay - AgentAttendance.StartDate.Value.TimeOfDay;
                            AgentAttendance.WorkingHours = duration.TotalHours.ToString("#.##");
                        }
                        else if (Attendance.StartDateTime.Value.Date == DateTime.Now.Date && DateTime.Now.TimeOfDay < new TimeSpan(17, 30, 0))
                        {
                            AgentAttendance.WorkingHours = "Currently Working";
                        }
                        else
                        {
                            TimeSpan ts = new TimeSpan(17, 30, 0);
                            AgentAttendance.EndDate = Attendance.StartDateTime.Value.Date + ts;
                            TimeSpan duration = AgentAttendance.EndDate.Value.TimeOfDay - AgentAttendance.StartDate.Value.TimeOfDay;
                            AgentAttendance.WorkingHours = duration.TotalHours.ToString("#.##");
                        }
                    }
                    AgentAttendance.IsLate = Attendance.IsLate;
                    AgentAttendance.IsExcused = Attendance.IsExcused;
                    AgentAttendance.IsPresent = Attendance.IsPresent;
                    AgentAttendance.IsAbsent = Attendance.IsAbsent;
                    AgentAttendance.IsAttendanceMarked = Attendance.IsAttendanceMarked;

                    response.AgentAttandanceList.Add(AgentAttendance);
                }
                response.IsSuccessfull = true;
            }
            catch (Exception e)
            {
                response.IsSuccessfull = false;
                response.ValidationErrors.Add(e.Message);
            }
            return response;
        }
    }
    public class GetAgentAttendanceResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public List<AgentAttendanceData> AgentAttandanceList { get; set; }
        public bool IsSuccessfull { get; set; }
        public List<String> ValidationErrors { get; set; } = new List<string>();

    }
    public class AgentAttendanceData
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsAttendanceMarked { get; set; }
        public bool IsPresent { get; set; }
        public bool IsLate { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsExcused { get; set; }
        public string WorkingHours { get; set; }

    }
}



//using AMS.Model.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace AMS.Models.Requests.AgentAttendance
//{
//    public class GetAgentAttendanceResponse
//    {
//        public int Id { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string ImageUrl { get; set; }
//        public DateTime? Date { get; set; }
//        public DateTime? StartDate { get; set; }
//        public DateTime? EndDate { get; set; }
//        public bool IsAttendanceMarked { get; set; }
//        public bool IsPresent { get; set; }
//        public bool IsLate { get; set; }
//        public bool IsExcused { get; set; }
//    }
//    public class GetAgentAttendanceRequest
//    {
//        private AMSEntities _dbContext = new AMSEntities();
//        public string UserId { get; set; }
//        public object RunRequest(GetAgentAttendanceRequest request)
//        {
//            var response = new GetAgentAttendanceResponse();
//            var Agent = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault();
//            response.FirstName = Agent.FisrtName;
//            response.LastName = Agent.LastName;
//            var Attendance = Agent.AgentAttendance.Where(x=>x.Date == DateTime.Today).FirstOrDefault();
//            response.ImageUrl = Agent.ImageUrl;
//            response.StartDate = Attendance.StartDate;
//            response.EndDate = Attendance.EndDate;
//            response.IsAttendanceMarked = Attendance.IsAttendanceMarked;
//            response.IsPresent = Attendance.IsPresent;
//            response.IsLate = Attendance.IsLate;
//            response.IsExcused = Attendance.IsExcused;
//            return response;
//        }
//    }
//}