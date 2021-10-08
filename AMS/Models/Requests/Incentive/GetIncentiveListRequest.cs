using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Incentive
{
    public class GetIncentiveListRequest
    {
        private AMSEntities _dbContext = new AMSEntities();

        public Object RunRequest(GetIncentiveListRequest request)
        {
            var response = new GetIncentiveListResponse();
            try
            {
                response.IncentiveList = new List<Incentive>();
                var Incentives = _dbContext.Incentives.ToList();
                foreach (var incentive in Incentives)
                {
                    var Incentive = new Incentive();
                    Incentive.Id = incentive.Id;
                    Incentive.Name = incentive.Name;
                    Incentive.Type = incentive.Type;
                    Incentive.Amount = incentive.Amount;
                    Incentive.CreatedBy = incentive.CreatedBy;
                    Incentive.CreatedAt = incentive.CreatedAt;
                    response.IncentiveList.Add(Incentive);
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
    public class GetIncentiveListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public List<Incentive> IncentiveList { get; set; }
    }
    public class Incentive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}