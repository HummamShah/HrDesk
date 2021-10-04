using System;
using AMS.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Tax
{
    public class EditTaxRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Object RunRequest(EditTaxRequest request)
        {
            var response = new EditTaxResponse();
            try
            {
                var Tax = _dbContext.Tax.Where(x=> x.Id == request.Id).FirstOrDefault();
                Tax.Name = request.Name;
                Tax.Type = request.Type;
                Tax.Amount = request.Amount;
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
    public class EditTaxResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}