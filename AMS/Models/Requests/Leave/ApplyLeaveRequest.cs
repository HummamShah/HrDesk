using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Request.Leave
{
    public class ApplyLeaveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime? ApplyDate { get; set; }
        public DateTime? AccpRejDate { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string Reason { get; set; }
        public DateTime? LeaveFrom { get; set; }
        public DateTime? LeaveTo { get; set; }

        public Object RunRequest(ApplyLeaveRequest request)
        {
            var response = new ApplyLeaveResponse();
            try
            {
                var Leave = new AMS.Model.Model.Leaves();
                Leave.AgentId = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().Id;
                Leave.LeaveFrom = request.LeaveFrom;
                if (request.LeaveTo == null)
                {
                    Leave.LeaveTo = request.LeaveFrom;
                }
                else {
                    Leave.LeaveTo = request.LeaveTo;
                }
                Leave.ApplyDate = DateTime.Now;
                Leave.Reason = request.Reason;
                Leave.Status = 0;
                _dbContext.Leaves.Add(Leave);
                _dbContext.SaveChanges();

                // need to add notification for HR here
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
    public class ApplyLeaveResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}