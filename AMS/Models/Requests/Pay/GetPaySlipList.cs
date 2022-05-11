using AMS.Model.Model;
using AMS.Models.Requests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.Pay
{
    public class GetPaySlipListResponse : Response
    {
       public List<PaySlipData> Data { get; set; }
    }
    public class PaySlipData
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
    }
    public class GetPaySlipListRequest
    {
        private AMSEntities _dbContext = new AMSEntities();
        public int Id { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public Object RunRequest(GetPaySlipListRequest request)
        {
            var response = new GetPaySlipListResponse();
            response.Data = new List<PaySlipData>();
            response.ValidationErrors = new List<string>();
            response.Success = true;
            try
            {
                var SlipList = _dbContext.PaySlip.AsNoTracking().Where(x=>x.Date <= request.DateTo && x.Date<=request.DateFrom).ToList();
                foreach(var Slip in SlipList)
                {
                    var row = new PaySlipData();
                    row.AgentId = Slip.AgentId;
                    row.AgentName = Slip.EmpName;
                    row.BasicPay = Slip.BasicPay;
                    row.CNIC = Slip.CNIC;
                    row.Date = Slip.Date;
                    row.DaysWorked = Slip.DaysWorked;
                    row.DepartmentId = Slip.DepartmentId;
                    if (Slip.Department != null)
                        row.DepartmentName = Slip.Department.Name;
                    row.Designation = Slip.Designation;
                    row.DOJ = Slip.DOJ;
                    row.EmpCode = Slip.EmpCode;
                    row.EmpName = Slip.EmpName;
                    row.EOBI = Slip.EOBI;
                    row.FuelAllowance = Slip.FuelAllowance;
                    row.Grade = Slip.Grade;
                    row.GrossPay = Slip.GrossPay;
                    row.HouseRent = Slip.HouseRent;
                    row.Id = Slip.Id;
                    row.Incentive = Slip.Incentive;
                    row.Location = Slip.Location;
                    row.NetPay = Slip.NetPay;
                    row.PadiBy = Slip.PadiBy;
                    row.Section = Slip.Section;
                    row.Shift = Slip.Shift;
                    row.Status = Slip.Status;
                    row.Tax = Slip.Tax;
                    row.Utility = Slip.Utility;
                    row.VehicleAllowance = Slip.VehicleAllowance;
                    row.WWID = Slip.WWID;
                    response.Data.Add(row);
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
}