using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Intensive
{
    public class GetIntensiveListRequest
    {
        private AMSEntities _dbContext = new AMSEntities();

        public Object RunRequest(GetIntensiveListRequest request)
        {
            var response = new GetIntensiveListResponse();
            try
            {
                response.IntensivesList = new List<Intensive>();
                var Intensives = _dbContext.Insentives.ToList();
                foreach (var intensive in Intensives)
                {
                    var Intensive = new Intensive();
                    Intensive.Id = intensive.Id;
                    Intensive.Name = intensive.Name;
                    Intensive.Type = intensive.Type;
                    Intensive.Amount = intensive.Amount;
                    Intensive.CreatedBy = intensive.CreatedBy;
                    Intensive.CreatedAt = intensive.CreatedAt;
                    response.IntensivesList.Add(Intensive);
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
    public class GetIntensiveListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public List<Intensive> IntensivesList { get; set; }
    }
    public class Intensive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}