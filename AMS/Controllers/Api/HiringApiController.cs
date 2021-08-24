using AMS.Models.Requests.Hiring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class HiringApiController : ApiController
    {
        [HttpPost]
        public object AddHiring([FromBody] AddHiringRequest req) //If not working remove frombody
        {
           
            var result = req.RunRequest(req);
            return result;
        }
        [HttpGet]
        public object GetHiring([FromUri] GetHiringRequest  req)
        {
            
            var result = req.RunRequest(req);
            return result;
        }
        [HttpPost]
        public object EditHiring([FromBody] EditHiringRequest req)
        {
                
            var result = req.RunRequest(req);
            return result;
        }


    }
}