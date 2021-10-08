using System;
using AMS.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Incentive {
    
    public class AddIncentiveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Object RunRequest(AddIncentiveRequest request)
        {
            var response = new AddIncentiveResponse();
            try
            {
                var Incentive = new Model.Model.Incentives();
                Incentive.Name = request.Name;
                Incentive.Type = request.Type;
                Incentive.Amount = request.Amount;
                Incentive.CreatedBy = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().FisrtName + " " + _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().LastName;
                Incentive.CreatedAt = DateTime.Now;
                _dbContext.Incentives.Add(Incentive);
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
    public class AddIncentiveResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}