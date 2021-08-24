using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.Hiring
{
    public class AddHiringResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
    }
    public class User
    {
        public string FirstInterview { get; set; }
        public string ApprovalImageUrl { get; set; }
        public string SecondInterview { get; internal set; }
        public string Remarks { get; set; }
        public string ApprovedBy { get; set; }
    }
    public class AddHiringRequest
    {
        private AMSEntities db = new AMSEntities();
       
        public int Id { get; set; }
        public User User { get; set; }

        public object RunRequest(AddHiringRequest req)
        {
            var response = new AddHiringResponse();
            var Agent = db.Agent.Where(x => x.Id == req.Id).FirstOrDefault();
            Agent.FirstInterviewBy = req.User.FirstInterview;
            Agent.SecondInterviewBy = req.User.SecondInterview;
            Agent.Remarks = req.User.Remarks;
            Agent.ApprovalImageUrl = req.User.ApprovalImageUrl;
            db.SaveChanges();
            return response;
        }
    }
}