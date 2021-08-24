using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Requests.AgentAttendance
{
    public class AddAgentExcuseResponse
    {
        public List<string> ValidationErrors { get; set; }
    }
    public class AddAgentExcuseRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public string Remarks { get; set; }

        public object RunRequest(AddAgentExcuseRequest req)
        {
            var response = new AddAgentExcuseResponse();
            var AgentAttendance = _dbContext.AgentAttendance.Where(x => x.Id == req.Id).FirstOrDefault();

            // undo the changes that were done at the time of login becuase of being late
            if (AgentAttendance.Agent.ConsecutiveLateCounter % 3 == 0)
            {
                if (AgentAttendance.Agent.DeductionInDays == 0)
                    AgentAttendance.Agent.RemainingLeaves++;
                else
                    AgentAttendance.Agent.DeductionInDays--;
            }

            AgentAttendance.IsExcused = true;
            AgentAttendance.IsLate = false;
            AgentAttendance.Agent.ConsecutiveLateCounter--;
            AgentAttendance.Remarks = req.Remarks;

            _dbContext.SaveChanges();
            return response;
        }
    }
}
