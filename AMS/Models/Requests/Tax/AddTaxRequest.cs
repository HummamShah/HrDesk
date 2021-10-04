using System;
using AMS.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Tax
{
    public class AddTaxRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Object RunRequest(AddTaxRequest request)
        {
            var response = new AddTaxResponse();
            try
            {
                var Tax = new Model.Model.Tax();
                Tax.Name = request.Name;
                Tax.Type = request.Type;
                Tax.Amount = request.Amount;
                Tax.CreatedBy = _dbContext.Agent.Where(x=>x.UserId == request.UserId).FirstOrDefault().FisrtName + " " + _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().LastName;
                Tax.CreatedAt = DateTime.Now;
                _dbContext.Tax.Add(Tax);
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
    public class AddTaxResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}