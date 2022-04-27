using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AMS.Models.Requests.Pay
{
    public class SavePaySlipRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string Month { get; set; } 
        public string MonthEnum { get; set; } 
        public int Year { get; set; }
        public decimal DeductionInDaysCalc { get; set; }
        public decimal BasicPay { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Utilities { get; set; }
        public decimal SalaryPerDay { get; set; }
        public decimal TotalTaxDeduction { get; set; }
        public decimal TotalDeductionsDeduction { get; set; }
        public decimal TotalIncentiveAddition { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal FinalSalary { get; set; }
        public List<SalaryDetails> TaxDeductions { get; set; } = new List<SalaryDetails>();
        public List<SalaryDetails> DeductionsDeductions { get; set; } = new List<SalaryDetails>();
        public List<SalaryDetails> IncentiveAddition { get; set; } = new List<SalaryDetails>();
        public Object RunRequest(SavePaySlipRequest request)
        {
            var response = new SavePaySlipResponse();
            try
            {
                var PaySlip = new Model.Model.Pay();
                PaySlip.AgentId = request.AgentId;
                PaySlip.Basic = request.BasicPay;
                PaySlip.HouseRent = request.HouseRent;
                PaySlip.Utilities = request.Utilities;
                PaySlip.AgentName = request.AgentName;
                PaySlip.Month = request.Month;
                PaySlip.Year = request.Year;
                PaySlip.BasicSalary = request.BasicSalary;
                PaySlip.SalaryPerDay = request.SalaryPerDay;
                PaySlip.FinalSalary = request.FinalSalary;
                PaySlip.TotalIncentiveAddition = request.TotalIncentiveAddition;
                PaySlip.TotalTaxDeduction = request.TotalTaxDeduction;
                PaySlip.TotalDeductionsDeduction = request.TotalDeductionsDeduction;
                PaySlip.GeneratedOn = DateTime.Now;
                PaySlip.GeneratedBy = _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().FisrtName + " " + _dbContext.Agent.Where(x => x.UserId == request.UserId).FirstOrDefault().LastName;
                PaySlip.MonthEnum = request.MonthEnum;
                _dbContext.Pay.Add(PaySlip);
                _dbContext.SaveChanges();

                // adding incentives
                foreach (var incentive in request.IncentiveAddition) {
                    var PayDetails = new PayDetails();
                    PayDetails.PaySlipId = PaySlip.Id;
                    PayDetails.Description = incentive.Description;
                    PayDetails.Amount = incentive.Amount;
                    PayDetails.Remarks = incentive.Remarks;
                    PayDetails.Type = "Incentive";

                    _dbContext.PayDetails.Add(PayDetails);
                    _dbContext.SaveChanges();
                }
                // adding taxes
                foreach (var tax in request.TaxDeductions)
                {
                    var PayDetails = new PayDetails();
                    PayDetails.PaySlipId = PaySlip.Id;
                    PayDetails.Description = tax.Description;
                    PayDetails.Amount = tax.Amount;
                    PayDetails.Remarks = tax.Remarks;
                    PayDetails.Type = "Tax";

                    _dbContext.PayDetails.Add(PayDetails);
                    _dbContext.SaveChanges();
                }
                // adding deductions
                foreach (var deduction in request.DeductionsDeductions)
                {
                    var PayDetails = new PayDetails();
                    PayDetails.PaySlipId = PaySlip.Id;
                    PayDetails.Description = deduction.Description;
                    PayDetails.Amount = deduction.Amount;
                    PayDetails.Remarks = deduction.Remarks;
                    PayDetails.Type = "Deduction";

                    _dbContext.PayDetails.Add(PayDetails);
                    _dbContext.SaveChanges();
                }
                //reset deduction in days to zero, once the salary is generated
                var Agent = _dbContext.Agent.Where(x => x.Id == request.AgentId).FirstOrDefault();
                Agent.DeductionInDays = 0;
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
    public class SavePaySlipResponse
    {
        public bool IsSuccessful { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}
