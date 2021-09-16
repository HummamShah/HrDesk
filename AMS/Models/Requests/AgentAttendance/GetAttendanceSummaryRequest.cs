using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AMS.Models.Requests.AgentAttendance
{
    public class GetAttendanceSummaryRequest  
    {

        private AMSEntities _dbContext = new AMSEntities();
        public string UserId{ get; set; }
        public int Month{ get; set; }
        public int Year{ get; set; }
        public Object RunRequest(GetAttendanceSummaryRequest request)
        {
            var response = new GetAttendanceSummaryResponse();
            try
            {
                //var id = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
                //var Attendances = _dbContext.AgentAttendance.Where(x => x.AgentId == id && x.IsAttendanceMarked == true).ToList();
                var Attendances = _dbContext.AgentAttendance.Where(x => x.Agent.UserId == request.UserId && x.IsAttendanceMarked == true).OrderBy(x => x.CreatedAt).ToList();
                if (request.Month == 1) {
                    Attendances = Attendances.Where(x => x.CreatedAt >= new DateTime(Year, Month + 1, 1) && x.CreatedAt <= new DateTime(Year, Month + 1, 28)).ToList();
                }
                else if (request.Month == 0 || request.Month == 2|| request.Month == 4 || request.Month == 6 || request.Month == 7 || request.Month == 9 || request.Month == 11) {
                    Attendances = Attendances.Where(x => x.CreatedAt >= new DateTime(Year, Month + 1, 1) && x.CreatedAt <= new DateTime(Year, Month + 1, 31)).ToList();
                }
                else if (request.Month == 3 || request.Month == 5 || request.Month == 8 || request.Month == 10)
                {
                    Attendances = Attendances.Where(x => x.CreatedAt >= new DateTime(Year, Month + 1, 1) && x.CreatedAt <= new DateTime(Year, Month + 1, 30)).ToList();
                }
                if (Attendances.Count != 0)
                {
                    response.AbsentCount = Attendances.Where(x => x.IsAbsent == true).Count();
                    response.PresentCount = Attendances.Where(x => x.IsPresent == true).Count();
                    response.LateCount = Attendances.Where(x => x.IsLate == true).Count();
                    response.ExcuseCount = Attendances.Where(x => x.IsExcused == true).Count();

                    // getting individual attendance details
                    response.AgentAttandanceList = new List<AgentAttendanceData>();
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
                        response.IsSuccessful = true;
                    }
                }
                else {
                    response.AttendanceIsEmpty = true;
                    response.IsSuccessful = true;
                }
            }
            catch (Exception e)
            {
                response.IsSuccessful = false;
                response.ValidationErrors.Add(e.Message);
            }
            return response;
        }
    }
    public class GetAttendanceSummaryResponse
    {
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LateCount { get; set; }
        public int ExcuseCount { get; set; }
        public bool IsSuccessful { get; set; }
        public bool AttendanceIsEmpty { get; set; }
        public List<AgentAttendanceData> AgentAttandanceList { get; set; } = new List<AgentAttendanceData>();
        public List<string> ValidationErrors { get; set; } = new List<string>();
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