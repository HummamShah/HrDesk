using AMS.Model.Requests.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    [Authorize]
    public class ReportApiController : ApiController
    {
        public object GetAttendanceReport([FromUri] GetAttendanceReportRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}