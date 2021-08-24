using AMS.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Requests.Hiring
{
    public class GetHiringResponse
    {
        public IEnumerable<string> ValidationErrors { get; set; }
        public string FirstInterview { get; set; }
        public string ApprovalImageUrl { get; set; }
        public string SecondInterview { get; internal set; }
        public string Remarks { get; set; }
        public string ApprovedBy { get; set; }
    }
    public class GetHiringRequest
    {
        private AMSEntities db = new AMSEntities();
       
        public int Id { get; set; }

        public object RunRequest(GetHiringRequest req)
        {
            var response = new GetHiringResponse();
            var Agent = db.Agent.Where(x => x.Id == req.Id).FirstOrDefault();
            response.FirstInterview = Agent.FirstInterviewBy;
            response.SecondInterview = Agent.SecondInterviewBy;
            response.Remarks = Agent.Remarks;
            response.ApprovedBy = Agent.ApprovedBy;
            response.ApprovalImageUrl = Agent.ApprovalImageUrl;
            return response;
        }
    }
}