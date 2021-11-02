using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AMS.Models.Requests.Pay
{
    public class GeneratePaySlipRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public Object RunRequest(GeneratePaySlipRequest request)
        {
            var response = new GeneratePaySlipResponse();
            try
            {
                var Agent = _dbContext.Agent.Where(x => x.Id == request.Id).FirstOrDefault();
                response.AgentId = request.Id;
                response.AgentName = Agent.FisrtName + " " + Agent.LastName;
                response.Month = request.Month;     //DateTime.Now.Month;
                response.MonthEnum = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Int16.Parse(request.Month)+1);
                response.Year = request.Year;
                response.BasicSalary = Agent.Salary;
                response.SalaryPerDay = Agent.Salary / 30;
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

                // deduction for deductions
                var AgentDeductions = _dbContext.AgentDeductions.Where(x => x.AgentId == request.Id).ToList();
                var deductionDueToDays = new SalaryDetails();
                if (Agent.DeductionInDays > 0)
                {
                    response.DeductionInDaysCalc = response.SalaryPerDay * Agent.DeductionInDays;
                    deductionDueToDays.Description = "Deduction in Days";
                    deductionDueToDays.Amount = response.SalaryPerDay * Agent.DeductionInDays;
                    Agent.Salary = Agent.Salary - (response.DeductionInDaysCalc);
                    response.DeductionsDeductions.Add(deductionDueToDays);
                    response.TotalDeductionsDeduction += deductionDueToDays.Amount;
                }
                foreach (var deduction in AgentDeductions)
                {
                    var deductionDetails = new SalaryDetails();
                    deductionDetails.Description = deduction.DeductionName;

                    if (deduction.Deductions.Type == "Percentage"){deductionDetails.Amount = (deduction.Deductions.Amount / 100) * Agent.Salary;}
                    else if (deduction.Deductions.Type == "Fixed Amount"){deductionDetails.Amount = deduction.Deductions.Amount;}

                    Agent.Salary = Agent.Salary - deductionDetails.Amount;
                    response.DeductionsDeductions.Add(deductionDetails);
                    response.TotalDeductionsDeduction += deductionDetails.Amount;
                }

                // addition of incentives
                var AgentIncentives = _dbContext.AgentIncentives.Where(x => x.AgentId == request.Id).ToList();
                foreach (var incentive in AgentIncentives)
                {
                    var additionDetails = new SalaryDetails();
                    additionDetails.Description = incentive.IncentiveName;
                    if (incentive.Incentives.Type == "Percentage") {additionDetails.Amount = (incentive.Incentives.Amount / 100) * Agent.Salary;}
                    else if (incentive.Incentives.Type == "Fixed Amount") {additionDetails.Amount = incentive.Incentives.Amount;}
                    
                    Agent.Salary = Agent.Salary + additionDetails.Amount;
                    response.IncentiveAddition.Add(additionDetails);
                    response.TotalIncentiveAddition += additionDetails.Amount;
                }

                response.FinalSalary = Agent.Salary;
                var Pay = _dbContext.Pay.Where(x => x.AgentId == Agent.Id && x.Month == request.Month && x.Year == request.Year).FirstOrDefault();
                if (Pay != null){response.IsGenerated = true;}
                else{response.IsGenerated = false;}
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
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string Month { get; set; }
        public string MonthEnum { get; set; }
        public int Year { get; set; }
        public decimal DeductionInDaysCalc { get; set; }
        public decimal SalaryPerDay { get; set; }
        public decimal TotalTaxDeduction { get; set; }
        public decimal TotalDeductionsDeduction { get; set; }
        public decimal TotalIncentiveAddition { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal FinalSalary { get; set; }
        public bool IsGenerated { get; set; }
        public List<SalaryDetails> TaxDeductions { get; set; } = new List<SalaryDetails>();
        public List<SalaryDetails> DeductionsDeductions { get; set; } = new List<SalaryDetails>();
        public List<SalaryDetails> IncentiveAddition { get; set; } = new List<SalaryDetails>();
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }

    public class SalaryDetails {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount{ get; set; }
        public string Remarks{ get; set; }
    }
}
