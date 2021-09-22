using AMS.Model.Requests.AgentAttendance;
using AMS.Models.Requests.AgentAttendance;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class AgentAttendanceApiController : ApiController
    {
        [HttpGet]
        public object GetListing([FromUri] GetListingRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpGet]
        public object GetEmployessList([FromUri] GetEmployessListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
        //[HttpGet]
        //public object GetAgentAttendance([FromUri] GetAgentAttendanceRequest req)
        //{
        //    req.UserId = User.Identity.GetUserId();
        //    var result = req.RunRequest(req);
        //    return result;
        //}

        [HttpPost]
        public object AddAgentAttendance([FromBody] AddAgentAttendanceRequest req)  
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object AddAgentExcuse([FromBody] AddAgentExcuseRequest req)  
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object SignOffAttendance([FromBody] SignOffAttendanceRequest req)  
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object GetSummary([FromBody] GetAttendanceSummaryRequest req)
        {
            req.CurrentUserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        //[HttpPost]
        //public object GetGraphData([FromBody] GetGraphDataRequest req) 
        //{
        //    var result = req.RunRequest(req);
        //    return result;
        //}
    }
}