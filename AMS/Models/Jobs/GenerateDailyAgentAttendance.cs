using AMS.Model.Model;
using FluentScheduler;
using System;
using System.Linq;

namespace AMS.Models.Jobs
{
    public class GenerateDailyAgentAttendance : IJob
    {
        private AMSEntities _dbContext = new AMSEntities();
        void IJob.Execute()
        {
            var today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Friday)
            {
                var Agent = _dbContext.Agent.ToList();
                var tom = DateTime.Today.AddDays(3);
                if (_dbContext.AgentAttendance.Where(x => x.Date == tom).Count() < 1)
                {
                    foreach (var row in Agent)
                    {
                        var Data = new AMS.Model.Model.AgentAttendance();
                        Data.AgentId = row.Id;
                        Data.CreatedAt = DateTime.Now.AddDays(3);
                        Data.Date = tom;
                        Data.IsPresent = false;
                        Data.IsAttendanceMarked = false;
                        Data.IsAbsent = true;
                        Data.IsLate = false;
                        _dbContext.AgentAttendance.Add(Data);
                        _dbContext.SaveChanges();
                    }
                }
            }
            else if (today.DayOfWeek != DayOfWeek.Saturday && today.DayOfWeek != DayOfWeek.Sunday)
            {
                var Agent = _dbContext.Agent.ToList();
                var tom = DateTime.Today.AddDays(1);
                if (_dbContext.AgentAttendance.Where(x => x.Date == tom).Count() < 1)
                {
                    foreach (var row in Agent)
                    {
                        var Data = new AMS.Model.Model.AgentAttendance();
                        Data.AgentId = row.Id;
                        Data.CreatedAt = DateTime.Now.AddDays(1);
                        Data.Date = DateTime.Today.AddDays(1);
                        Data.IsPresent = false;
                        Data.IsAttendanceMarked = false;
                        Data.IsAbsent = true;
                        Data.IsLate = false;
                        _dbContext.AgentAttendance.Add(Data);
                        _dbContext.SaveChanges();
                    }
                }
            }

            if (today.DayOfWeek != DayOfWeek.Sunday && today.DayOfWeek != DayOfWeek.Monday)
            {
                var yesterday = DateTime.Today.AddDays(-1);
                var Attendance = _dbContext.AgentAttendance.Where(x => x.Date == yesterday).ToList();

                // deducting leave for yesterday's absentees
                var Absents = Attendance.Where(x => x.IsAbsent == true && x.IsAttendanceMarked == false).ToList();
                foreach (var absentAgent in Absents)
                {
                    if (absentAgent.Agent.RemainingLeaves > 0 && absentAgent.Agent.AnnualLeaves > 0)
                    {
                        absentAgent.Agent.RemainingLeaves--;
                        absentAgent.Agent.AnnualLeaves--;
                        _dbContext.SaveChanges();
                    }
                    else
                        absentAgent.Agent.DeductionInDays++;
                }

                // setting clock out time 5:30pm for non-marked
                var AttEndTime = Attendance.Where(x => x.IsAbsent == false && x.IsAttendanceMarked == true && x.EndDateTime == null).ToList();
                TimeSpan ts = new TimeSpan(17, 30, 0);
                foreach (var attendance in AttEndTime)
                {
                    attendance.EndDateTime = attendance.StartDateTime.Value.Date + ts;
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}