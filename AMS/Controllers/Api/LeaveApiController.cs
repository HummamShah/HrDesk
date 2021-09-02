using AMS.Model.Request.Leave;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class LeaveApiController : ApiController
    {
        [HttpPost]
        public object GetLeaveRecord([FromBody] GetLeaveRecordRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object ApplyLeave([FromBody] ApplyLeaveRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
    }
}