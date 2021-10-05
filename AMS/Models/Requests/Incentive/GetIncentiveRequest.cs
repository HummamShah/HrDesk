using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Incentive
{
    public class GetIncentiveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public Object RunRequest(GetIncentiveRequest request)
        {
            var response = new GetIncentiveResponse();
            try
            {
                var Incentive = _dbContext.Incentives.Where(x => x.Id == request.Id).FirstOrDefault();
                response.Id = Incentive.Id;
                response.Name = Incentive.Name;
                response.Type = Incentive.Type;
                response.Amount = Incentive.Amount;
                response.CreatedBy = Incentive.CreatedBy;
                response.CreatedAt = Incentive.CreatedAt;
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
    public class GetIncentiveResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}