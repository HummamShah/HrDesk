using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AMS.Models.Requests.AgentAttendance
{
    public class GetAttendanceSummaryRequest  
    {

        private AMSEntities _dbContext = new AMSEntities();
        public string CurrentUserId{ get; set; }
        public int? AgentId{ get; set; }
        public int Month{ get; set; }
        public int Year{ get; set; }
        public Object RunRequest(GetAttendanceSummaryRequest request)
        {
            var response = new GetAttendanceSummaryResponse();
            try
            {
                int Id;
                if (request.AgentId != null) {
                    Id = request.AgentId.Value;
                }
                else {
                    Id = _dbContext.Agent.Where(x => x.UserId == request.CurrentUserId).FirstOrDefault().Id;
                }
                var tomorrow = DateTime.Today.AddDays(1);
                var Attendances = _dbContext.AgentAttendance.Where(x => x.Agent.Id == Id && x.Date != tomorrow).OrderBy(x => x.CreatedAt).ToList();
                var firstOfMonth = new DateTime(request.Year, request.Month + 1, 1);
                var lastOFMonth = firstOfMonth.AddMonths(1).AddDays(-1);
                Attendances = Attendances.Where(x => x.Date >= firstOfMonth && x.Date <= lastOFMonth).ToList();
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
                            else if (Attendance.StartDateTime.Value.Date == DateTime.Now.Date && DateTime.Now.TimeOfDay < Attendance.Shifts.EndTime.Value.TimeOfDay)
                            {
                                AgentAttendance.WorkingHours = "Currently Working";
                            }
                            else
                            {
                                TimeSpan ts = new TimeSpan(17, 30, 0);
                                AgentAttendance.EndDate = Attendance.StartDateTime.Value.Date + ts;
                                //AgentAttendance.EndDate = Attendance.StartDateTime.Value.Date + Attendance.Shifts.EndTime.Value.TimeOfDay;
                                TimeSpan duration = AgentAttendance.EndDate.Value.TimeOfDay - AgentAttendance.StartDate.Value.TimeOfDay;
                                AgentAttendance.WorkingHours = duration.TotalHours.ToString("#.##");
                            }
                        }
                        AgentAttendance.IsLate = Attendance.IsLate;
                        AgentAttendance.IsExcused = Attendance.IsExcused;
                        AgentAttendance.IsPresent = Attendance.IsPresent;
                        AgentAttendance.IsAbsent = Attendance.IsAbsent;
                        AgentAttendance.IsAttendanceMarked = Attendance.IsAttendanceMarked;
                        AgentAttendance.ShiftId = Attendance.ShiftId;
                        AgentAttendance.ShiftName = Attendance.Shifts.Name;

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
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
    }
}