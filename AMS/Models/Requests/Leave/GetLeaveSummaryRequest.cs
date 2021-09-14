using AMS.Model.Enum;
using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AMS.Models.Requests.Leave
{
    public class GetLeaveSummaryRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Object RunRequest(GetLeaveSummaryRequest request)
        {
            var response = new GetLeaveSummaryResponse();
            try
            {
                var id = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
                var LeaveDates = _dbContext.LeaveDates.Where(x => x.Leaves.AgentId == id).OrderBy(x => x.Dates).ToList();
                if (LeaveDates != null)
                {
                    LeaveDates = LeaveDates.Where(x => x.Dates.Value.Month == request.Month + 1 && x.Dates.Value.Year == request.Year).ToList();
                    if (LeaveDates.Count != 0)
                    {
                        response.AnnualLeaves = LeaveDates.Where(x => x.Leaves.Type == 0).Count();
                        response.MedicalLeaves = LeaveDates.Where(x => x.Leaves.Type == 1).Count();
                        response.IsSuccessful = true;
                        response.LeaveRecordList = new List<LeaveDetails>();
                        foreach(var leave in LeaveDates) 
                        {
                            var leaveRow = new LeaveDetails();
                            leaveRow.Id = leave.LeaveId;
                            leaveRow.LeaveDate = leave.Dates;
                            leaveRow.ApplyDate = leave.Leaves.ApplyDate;
                            leaveRow.AccpRejDate = leave.Leaves.AccpRejDate;
                            leaveRow.Status = leave.Leaves.Status;
                            leaveRow.StatusEnum = ((LeaveStatus)leave.Leaves.Status.Value).ToString();
                            leaveRow.Reason = leave.Leaves.Reason;
                            leaveRow.Type = leave.Leaves.Type;
                            leaveRow.TypeEnum = ((LeaveType)leave.Leaves.Type.Value).ToString();
                            
                            response.LeaveRecordList.Add(leaveRow);
                        }
                    }
                    else
                    {
                        response.LeaveIsEmpty = true;
                        response.IsSuccessful = true;
                    }
                }
                else
                {
                    response.LeaveIsEmpty = true;
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
    public class GetLeaveSummaryResponse
    {
        public int RemainingLeaves { get; set; }
        public int AnnualLeaves { get; set; }
        public int MedicalLeaves { get; set; }
        public bool IsSuccessful { get; set; }
        public bool LeaveIsEmpty { get; set; }
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
        public string TypeEnum { get; set; }
        public string StatusEnum { get; set; }
    }
}