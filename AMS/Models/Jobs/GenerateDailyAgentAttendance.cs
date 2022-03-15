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
                var Agent = _dbContext.Agent.Where(x=> x.IsActive == false).ToList();
                var tom = DateTime.Today.AddDays(3);
                if (_dbContext.AgentAttendance.Where(x => x.Date == tom).Count() < 1)
                {
                    foreach (var row in Agent)
                    {
                        var Data = new AgentAttendance();
                        Data.AgentId = row.Id;
                        Data.CreatedAt = DateTime.Now.AddDays(3);
                        Data.Date = tom;
                        Data.IsPresent = false;
                        Data.IsAttendanceMarked = false;
                        Data.IsAbsent = true;
                        Data.IsLate = false;
                        Data.ShiftId = row.ShiftId;
                        _dbContext.AgentAttendance.Add(Data);
                        _dbContext.SaveChanges();
                    }
                }
            }
            else if (today.DayOfWeek != DayOfWeek.Saturday && today.DayOfWeek != DayOfWeek.Sunday)
            {
                var Agent = _dbContext.Agent.Where(x => x.IsActive == false).ToList();
                var tom = DateTime.Today.AddDays(1);
                if (_dbContext.AgentAttendance.Where(x => x.Date == tom).Count() < 1)
                {
                    foreach (var row in Agent)
                    {
                        var Data = new AgentAttendance();
                        Data.AgentId = row.Id;
                        Data.CreatedAt = DateTime.Now.AddDays(1);
                        Data.Date = DateTime.Today.AddDays(1);
                        Data.IsPresent = false;
                        Data.IsAttendanceMarked = false;
                        Data.IsAbsent = true;
                        Data.IsLate = false;
                        Data.ShiftId = row.ShiftId;
                        _dbContext.AgentAttendance.Add(Data);
                        _dbContext.SaveChanges();
                    }
                }
            }

            if (today.DayOfWeek != DayOfWeek.Sunday && today.DayOfWeek != DayOfWeek.Monday)
            {
                var yesterday = DateTime.Today.AddDays(-1);
                var Attendance = _dbContext.AgentAttendance.Where(x => x.Date == yesterday).ToList();

                // marking holiday for yesterday
                var wasYesterdayHoliday = _dbContext.Holidays.Where(x => x.Date == yesterday).FirstOrDefault();
                if (wasYesterdayHoliday != null)
                {
                    foreach (var attendance in Attendance) {
                        attendance.IsHoliday = true;
                        attendance.IsAbsent = false;
                        attendance.IsAttendanceMarked = true;
                        _dbContext.SaveChanges();
                    }
                    return;
                }

                // deducting leave for yesterday's absentees
                var Absentees = Attendance.Where(x => x.IsAbsent == true).ToList();
                foreach (var absentAgent in Absentees)
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

                // setting clock out time for non-marked according to their shift's timing
                var UnMarkedEndTimeAttendance = Attendance.Where(x => x.IsAbsent == false && x.IsAttendanceMarked == true && x.EndDateTime == null).ToList();
                foreach (var attendance in UnMarkedEndTimeAttendance)
                {
                    attendance.EndDateTime = attendance.Agent.Shifts.EndTime;
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}