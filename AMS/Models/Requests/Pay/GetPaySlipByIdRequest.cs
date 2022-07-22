using AMS.Model.Model;
using AMS.Models.Requests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.Pay
{
    public class GetPaySlipByIdResponse : Response
    {
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
        public string AmountInWords { get; set; }
    }
    public class GetPaySlipByIdRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public Object RunRequest(GetPaySlipByIdRequest request)
        {
            var response = new GetPaySlipByIdResponse();
            response.ValidationErrors = new List<string>();
            response.Success = true;
            try
            {
                var Slip = _dbContext.PaySlip.AsNoTracking().Where(x => x.Id == request.Id).FirstOrDefault();
                if(Slip != null)
                {
                    response.AgentId = Slip.AgentId;
                    response.AgentName = Slip.EmpName;
                    response.BasicPay = Slip.BasicPay;
                    response.CNIC = Slip.CNIC;
                    response.Date = Slip.Date;
                    response.DaysWorked = Slip.DaysWorked;
                    response.DepartmentId = Slip.DepartmentId;
                    if (Slip.Department != null)
                         response.DepartmentName = Slip.Department.Name;
                    response.Designation = Slip.Designation; 
                    response.DOJ = Slip.DOJ;
                    response.EmpCode = Slip.EmpCode;
                    response.EmpName = Slip.EmpName;
                    response.EOBI = Slip.EOBI;
                    response.FuelAllowance = Slip.FuelAllowance;
                    response.Grade = Slip.Grade;
                    response.GrossPay = Slip.GrossPay;
                    response.HouseRent = Slip.HouseRent;
                    response.Id = Slip.Id;
                    response.Incentive = Slip.Incentive;
                    response.Location = Slip.Location;
                    response.NetPay = Slip.NetPay;
                    response.PadiBy = Slip.PadiBy;
                    response.Section = Slip.Section;
                    response.Shift = Slip.Shift;
                    response.Status = Slip.Status;
                    response.Tax = Slip.Tax;
                    response.Utility = Slip.Utility;
                    response.VehicleAllowance = Slip.VehicleAllowance;
                    response.WWID = Slip.WWID;
                    response.AmountInWords = ConvertAmount(Slip.NetPay ?? 0);
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.ValidationErrors.Add(e.Message);
            }
            return response;
        }

         
    private String[] units = { "Zero", "One", "Two", "Three",
    "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
    "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "", "", "Twenty", "Thirty", "Forty",
    "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public String ConvertAmount(decimal amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return Convert(amount_int) + " Only.";
                }
                else
                {
                    return Convert(amount_int) + " Point " + Convert(amount_dec) + " Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public String Convert(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " " + Convert(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " And " + Convert(i % 100) : "");
            }
            if (i < 100000)
            {
                return Convert(i / 1000) + " Thousand "
                        + ((i % 1000 > 0) ? " " + Convert(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return Convert(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + Convert(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return Convert(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + Convert(i % 10000000) : "");
            }
            return Convert(i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + Convert(i % 1000000000) : "");
        }
    
}
}