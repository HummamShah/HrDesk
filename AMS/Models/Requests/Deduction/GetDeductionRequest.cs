using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Deduction
{
    public class GetDeductionRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id{ get; set; }
        public Object RunRequest(GetDeductionRequest request)
        {
            var response = new GetDeductionResponse();
            try
            {
                var Deduction = _dbContext.Deductions.Where(x => x.Id == request.Id).FirstOrDefault();
                response.Id = Deduction.Id;
                response.Name = Deduction.Name;
                response.Type = Deduction.Type;
                response.Amount = Deduction.Amount;
                response.CreatedBy = Deduction.CreatedBy;
                response.CreatedAt = Deduction.CreatedAt;
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
    public class GetDeductionResponse
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