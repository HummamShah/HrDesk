using System;
using AMS.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Deduction
{
    public class EditDeductionRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Object RunRequest(EditDeductionRequest request)
        {
            var response = new EditDeductionResponse();
            try
            {
                var Deduction = _dbContext.Deductions.Where(x=> x.Id == request.Id).FirstOrDefault();
                Deduction.Name = request.Name;
                Deduction.Type = request.Type;
                Deduction.Amount = request.Amount;
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
    public class EditDeductionResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}