using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMS.Models.Requests.Pay
{
    public class GeneratePaySlipRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public Object RunRequest(GeneratePaySlipRequest request)
        {
            var response = new GeneratePaySlipResponse();
            try
            {
                var Agent = _dbContext.Agent.Where(x => x.Id == request.Id).FirstOrDefault();

                // deduction for taxes
                var AgentTaxes = _dbContext.AgentTaxes.Where(x => x.AgentId == request.Id).ToList();
                foreach (var tax in AgentTaxes) {
                    var deductionDetails = new SalaryDetails();
                    deductionDetails.Description = tax.TaxName;
                    if (tax.Tax.Type == "Percentage")
                    {
                        deductionDetails.Amount = (tax.Tax.Amount / 100) * Agent.Salary;
                    }
                    else if (tax.Tax.Type == "Fixed Amount")
                    {
                        deductionDetails.Amount = tax.Tax.Amount;
                    }
                    
                    Agent.Salary = Agent.Salary - deductionDetails.Amount;
                    response.TaxDeductions.Add(deductionDetails);
                    response.TotalTaxDeduction += deductionDetails.Amount;
                }

                // addition of incentives
                var AgentIncentives = _dbContext.AgentIncentives.Where(x => x.AgentId == request.Id).ToList();
                foreach (var incentive in AgentIncentives)
                {
                    var additionDetails = new SalaryDetails();
                    additionDetails.Description = incentive.IncentiveName;
                    if (incentive.Incentives.Type == "Percentage") {
                        additionDetails.Amount = (incentive.Incentives.Amount / 100) * Agent.Salary;
                    }
                    else if (incentive.Incentives.Type == "Fixed Amount") {
                        additionDetails.Amount = incentive.Incentives.Amount;
                    }
                    
                    Agent.Salary = Agent.Salary + additionDetails.Amount;
                    response.IncentiveAddition.Add(additionDetails);
                    response.TotalIncentiveAddition += additionDetails.Amount;
                }

                // deduction for absent days
                response.SalaryPerDay = Agent.Salary/30;
                if (Agent.DeductionInDays > 0)
                {
                    response.DeductionInDaysCalc = response.SalaryPerDay * Agent.DeductionInDays;
                    Agent.Salary = Agent.Salary - (response.DeductionInDaysCalc);
                }
                response.FinalSalary = Agent.Salary;
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
    public class GeneratePaySlipResponse
    {
        public bool IsSuccessful { get; set; }
        public decimal DeductionInDaysCalc { get; set; }
        public decimal SalaryPerDay { get; set; }
        public decimal TotalTaxDeduction { get; set; }
        public decimal TotalIncentiveAddition { get; set; }
        public decimal FinalSalary { get; set; }
        public List<SalaryDetails> TaxDeductions { get; set; } = new List<SalaryDetails>();
        public List<SalaryDetails> IncentiveAddition { get; set; } = new List<SalaryDetails>();
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }

    public class SalaryDetails {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount{ get; set; }
    }
}
