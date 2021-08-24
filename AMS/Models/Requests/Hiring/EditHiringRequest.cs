using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.Hiring
{
    public class EditHiringResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class EditHiringRequest
    {
        private AMSEntities db = new AMSEntities();
        public string FirstInterview { get; set; }
        public string ApprovalImageUrl { get; set; }
        public string SecondInterview { get; internal set; }
        public string Remarks { get; set; }
        public string ApprovedBy { get; set; }
        public int Id { get; set; }

        public object RunRequest(EditHiringRequest req)
        {
            var response = new EditHiringResponse();
            var Agent = db.Agent.Where(x => x.Id == req.Id).FirstOrDefault();
            Agent.FirstInterviewBy = req.FirstInterview;
            Agent.SecondInterviewBy = req.SecondInterview;
            Agent.Remarks = req.Remarks;
            Agent.ApprovalImageUrl = req.ApprovalImageUrl;
            db.SaveChanges();
            return response;
        }
    }
}