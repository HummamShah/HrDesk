using AMS.Model.Enum;
using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AMS.Models.Requests.Pay
{
    public class EditPaySlipRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public string UserId { get; set; }
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string PadiBy { get; set; }
        public int DaysWorked { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int AgentId { get; set; }
        public string Location { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string Designation { get; set; }
        public string Section { get; set; }
        public string Grade { get; set; }
        public Nullable<System.DateTime> DOJ { get; set; }
        public string CNIC { get; set; }
        public string Shift { get; set; }
        public string WWID { get; set; }
        public Nullable<decimal> BasicPay { get; set; }
        public Nullable<decimal> HouseRent { get; set; }
        public Nullable<decimal> Utility { get; set; }
        public Nullable<decimal> FuelAllowance { get; set; }
        public Nullable<decimal> VehicleAllowance { get; set; }
        public Nullable<decimal> Incentive { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> EOBI { get; set; }
        public Nullable<decimal> GrossPay { get; set; }
        public Nullable<decimal> NetPay { get; set; }
        public string Status { get; set; }
        public string AgentName { get; set; }
        public string DepartmentName { get; set; }
        public Object RunRequest(EditPaySlipRequest request)
        {
            var response = new EditPaySlipResponse();
            response.ValidationErrors = new List<string>();
            response.Success = true;
            try
            {
                var PaySlip = _dbContext.PaySlip.Where(x => x.Id == request.Id).FirstOrDefault();
                if(PaySlip != null)
                {
                    //PaySlip.AgentId = request.Id;
                    PaySlip.BasicPay = request.BasicPay;
                    PaySlip.CNIC = request.CNIC;
                    PaySlip.Date =request.Date;
                    PaySlip.DaysWorked = 30;
                    PaySlip.DepartmentId = request.DepartmentId;
                    PaySlip.Designation = request.Designation;
                    PaySlip.DOJ = request.DOJ;
                    PaySlip.EmpCode = request.EmpCode;
                    PaySlip.EmpName = request.EmpName;
                    PaySlip.EOBI = request.EOBI;
                    PaySlip.FuelAllowance = request.FuelAllowance;
                    PaySlip.Grade = request.Grade;
                    PaySlip.HouseRent = request.HouseRent;
                    PaySlip.Incentive = 0;
                    PaySlip.Location = request.Location;
                    PaySlip.PadiBy = request.PadiBy;
                    PaySlip.Section = request.Section;
                    PaySlip.Shift = request.Shift;
                    PaySlip.Status = request.Status;
                    PaySlip.Tax = request.Tax;
                    PaySlip.Utility = request.Utility;
                    PaySlip.VehicleAllowance = request.VehicleAllowance;

                    PaySlip.GrossPay = request.BasicPay + request.Utility + request.VehicleAllowance + request.HouseRent + request.FuelAllowance + 0;
                    decimal tax = 0;
                    if (PaySlip.GrossPay * 12 <= 600000)
                    {
                        tax = 0;
                    }
                    else if (PaySlip.GrossPay * 12 > 600000 && PaySlip.GrossPay * 12 <= 1200000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 600000;
                        tax = taxableamount * 5 / 100 / 12 ?? 0;
                    }
                    else if (PaySlip.GrossPay * 12 > 1200000 && PaySlip.GrossPay * 12 <= 1800000)
                    {
                        decimal? taxableamount = PaySlip.GrossPay * 12 - 1200000;
                        tax = taxableamount * 10 / 100 + 30000 ?? 0;
                        tax = tax / 12;
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
                    PaySlip.NetPay = request.GrossPay - request.EOBI - tax;

                    PaySlip.WWID = request.WWID;
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
    public class EditPaySlipResponse
    {
        public bool Success { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}
