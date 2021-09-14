using AMS.Model.Enum;
using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Request.Leave
{
    public class GetLeaveRecordRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Object RunRequest(GetLeaveRecordRequest request)
        {
            var response = new GetLeaveHistoryResponse();
            try
            {
                var id = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
                var AgentLeave = _dbContext.Leaves.Where(x => x.AgentId == id).FirstOrDefault();
                
                if (AgentLeave != null)
                {
                    response.LeaveRecordList = new List<LeaveDetails>();
                    var LeaveId = AgentLeave.Id;
                    var LeaveRecord = _dbContext.LeaveDates.Where(x => x.LeaveId == LeaveId && x.Dates.Value.Month == request.Month && x.Dates.Value.Year == request.Year).ToList();

                    
                    foreach (var Leave in LeaveRecord)
                    {
                        var leave = new LeaveDetails();
                        leave.Id = Leave.Id;
                        leave.ApplyDate = Leave.Leaves.ApplyDate;
                        //leave.LeaveFrom = Leave.Leaves.LeaveFrom;
                        //leave.LeaveTo = Leave.Leaves.LeaveTo;
                        leave.LeaveDate = Leave.Dates;
                        leave.Status = Leave.Leaves.Status;
                        leave.Type = Leave.Leaves.Type;
                        if (leave.Type.HasValue)
                        {
                            leave.TypeEnum = ((LeaveType)Leave.Leaves.Type.Value).ToString();
                        }
                        leave.Reason = Leave.Leaves.Reason;
                        leave.AccpRejDate = Leave.Leaves.AccpRejDate;
                        response.LeaveRecordList.Add(leave);
                    }

                    response.IsSuccessful = true;
                }
                else {
                    response.IsSuccessful = false;
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
    public class GetLeaveHistoryResponse
    {
        public bool IsSuccessful { get; set; }
        public List<LeaveDetails> LeaveRecordList { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
    public class LeaveDetails
    {
        public int Id { get; set; }
        public DateTime? ApplyDate { get; set; }
        public DateTime? AccpRejDate { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string Reason { get; set; }
        //public DateTime? LeaveFrom { get; set; }
        //public DateTime? LeaveTo{ get; set; }
        public DateTime? LeaveDate { get; set; }
        public string TypeEnum{ get; set; }
    }
}