using System;
using AMS.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Intensive
{
    public class EditIntensiveRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Object RunRequest(EditIntensiveRequest request)
        {
            var response = new EditIntensiveResponse();
            try
            {
                var Intensive = _dbContext.Insentives.Where(x => x.Id == request.Id).FirstOrDefault();
                Intensive.Name = request.Name;
                Intensive.Type = request.Type;
                Intensive.Amount = request.Amount;
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
    public class EditIntensiveResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}