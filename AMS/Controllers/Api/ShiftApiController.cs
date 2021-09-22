using AMS.Models.Requests.Shifts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class ShiftApiController : ApiController
    {
        [HttpGet]
        public object GetShifts([FromUri] GetShiftsRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}