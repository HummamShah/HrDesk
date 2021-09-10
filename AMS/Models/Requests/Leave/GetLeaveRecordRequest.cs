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
                var LeaveRecord = _dbContext.Leaves.Where(x => x.Agent.UserId == request.UserId).ToList();
                response.LeaveRecordList = new List<LeaveDetails>();
                foreach (var Leave in LeaveRecord) {
                    var leave = new LeaveDetails();
                    leave.Id = Leave.Id;
                    leave.ApplyDate = Leave.ApplyDate;
                    leave.LeaveFrom = Leave.LeaveFrom;
                    leave.LeaveTo = Leave.LeaveTo;
                    leave.Status = Leave.Status;
                    leave.Type = Leave.Type;
                    if (leave.Type.HasValue)
                    {
                        leave.TypeEnum = ((LeaveType)Leave.Type.Value).ToString();
                    }
                    leave.Reason = Leave.Reason;
                    leave.AccpRejDate = Leave.AccpRejDate;
                    response.LeaveRecordList.Add(leave);
                }

                response.IsSuccessful = true;
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
        public DateTime? LeaveFrom { get; set; }
        public DateTime? LeaveTo{ get; set; }
        public string TypeEnum{ get; set; }
    }
}