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
        public int? AgentsId{ get; set; }
        public DateTime Date{ get; set; }
      /*  public int Month{ get; set; }
        public int Year{ get; set; }*/
        public Object RunRequest(GetAttendanceSummaryRequest request)
        {
            var response = new GetAttendanceSummaryResponse();
            response.AgentAttandanceList = new List<AgentAttendanceData>();
            try
            {
                if (request.AgentId != null)
                {
                    var Attendances = _dbContext.AgentAttendance.Where(x => x.Date.Value.Month == request.Date.Month && x.AgentId == request.AgentId).ToList();

                    if (Attendances.Count != 0)
                    {
                        response.AbsentCount = Attendances.Where(x => x.IsAbsent == true).Count();
                        response.PresentCount = Attendances.Where(x => x.IsPresent == true).Count();
                        response.LateCount = Attendances.Where(x => x.IsLate == true).Count();
                        response.ExcuseCount = Attendances.Where(x => x.IsExcused == true).Count();
                        response.HolidayCount = Attendances.Where(x => x.IsHoliday == true).Count();
                        // getting individual attendance details
                        foreach (var Attendance in Attendances)
                        {
                            var AGD = new AgentAttendanceData();
                            AGD.Date = Attendance.Date;
                            AGD.IsLate = Attendance.IsLate;
                            AGD.IsExcused = Attendance.IsExcused;
                            AGD.IsPresent = Attendance.IsPresent;
                            AGD.IsAbsent = Attendance.IsAbsent;
                            AGD.IsHoliday = Attendance.IsHoliday;
                            AGD.IsAttendanceMarked = Attendance.IsAttendanceMarked;
                            AGD.ShiftId = Attendance.ShiftId;
                            AGD.ShiftName = Attendance.Shifts.Name;
                            AGD.StartDate = Attendance.StartDateTime;
                            if (Attendance.StartDateTime.HasValue)
                            {
                                if (Attendance.EndDateTime != null)
                                {
                                    AGD.EndDate = Attendance.EndDateTime;
                                    TimeSpan duration = AGD.EndDate.Value.TimeOfDay - AGD.StartDate.Value.TimeOfDay;
                                    AGD.WorkingHours = duration.TotalHours.ToString("#.##");
                                    var time = TimeSpan.FromHours(Convert.ToDouble(AGD.WorkingHours));
                                    AGD.WorkingHours = time.Hours + "h " + time.Minutes + "m";
                                }
                                else if (Attendance.StartDateTime.Value.Date == DateTime.Now.Date && DateTime.Now.TimeOfDay < Attendance.Shifts.EndTime.Value.TimeOfDay)
                                {
                                    AGD.WorkingHours = "Currently Working";
                                }
                                else
                                {
                                    TimeSpan ts = new TimeSpan(17, 30, 0);
                                    AGD.EndDate = Attendance.StartDateTime.Value.Date + ts;
                                    TimeSpan duration = AGD.EndDate.Value.TimeOfDay - AGD.StartDate.Value.TimeOfDay;
                                    AGD.WorkingHours = duration.TotalHours.ToString("#.##");
                                    var time = TimeSpan.FromHours(Convert.ToDouble(AGD.WorkingHours));
                                    AGD.WorkingHours = time.Hours + "h " + time.Minutes + "m";
                                }
                            }
                            response.AgentAttandanceList.Add(AGD);
                            response.IsSuccessful = true;

                        }
                    }
                }
                if (request.AgentsId != null)
                {
                    var Attendances = _dbContext.AgentAttendance.Where(x => x.Date.Value.Month == request.Date.Month && x.AgentId == request.AgentsId).ToList();

                    if (Attendances.Count != 0)
                    {
                        response.AbsentCount = Attendances.Where(x => x.IsAbsent == true).Count();
                        response.PresentCount = Attendances.Where(x => x.IsPresent == true).Count();
                        response.LateCount = Attendances.Where(x => x.IsLate == true).Count();
                        response.ExcuseCount = Attendances.Where(x => x.IsExcused == true).Count();
                        response.HolidayCount = Attendances.Where(x => x.IsHoliday == true).Count();
                        // getting individual attendance details
                        foreach (var Attendance in Attendances)
                        {
                            var AGD = new AgentAttendanceData();
                            AGD.Date = Attendance.Date;
                            AGD.IsLate = Attendance.IsLate;
                            AGD.IsExcused = Attendance.IsExcused;
                            AGD.IsPresent = Attendance.IsPresent;
                            AGD.IsAbsent = Attendance.IsAbsent;
                            AGD.IsHoliday = Attendance.IsHoliday;
                            AGD.IsAttendanceMarked = Attendance.IsAttendanceMarked;
                            AGD.ShiftId = Attendance.ShiftId;
                            AGD.ShiftName = Attendance.Shifts.Name;
                            AGD.StartDate = Attendance.StartDateTime;
                            if (Attendance.StartDateTime.HasValue)
                            {
                                if (Attendance.EndDateTime != null)
                                {
                                    AGD.EndDate = Attendance.EndDateTime;
                                    TimeSpan duration = AGD.EndDate.Value.TimeOfDay - AGD.StartDate.Value.TimeOfDay;
                                    AGD.WorkingHours = duration.TotalHours.ToString("#.##");
                                    var time = TimeSpan.FromHours(Convert.ToDouble(AGD.WorkingHours));
                                    AGD.WorkingHours = time.Hours + "h " + time.Minutes + "m";
                                }
                                else if (Attendance.StartDateTime.Value.Date == DateTime.Now.Date && DateTime.Now.TimeOfDay < Attendance.Shifts.EndTime.Value.TimeOfDay)
                                {
                                    AGD.WorkingHours = "Currently Working";
                                }
                                else
                                {
                                    TimeSpan ts = new TimeSpan(17, 30, 0);
                                    AGD.EndDate = Attendance.StartDateTime.Value.Date + ts;
                                    TimeSpan duration = AGD.EndDate.Value.TimeOfDay - AGD.StartDate.Value.TimeOfDay;
                                    AGD.WorkingHours = duration.TotalHours.ToString("#.##");
                                    var time = TimeSpan.FromHours(Convert.ToDouble(AGD.WorkingHours));
                                    AGD.WorkingHours = time.Hours + "h " + time.Minutes + "m";
                                }
                            }
                            response.AgentAttandanceList.Add(AGD);
                            response.IsSuccessful = true;

                        }
                    }
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
        public int HolidayCount { get; set; }
        public bool IsSuccessful { get; set; }
        public bool AttendanceIsEmpty { get; set; }
        public List<AgentAttendanceData> AgentAttandanceList { get; set; }
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
        public bool IsHoliday{ get; set; }
        public string WorkingHours { get; set; }
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
    }
}