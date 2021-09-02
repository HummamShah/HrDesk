using AMS.Model.Request.Pay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class PayApiContoller : ApiController
    {
        [HttpPost]
        public object GetPayRecord([FromBody] GetPayRecordRequest req) {
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object AddPayRecord([FromBody] AddPayRecordRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }
    }
}