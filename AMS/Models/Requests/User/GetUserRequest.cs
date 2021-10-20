using AMS.Model.Model;
using AMS.Models.Enums;
using AMS.Models.Requests.Deduction;
using AMS.Models.Requests.FileUpload;
using AMS.Models.Requests.Incentive;
using AMS.Models.Requests.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.User
{
	public class GetUserResponse
	{
		public int Id { get; set; }
		public int AgentId { get; set; }
		public string UpdatedBy { get; set; }
		public int? DepartmentId { get; set; }
		public string DepartmentEnum { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Contact1 { get; set; }
		public string Contact2 { get; set; }
		public string Email { get; set; }
		public bool? HasSupervisor { get; set; }
		public int? SupervisorId { get; set; }
        public string ImageUrl { get; set; }
        public string Gender { get; set; }
		public int? RemainingLeaves { get; set; }
		public int? AnnualLeaves { get; set; }
		public int? MedicalLeaves { get; set; }
		public bool? IsLate { get; set; }
		public bool? IsPresent { get; set; }
		public bool? IsAbsent{ get; set; }
		public bool? IsExcused{ get; set; }
		public DateTime? TodayClockIn { get; set; }
		public int? AbsentCount { get; set; }
		public int? ExcusedCount { get; set; }
		public int? PresentCount { get; set; }
		public int? LateCount { get; set; }
		public int? ShiftId { get; set; }
		public decimal Salary { get; set; }
		public List<Document> Docs { get; set; }
		public List<Document> EducationalDocs { get; set; }
		public List<Document> Certificates { get; set; }
		public List<GetTaxResponse> Taxes { get; set; }
		public List<GetDeductionResponse> Deductions { get; set; }
		public List<GetIncentiveResponse> Incentives { get; set; }
	}

	public class GetUserRequest
	{
		private AMSEntities _dbContext = new AMSEntities();
		public int Id { get; set; }
        public string UserId { get; set; }
        public object RunRequest(GetUserRequest req)
		{
			var response = new GetUserResponse();

			//TODO set data  accorvar Data = .ToList();
			if (req.Id != null && req.Id != 0)
            {
				var Data = _dbContext.Agent.Where(x => x.Id == req.Id).FirstOrDefault();
				response.Id = Data.Id;
				response.FirstName = Data.FisrtName;
				response.Address = Data.Address;
				response.AgentId = Data.Id;
				response.Contact1 = Data.Contact1;
				response.Contact2 = Data.Contact2;
				response.DepartmentId = Data.DepartmentId;
				response.Email = Data.Email;
				response.HasSupervisor = Data.HasSupervisor;
				response.LastName = Data.LastName;
				response.SupervisorId = Data.SuperVisorId;
				response.ImageUrl = Data.ImageUrl;
				response.RemainingLeaves = Data.RemainingLeaves;
				response.AnnualLeaves = Data.AnnualLeaves;
				response.MedicalLeaves = Data.MedicalLeaves;
                response.Gender = Data.Gender;
				response.ShiftId = Data.ShiftId;
				response.Salary = Data.Salary;

				// get taxes applied on user
				response.Taxes = new List<GetTaxResponse>();
				var AgentTaxes = _dbContext.AgentTaxes.Where( x => x.AgentId == Data.Id).ToList();
				foreach (var tax in AgentTaxes) {
					var Tax = new GetTaxResponse();
					Tax.Id = tax.TaxId;
					Tax.Name = tax.TaxName;
					Tax.Type = tax.Tax.Type;
					Tax.Amount = tax.Tax.Amount;
					Tax.CreatedBy = tax.Tax.CreatedBy;
					Tax.CreatedAt = tax.Tax.CreatedAt;
					response.Taxes.Add(Tax);
				}

				// get deductions applied on user
				response.Deductions = new List<GetDeductionResponse>();
				var AgentDeductions = _dbContext.AgentDeductions.Where(x => x.AgentId == Data.Id).ToList();
				foreach (var deduction in AgentDeductions)
				{
					var Deduction = new GetDeductionResponse();
					Deduction.Id = deduction.DeductionId;
					Deduction.Name = deduction.DeductionName;
					Deduction.Type = deduction.Deductions.Type;
					Deduction.Amount = deduction.Deductions.Amount;
					Deduction.CreatedBy = deduction.Deductions.CreatedBy;
					Deduction.CreatedAt = deduction.Deductions.CreatedAt;
					response.Deductions.Add(Deduction);
				}

				// get incentives applied on user
				response.Incentives = new List<GetIncentiveResponse>();
				var AgentIncentives = _dbContext.AgentIncentives.Where(x => x.AgentId == Data.Id).ToList();
				foreach (var incentive in AgentIncentives)
				{
					var Incentive = new GetIncentiveResponse();
					Incentive.Id = incentive.IncentiveId;
					Incentive.Name = incentive.IncentiveName;
					Incentive.Type = incentive.Incentives.Type;
					Incentive.Amount = incentive.Incentives.Amount;
					Incentive.CreatedBy = incentive.Incentives.CreatedBy;
					Incentive.CreatedAt = incentive.Incentives.CreatedAt;
					
					response.Incentives.Add(Incentive);
				}

				// get user docs
				response.Docs = new List<Document>();
				var Docs = Data.Documents.Where(x => x.AgentId == Data.Id && x.Title == "Resume" || x.Title == "CNIC front" || x.Title == "CNIC back" || x.Title == "Appointment Letter").ToList();
				
				AddEmptyDocRowInDb(Docs, "Resume", Data.Id);
				AddEmptyDocRowInDb(Docs, "CNIC front", Data.Id);
				AddEmptyDocRowInDb(Docs, "CNIC back", Data.Id);
				AddEmptyDocRowInDb(Docs, "Appointment Letter", Data.Id);

				Docs = Data.Documents.Where(x => x.AgentId == Data.Id && x.Title == "Resume" || x.Title == "CNIC front" || x.Title == "CNIC back" || x.Title == "Appointment Letter").OrderBy(x => x.Title).ToList();
				foreach (var doc in Docs) {
					var row = new Document();
					row.Id = doc.Id;
					row.Title = doc.Title;
					row.SubTitle = doc.SubTitle;
					row.Url = doc.DocumentUrl;
					row.UploadedOn = doc.UploadedOn;
					row.UploadedBy = doc.UploadedBy;
					response.Docs.Add(row);
				}
				response.EducationalDocs = new List<Document>();
				var EducationalDocs = Data.Documents.Where(x => x.AgentId == Data.Id && x.Title == "Educational").ToList();
				foreach (var doc in EducationalDocs)
				{
					var row = new Document();
					row.Id = doc.Id;
					row.Title = doc.Title;
					row.SubTitle = doc.SubTitle;
					row.Url = doc.DocumentUrl;
					row.UploadedOn = doc.UploadedOn;
					row.UploadedBy = doc.UploadedBy;
					response.EducationalDocs.Add(row);
				}
				response.Certificates = new List<Document>();
				var Certifications = Data.Documents.Where(x => x.AgentId == Data.Id && x.Title == "Certifications").ToList();
				foreach (var doc in Certifications)
				{
					var row = new Document();
					row.Id = doc.Id;
					row.Title = doc.Title;
					row.SubTitle = doc.SubTitle;
					row.Url = doc.DocumentUrl;
					row.UploadedOn = doc.UploadedOn;
					row.UploadedBy = doc.UploadedBy;
					response.Certificates.Add(row);
				}

				//response.Taxes = new List<AgentTaxes.AgentTaxes>();
				//var AgentTaxes = Data.AgentTaxes.Where(x => x.AgentId == Data.Id).ToList();
				//foreach (var taxes in AgentTaxes) {
				//	var Taxes = new AgentTaxes.AgentTaxes();
				//	Taxes.Id = taxes.Id;
				//	Taxes.TaxId = taxes.TaxId;
				//	Taxes.TaxName = taxes.TaxName;
				//	Taxes.AgentId = taxes.AgentId;
				//	Taxes.AgentName = taxes.AgentName;
				//	response.Taxes.Add(Taxes);
				//}

				//response.Incentives = new List<AgentIncentives.AgentIncentives>();
				//var AgentIncentives = Data.AgentIncentives.Where(x => x.AgentId == Data.Id).ToList();
				//foreach (var incentive in AgentIncentives)
				//{
				//	var Incentive = new AgentIncentives.AgentIncentives();
				//	Incentive.Id = incentive.Id;
				//	Incentive.IncentiveId = incentive.IncentiveId;
				//	Incentive.IncentiveName = incentive.IncentiveName;
				//	Incentive.AgentId = incentive.AgentId;
				//	Incentive.AgentName = incentive.AgentName;
				//	response.Incentives.Add(Incentive);
				//}

				var MonthAttendance = Data.AgentAttendance.Where(x => x.CreatedAt.Value.Date.Month == DateTime.Today.Month && x.IsAttendanceMarked == true).ToList();
				response.AbsentCount = MonthAttendance.Where(x => x.IsAbsent == true).Count();
				response.PresentCount = MonthAttendance.Where(x => x.IsPresent == true).Count();
				response.LateCount = MonthAttendance.Where(x => x.IsLate == true).Count();
				response.ExcusedCount = MonthAttendance.Where(x => x.IsExcused == true).Count();
;				var TodaysAttendance = Data.AgentAttendance.Where(x => x.CreatedAt.Value.Date == DateTime.Today.Date).FirstOrDefault();
				if (TodaysAttendance != null)
				{
					if (TodaysAttendance.IsAbsent == false)
						response.TodayClockIn = TodaysAttendance.StartDateTime;
					if (response.DepartmentId.HasValue)
					{
						response.DepartmentEnum = ((Departments)Data.DepartmentId.Value).ToString();
					}
					response.IsLate = TodaysAttendance.IsLate;
					response.IsPresent = TodaysAttendance.IsPresent;
					response.IsAbsent = TodaysAttendance.IsAbsent;
					response.IsExcused = TodaysAttendance.IsExcused;
				}
			}
            else
            {
				var Data = _dbContext.Agent.Where(x => x.UserId == req.UserId).FirstOrDefault();
				response.Id = Data.Id;
				response.FirstName = Data.FisrtName;
				response.Address = Data.Address;
				response.AgentId = Data.Id;
				response.Contact1 = Data.Contact1;
				response.Contact2 = Data.Contact2;
				response.DepartmentId = Data.DepartmentId;
				response.Email = Data.Email;
				response.HasSupervisor = Data.HasSupervisor;
				response.LastName = Data.LastName;
				response.SupervisorId = Data.SuperVisorId;
				response.ImageUrl = Data.ImageUrl;
				response.RemainingLeaves = Data.RemainingLeaves;
				response.AnnualLeaves = Data.AnnualLeaves;
				response.MedicalLeaves = Data.MedicalLeaves;
				response.Gender = Data.Gender;
				response.ShiftId = Data.ShiftId;

				// uncomment all the below liens of codes if we need the documents when an employee himslef needs it. For now, the above line of codes work only for HR when he/she provieds the AgentId through query string


				//response.Docs = new List<Document>();
				//var Docs = Data.Documents.Where(x => x.AgentId == Data.Id && x.Title == "Resume" || x.Title == "'CNIC front" || x.Title == "CNIC back" || x.Title == "Appointment Letter").ToList();
				//foreach (var doc in Docs)
				//{
				//	var row = new Document();
				//	row.Id = doc.Id;
				//	row.Title = doc.Title;
				//	row.SubTitle = doc.SubTitle;
				//	row.Url = doc.DocumentUrl;
				//	row.UploadedOn = doc.UploadedOn;
				//	row.UploadedBy = doc.UploadedBy;
				//	response.Docs.Add(row);
				//}
				//response.EducationalDocs = new List<Document>();
				//var EducationalDocs = Data.Documents.Where(x => x.AgentId == Data.Id && x.Title == "Educational").ToList();
				//foreach (var doc in EducationalDocs)
				//{
				//	var row = new Document();
				//	row.Id = doc.Id;
				//	row.Title = doc.Title;
				//	row.SubTitle = doc.SubTitle;
				//	row.Url = doc.DocumentUrl;
				//	row.UploadedOn = doc.UploadedOn;
				//	row.UploadedBy = doc.UploadedBy;
				//	response.EducationalDocs.Add(row);
				//}
				//response.Certificates = new List<Document>();
				//var Certifications = Data.Documents.Where(x => x.AgentId == Data.Id && x.Title == "Certifications").ToList();
				//foreach (var doc in Certifications)
				//{
				//	var row = new Document();
				//	row.Id = doc.Id;
				//	row.Title = doc.Title;
				//	row.SubTitle = doc.SubTitle;
				//	row.Url = doc.DocumentUrl;
				//	row.UploadedOn = doc.UploadedOn;
				//	row.UploadedBy = doc.UploadedBy;
				//	response.Certificates.Add(row);
				//}

				var MonthAttendance = Data.AgentAttendance.Where(x => x.CreatedAt.Value.Date.Month == DateTime.Today.Month && x.IsAttendanceMarked == true).ToList();
				response.AbsentCount = MonthAttendance.Where(x => x.IsAbsent == true).Count();
				response.PresentCount = MonthAttendance.Where(x => x.IsPresent == true).Count();
				response.LateCount = MonthAttendance.Where(x => x.IsLate == true).Count();
				response.ExcusedCount = MonthAttendance.Where(x => x.IsExcused == true).Count();
				var TodaysAttendance = Data.AgentAttendance.Where(x => x.CreatedAt.Value.Date == DateTime.Today.Date).FirstOrDefault();
				if (TodaysAttendance != null)
				{
					if (TodaysAttendance.IsAbsent == false)
						response.TodayClockIn = TodaysAttendance.StartDateTime;
					if (response.DepartmentId.HasValue)
					{
						response.DepartmentEnum = ((Departments)Data.DepartmentId.Value).ToString();
					}
					response.IsLate = TodaysAttendance.IsLate;
					response.IsPresent = TodaysAttendance.IsPresent;
					response.IsAbsent = TodaysAttendance.IsAbsent;
					response.IsExcused = TodaysAttendance.IsExcused;
				}
			}
			return response;
		}

		public bool AddEmptyDocRowInDb(List<Documents> Docs, string Title, int AgentId) {
			var flag = false;
			foreach (var doc in Docs)
			{
				if (doc.Title == Title)
				{
					flag = true;
					break;
				}
			}
			if (!flag) {
				var doc = new Documents();
				doc.AgentId = AgentId;
				doc.Title = Title;
				_dbContext.Documents.Add(doc);
				_dbContext.SaveChanges();
				flag = true;
			}
			return flag;
		}
	}
}