using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Leave
{
    public class RejectLeaveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int LeaveId  { get; set; }
        public Object RunRequest(RejectLeaveRequest request)
        {
            var response = new RejectLeaveResponse();
            try
            {
                var Leave = _dbContext.Leaves.Where(x => x.Id == LeaveId).FirstOrDefault();
                Leave.Status = 2;
                Leave.AccpRejDate = DateTime.Now;
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
    public class RejectLeaveResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}