using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Pay
{
    public class GetPayRollListRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string Month { get; set; }
        public int Year { get; set; }
        public Object RunRequest(GetPayRollListRequest request)
        {
            var response = new GetPayRollListResponse();
            try
            {
                var Agents = _dbContext.Agent.ToList().OrderBy( x => x.FisrtName);
                response.PayRollList = new List<AgentPayDetail>();
                foreach (var agent in Agents)
                {
                    var Agent = new AgentPayDetail();
                    Agent.Id = agent.Id;
                    Agent.FirstName = agent.FisrtName;
                    Agent.LastName = agent.LastName;
                    Agent.Salary = agent.Salary;
                    Agent.DeductionInDays = agent.DeductionInDays;
                    var Pay = _dbContext.Pay.Where(x => x.AgentId == Agent.Id && x.Month == request.Month && x.Year == request.Year).FirstOrDefault();
                    if (Pay != null) {Agent.IsGenerated = true;}
                    else {Agent.IsGenerated = false;}
                    response.PayRollList.Add(Agent);
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
    public class GetPayRollListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public List<AgentPayDetail> PayRollList { get; set; } = new List<AgentPayDetail>();
    }
    public class AgentPayDetail
    {
        public int Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public decimal Salary{ get; set; }
        public decimal DeductionInDays { get; set; }
        public bool IsGenerated { get; set; }
    }
}
