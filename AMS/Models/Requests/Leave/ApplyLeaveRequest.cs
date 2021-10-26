using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.Model.Request.Leave
{
    public class ApplyLeaveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? Type { get; set; }
        public string Reason { get; set; }
        public DateTime LeaveFrom { get; set; }
        public DateTime LeaveTo { get; set; }

        public Object RunRequest(ApplyLeaveRequest request)
        {
            var response = new ApplyLeaveResponse();
            try
            {
                var Leave = new AMS.Model.Model.Leaves();
                var Agent = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault();
                var RemainingLeaves = Agent.RemainingLeaves;
                var AnnualLeaves = Agent.AnnualLeaves;
                var MedicalLeaves = Agent.MedicalLeaves;

                // getting individual dates from range (exluding weekends)
                List<DateTime> allDates = new List<DateTime>();
                for (DateTime date = request.LeaveFrom; date <= request.LeaveTo; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != DayOfWeek.Sunday && date.DayOfWeek != DayOfWeek.Saturday)
                    {
                        allDates.Add(date);
                    }
                }

                // validation of duplicate leave application
                var AlreadyAppliedLeaves = _dbContext.LeaveDates.Where(x => x.Leaves.AgentId == Agent.Id && x.Leaves.Status != 2).ToList();
                foreach (var alreadyAppliedLeave in AlreadyAppliedLeaves) {
                    foreach (var singleDay in allDates ) {
                        if (singleDay == alreadyAppliedLeave.Dates) {
                            response.IsSuccessful = false;
                            response.ValidationErrors.Add("You have alaready applied for " + singleDay.ToShortDateString() + " before!");
                            return response;
                        }
                    }
                }

                // checking if agent has remaining leaves
                if (RemainingLeaves == 0)
                {
                    response.ValidationErrors.Add("You can't apply for leave, you have availed all of your leaves");
                }
                else if (RemainingLeaves < allDates.Count)     // may not need this condition becusae the next two blocks are already checking this condition
                {
                    response.ValidationErrors.Add("You have only " + RemainingLeaves + " leaves left");
                }
                else if (request.Type == 0 && AnnualLeaves < allDates.Count) 
                {
                    response.ValidationErrors.Add("You applied for " + allDates.Count + " days but you have only " + AnnualLeaves + " annual leave(s) left");
                }
                else if (request.Type == 1 && MedicalLeaves < allDates.Count)
                {
                    response.ValidationErrors.Add("You applied for " + allDates.Count + " days but you have only " + MedicalLeaves + " medical leave(s) left");
                }
                else {
                    Leave.AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
                    Leave.LeaveFrom = request.LeaveFrom;
                    if (request.LeaveTo == null)
                    {
                        Leave.LeaveTo = request.LeaveFrom;
                    }
                    else
                    {
                        Leave.LeaveTo = request.LeaveTo;
                    }
                    Leave.ApplyDate = DateTime.Now;
                    Leave.Reason = request.Reason;
                    Leave.Type = request.Type;
                    Leave.Status = 0;
                    Leave.DaysCount = allDates.Count;
                    _dbContext.Leaves.Add(Leave);
                    _dbContext.SaveChanges();

                    // saving dates in LeaveDate table
                    var LeaveDates = new AMS.Model.Model.LeaveDates();
                    foreach (var date in allDates)
                    {
                        LeaveDates.Dates = date;
                        LeaveDates.LeaveId = Leave.Id;
                        _dbContext.LeaveDates.Add(LeaveDates);
                        _dbContext.SaveChanges();
                    }
                    
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
    public class ApplyLeaveResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}