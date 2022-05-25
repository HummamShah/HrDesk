using AMS.Model.Enum;
using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AMS.Models.Requests.Pay
{
    public class GeneratePaySlipForAllRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public Object RunRequest(GeneratePaySlipForAllRequest request)
        {
            var response = new GeneratePaySlipForAllResponse();
            response.ValidationErrors = new List<string>();
            response.Success = true;
            try
            {
                var Employees = _dbContext.Agent.AsNoTracking().ToList();
                foreach(var emp in Employees)
                {
                    var PaySlip = new Model.Model.PaySlip();
                    PaySlip.AgentId = emp.Id;
                    PaySlip.BasicPay = emp.Salary;
                    PaySlip.CNIC = emp.CNIC;
                    PaySlip.Date = DateTime.Today;
                    PaySlip.DaysWorked = 30;
                    PaySlip.DepartmentId = emp.DepartmentId;
                    if(emp.Designation.HasValue)
                         PaySlip.Designation = ((Designation)emp.Designation.Value).ToString();
                    PaySlip.DOJ = emp.DateOfJoining;
                    PaySlip.EmpCode = emp.EmpCode;
                    PaySlip.EmpName = emp.FisrtName + " " + emp.LastName;
                    PaySlip.EOBI = emp.EOBIDeduction;
                    PaySlip.FuelAllowance = emp.FuelAllowance;
                    PaySlip.Grade = emp.Grade;
                    PaySlip.HouseRent = emp.HouseRentAllowance;
                    PaySlip.Incentive = 0;
                    PaySlip.Location = emp.Location;
                    PaySlip.PadiBy = "Cheque";
                    PaySlip.Section = emp.Section;
                    if (emp.Shifts != null)
                        PaySlip.Shift = emp.Shifts.Name;
                    PaySlip.Status = "Pending";
                    PaySlip.Tax = 0;
                    PaySlip.Utility = emp.Utility;
                    PaySlip.VehicleAllowance = emp.VehicleAllowance;
                    
                    PaySlip.GrossPay = emp.Salary + emp.Utility + emp.VehicleAllowance + emp.HouseRentAllowance + emp.FuelAllowance + 0;
                    decimal tax = 0;
                    if(PaySlip.GrossPay * 12 <= 600000)
                    {
                        tax = 0;
                    }else if (PaySlip.GrossPay * 12 > 600000 && PaySlip.GrossPay * 12 <= 1200000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 600000;
                        tax =  taxableamount * 5 / 100 /12??0;
                    }
                    else if(PaySlip.GrossPay * 12 > 1200000 && PaySlip.GrossPay * 12 <= 1800000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 1200000;
                        tax = taxableamount * 10 / 100  + 30000??0;
                        tax = tax/12;
                    }
                    else if (PaySlip.GrossPay * 12 > 1800000 && PaySlip.GrossPay * 12 <= 2500000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 1800000;
                        tax = taxableamount * 15 / 100 + 90000 ?? 0;
                        tax = tax / 12;
                    }
                    else if (PaySlip.GrossPay * 12 > 2500000 && PaySlip.GrossPay * 12 <= 3500000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 2500000;
                        tax = taxableamount * (decimal)17.5 / 100 + 195000 ?? 0;
                        tax = tax / 12;
                    }
                    else if (PaySlip.GrossPay * 12 > 3500000 && PaySlip.GrossPay * 12 <= 5000000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 3500000;
                        tax = taxableamount * 20 / 100 + 370000 ?? 0;
                        tax = tax / 12;
                    }
                    else if (PaySlip.GrossPay * 12 > 5000000 && PaySlip.GrossPay * 12 <= 8000000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 5000000;
                        tax = taxableamount * (decimal)22.5 / 100 + 670000 ?? 0;
                        tax = tax / 12;
                    }
                    else if (PaySlip.GrossPay * 12 > 8000000 && PaySlip.GrossPay * 12 <= 12000000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 8000000;
                        tax = taxableamount * 25 / 100 + 1345000 ?? 0;
                        tax = tax / 12;
                    }
                   
                    PaySlip.Tax = tax;
                    PaySlip.NetPay = PaySlip.GrossPay - emp.EOBIDeduction - tax;
                    
                    PaySlip.WWID = emp.WWID;
                    _dbContext.PaySlip.Add(PaySlip);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message);
            }
            return response;
        }
    }
    public class GeneratePaySlipForAllResponse
    {
        public bool Success { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}
