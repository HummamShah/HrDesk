using AMS.Models.Requests.Pay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class PayApiController : ApiController
    {
        [HttpGet]
        public object GetPayRollList([FromUri] GetPayRollListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object GeneratePaySlip([FromBody] GeneratePaySlipRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object SavePaySlip([FromBody] SavePaySlipRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}