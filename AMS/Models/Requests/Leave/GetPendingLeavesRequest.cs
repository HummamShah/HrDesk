using AMS.Model.Enum;
using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AMS.Models.Requests.Leave
{
    public class GetPendingLeavesRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public Object RunRequest(GetPendingLeavesRequest request)
        {
            var response = new GetPendingLeavesResponse();
            try
            {
                var Leaves = _dbContext.Leaves.Where(x => x.Status == 0).OrderBy(x=>x.ApplyDate).ToList();
                response.PendingLeavesList = new List<PendigLeaveDetails>();
                foreach (var leave in Leaves) {
                    var pendingLeave = new PendigLeaveDetails();
                    pendingLeave.Id = leave.Id;
                    pendingLeave.AgentId = leave.AgentId;
                    pendingLeave.AgentName = leave.Agent.FisrtName + " " + leave.Agent.LastName;
                    pendingLeave.RemainingLeaves = leave.Agent.RemainingLeaves;
                    pendingLeave.AnnualLeaves = leave.Agent.AnnualLeaves;
                    pendingLeave.MedicalLeaves = leave.Agent.MedicalLeaves;
                    pendingLeave.Status = leave.Status;
                    pendingLeave.StatusEnum = ((LeaveStatus)leave.Status.Value).ToString();
                    pendingLeave.Type = leave.Type;
                    pendingLeave.TypeEnum = ((LeaveType)leave.Type.Value).ToString();
                    pendingLeave.Reason = leave.Reason;
                    pendingLeave.ApplyDate = leave.ApplyDate;
                    pendingLeave.LeaveFrom = leave.LeaveFrom;
                    pendingLeave.LeaveTo = leave.LeaveTo;
                    pendingLeave.DaysCount = leave.DaysCount;

                    response.PendingLeavesList.Add(pendingLeave);
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
    public class GetPendingLeavesResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public List<PendigLeaveDetails> PendingLeavesList { get; set; }
    }
    public class PendigLeaveDetails
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public string AgentName { get; set; }
        public int? RemainingLeaves { get; set; }
        public int? AnnualLeaves { get; set; }
        public int? MedicalLeaves { get; set; }
        public int? Status { get; set; }
        public string StatusEnum { get; set; }
        public int? Type { get; set; }
        public string TypeEnum { get; set; }
        public string Reason { get; set; }
        public DateTime? ApplyDate { get; set; }
        public DateTime? LeaveFrom { get; set; }
        public DateTime? LeaveTo { get; set; }
        public int? DaysCount { get; set; }
    }
}