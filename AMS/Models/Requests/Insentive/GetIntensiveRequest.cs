using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Intensive
{
    public class GetIntensiveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public Object RunRequest(GetIntensiveRequest request)
        {
            var response = new GetIntensiveResponse();
            try
            {
                var Intensive = _dbContext.Insentives.Where(x => x.Id == request.Id).FirstOrDefault();
                response.Id = Intensive.Id;
                response.Name = Intensive.Name;
                response.Type = Intensive.Type;
                response.Amount = Intensive.Amount;
                response.CreatedBy = Intensive.CreatedBy;
                response.CreatedAt = Intensive.CreatedAt;
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
    public class GetIntensiveResponse
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