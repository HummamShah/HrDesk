using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Requests.Report
{
    public class GetAttendanceReportResponse
    {
        public List<AgentAttendanceData> Data { get; set; }
        public List<DateTime?> Dates { get; set; }
    }
    public class AttendanceDetails
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public bool IsPresent { get; set; }
        public bool IsExcused { get; set; }
        public bool IsLate { get; set; }
        public bool IsAbsent { get; set; }
        public int DeductionInDays { get; set; }
        public int ConsecutiveLateCounter { get; set; }
        public int RemainingLeaves { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class AgentAttendanceData
    {
        public DateTime? Date { get; set; }
        public string AgentName { get; set; }
        public int TotalConsecutiveLateCounter { get; set; }
        public int TotalRemainingLeaves { get; set; }
        public int TotalDeductionInDays { get; set; }
        public List<AttendanceDetails> Details { get; set; }
    }
    public class GetAttendanceReportRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public object RunRequest(GetAttendanceReportRequest req)
        {
            var response = new GetAttendanceReportResponse();
            response.Data = new List<AgentAttendanceData>();
            response.Dates = new List<DateTime?>();
            var Attendances = _dbContext.AgentAttendance.Where(x => x.Date >= req.DateFrom && x.Date <= req.DateTo).OrderBy(d => d.Date).GroupBy(z => z.Agent).ToList();
            var AttendanceDate = _dbContext.AgentAttendance.Where(x => x.Date >= req.DateFrom && x.Date <= req.DateTo).GroupBy(z => z.Date).ToList();
            foreach (var Dates in AttendanceDate)
            {
                response.Dates.Add(Dates.Key.Value);
            }
            foreach (var AttendanceGroup in Attendances)
            {
                var Row = new AgentAttendanceData();
                Row.Details = new List<AttendanceDetails>();
                if (AttendanceGroup.Key != null)
                {
                    Row.AgentName = AttendanceGroup.Key.FisrtName + " " + AttendanceGroup.Key.LastName;
                    foreach (var attendance in AttendanceGroup)
                    {
                        var AgentAttendance = new AttendanceDetails();
                        AgentAttendance.Id = attendance.Id;
                        AgentAttendance.AgentId = attendance.AgentId;
                        AgentAttendance.AgentName = attendance.Agent.FisrtName + " " + attendance.Agent.LastName;
                        AgentAttendance.Date = attendance.Date;
                        AgentAttendance.EndTime = attendance.EndDateTime;
                        AgentAttendance.IsLate = attendance.IsLate;
                        AgentAttendance.IsExcused = attendance.IsExcused;
                        AgentAttendance.IsPresent = attendance.IsPresent;
                        AgentAttendance.IsAbsent = attendance.IsAbsent;
                        Row.Details.Add(AgentAttendance);
                    }
                    Row.TotalConsecutiveLateCounter = AttendanceGroup.Key.ConsecutiveLateCounter;
                    Row.TotalDeductionInDays = AttendanceGroup.Key.DeductionInDays;
                    Row.TotalRemainingLeaves = AttendanceGroup.Key.RemainingLeaves;
                }
                response.Data.Add(Row);
            }
            return response;

        }
    }
}
