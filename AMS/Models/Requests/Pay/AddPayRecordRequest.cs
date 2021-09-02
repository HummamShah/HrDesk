using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Request.Pay
{
    public class AddPayRecordRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int AgentId { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public int Salary { get; set; }
        public DateTime GeneratedOn { get; set; }
        public string PaySlipUrl { get; set; }
        public Object RunRequest(AddPayRecordRequest request)
        {
            var response = new AddPayRecordResponse();
            try
            {
                var PayRecord = new AMS.Model.Model.Pay();
                PayRecord.AgentId = request.AgentId;
                PayRecord.Month = request.Month;
                PayRecord.Year = request.Year;
                PayRecord.Salary = request.Salary;
                PayRecord.GeneratedOn = request.GeneratedOn;
                PayRecord.PaySlipUrl = request.PaySlipUrl;
                _dbContext.Pay.Add(PayRecord);
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
    public class AddPayRecordResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}

