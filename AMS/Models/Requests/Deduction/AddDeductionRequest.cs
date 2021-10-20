using System;
using AMS.Model.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMS.Models.Requests.Deduction
{
    public class AddDeductionRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public Object RunRequest(AddDeductionRequest request)
        {
            var response = new AddDeductionResponse();
            try
            {
                var Deduction = new Model.Model.Deductions();
                Deduction.Name = request.Name;
                Deduction.Type = request.Type;
                Deduction.Amount = request.Amount;
                Deduction.CreatedBy = _dbContext.Agent.Where(x=>x.UserId == request.UserId).FirstOrDefault().FisrtName + " " + _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().LastName;
                Deduction.CreatedAt = DateTime.Now;
                _dbContext.Deductions.Add(Deduction);
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
    public class AddDeductionResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}