using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Leave
{
    public class AcceptLeaveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int LeaveId { get; set; }
        public int DaysCount{ get; set; }
        public Object RunRequest(AcceptLeaveRequest request)
        {
            var response = new AcceptLeaveResponse();
            try
            {
                var Leave = _dbContext.Leaves.Where(x => x.Id == LeaveId).FirstOrDefault();
                Leave.Agent.RemainingLeaves -= request.DaysCount;
                if (Leave.Type == 0) {
                    Leave.Agent.AnnualLeaves -= request.DaysCount;
                }
                else if (Leave.Type == 1)
                {
                    Leave.Agent.MedicalLeaves -= request.DaysCount;
                }
                Leave.AccpRejDate = DateTime.Now;
                Leave.Status = 1;
                _dbContext.SaveChanges();
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
    public class AcceptLeaveResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}