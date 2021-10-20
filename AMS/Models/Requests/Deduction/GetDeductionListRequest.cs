using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Deduction
{
    public class GetDeductionListRequest
    {
        private AMSEntities _dbContext = new AMSEntities();

        public Object RunRequest(GetDeductionListRequest request)
        {
            var response = new GetDeductionListResponse();
            try
            {
                response.DeductionsList = new List<Deduction>();
                var Deductions = _dbContext.Deductions.ToList();
                foreach (var deduction in Deductions) {
                    var Deduction = new Deduction();
                    Deduction.Id = deduction.Id;
                    Deduction.Name = deduction.Name;
                    Deduction.Type = deduction.Type;
                    Deduction.Amount = deduction.Amount;
                    Deduction.CreatedBy = deduction.CreatedBy;
                    Deduction.CreatedAt = deduction.CreatedAt;
                    response.DeductionsList.Add(Deduction);
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
    public class GetDeductionListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public List<Deduction> DeductionsList { get; set; }
    }
    public class Deduction
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Type{ get; set; }
        public decimal Amount{ get; set; }
        public string CreatedBy{ get; set; }
        public DateTime? CreatedAt{ get; set; }
    }
}