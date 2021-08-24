using AMS.Model.Model;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Jobs
{
    public class GenerateDailyAgentAttendance : IJob
    {
        private AMSEntities _dbContext = new AMSEntities();
        //Generate List of Agent Attendance.
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
                        var result = _dbContext.AgentAttendance.Add(Data);
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
                        var result = _dbContext.AgentAttendance.Add(Data);
                        _dbContext.SaveChanges();


                    }
                }
            }
        }

    }
}