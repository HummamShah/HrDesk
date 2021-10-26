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

                // validation that the HR should not accept the leaves of passed dates
                if (Leave.LeaveFrom <= DateTime.Now.Date)
                {
                    response.IsSuccessful = false;
                    response.ValidationErrors.Add("The date has already passed");
                    return response;
                }

                // validation that the user has enough leaves left (it has already been done at the time of applying leave by the employee
                // but needs to be checked again here beucase of multiple pending requests)
                if (Leave.Type == 0) {
                    if (Leave.Agent.AnnualLeaves >= request.DaysCount)
                    {
                        Leave.Agent.AnnualLeaves -= request.DaysCount;
                    }
                    else
                    {
                        response.IsSuccessful = false;
                        response.ValidationErrors.Add("The employee has only " + Leave.Agent.AnnualLeaves + " annual leaves left!");
                        return response;
                    }
                }
                else if (Leave.Type == 1)
                {
                    if (Leave.Agent.MedicalLeaves >= request.DaysCount)
                    {
                        Leave.Agent.MedicalLeaves -= request.DaysCount;
                    }
                    else
                    {
                        response.IsSuccessful = false;
                        response.ValidationErrors.Add("The employee has only " + Leave.Agent.MedicalLeaves + " medical leaves left!");
                        return response;
                    }
                }
                Leave.Agent.RemainingLeaves -= request.DaysCount;
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
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}