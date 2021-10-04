using System;
using AMS.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Intensive
{
    public class AddIntensiveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Object RunRequest(AddIntensiveRequest request)
        {
            var response = new AddIntensiveResponse();
            try
            {
                var Intensive = new Model.Model.Insentives();
                Intensive.Name = request.Name;
                Intensive.Type = request.Type;
                Intensive.Amount = request.Amount;
                Intensive.CreatedBy = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().FisrtName + " " + _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().LastName;
                Intensive.CreatedAt = DateTime.Now;
                _dbContext.Insentives.Add(Intensive);
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
    public class AddIntensiveResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}