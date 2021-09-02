using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Model.Request.Pay
{
    public class GetPayRecordRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public Object RunRequest(GetPayRecordRequest request)
        {
            var response = new GetPayRecordResponse();
            try
            {
                var PayRecord = _dbContext.Pay.Where(x => x.Agent.UserId == request.UserId).ToList();
                response.PayRecordList = new List<PayDetails>();
                foreach (var Pay in PayRecord)
                {
                    var pay = new PayDetails();
                    pay.Id = Pay.Id;
                    pay.Month = Pay.Month;
                    pay.Year = Pay.Year;
                    pay.PaySlipUrl = Pay.PaySlipUrl;
                    pay.GeneratedOn = Pay.GeneratedOn;
                    pay.Salary = Pay.Salary;
                    response.PayRecordList.Add(pay);
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
    public class GetPayRecordResponse
    {
        public bool IsSuccessful { get; set; }
        public List<PayDetails> PayRecordList { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
    public class PayDetails
    {
        public int Id { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public string PaySlipUrl{ get; set; }
        public DateTime? GeneratedOn { get; set; }
        public int? Salary { get; set; }

    }
}
