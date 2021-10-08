using System;
using AMS.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Incentive
{
    public class EditIncentiveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Object RunRequest(EditIncentiveRequest request)
        {
            var response = new EditIncentiveResponse();
            try
            {
                var Incentive = _dbContext.Incentives.Where(x => x.Id == request.Id).FirstOrDefault();
                Incentive.Name = request.Name;
                Incentive.Type = request.Type;
                Incentive.Amount = request.Amount;
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
    public class EditIncentiveResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}