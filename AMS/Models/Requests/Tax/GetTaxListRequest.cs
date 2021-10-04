using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Models.Requests.Tax
{
    public class GetTaxListRequest
    {
        private AMSEntities _dbContext = new AMSEntities();

        public Object RunRequest(GetTaxListRequest request)
        {
            var response = new GetTaxListResponse();
            try
            {
                response.TaxesList = new List<Tax>();
                var Taxes = _dbContext.Tax.ToList();
                foreach (var tax in Taxes) {
                    var Tax = new Tax();
                    Tax.Id = tax.Id;
                    Tax.Name = tax.Name;
                    Tax.Type = tax.Type;
                    Tax.Amount = tax.Amount;
                    Tax.CreatedBy = tax.CreatedBy;
                    Tax.CreatedAt = tax.CreatedAt;
                    response.TaxesList.Add(Tax);
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
    public class GetTaxListResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public List<Tax> TaxesList { get; set; }
    }
    public class Tax
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Type{ get; set; }
        public decimal Amount{ get; set; }
        public string CreatedBy{ get; set; }
        public DateTime? CreatedAt{ get; set; }
    }
}