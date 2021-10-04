using AMS.Models.Requests.Intensive;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class IntensiveApiController : ApiController
    {
        [HttpGet]
        public object GetIntensiveList([FromUri] GetIntensiveListRequest request)
        {
            var result = request.RunRequest(request);
            return result;
        }

        [HttpGet]
        public object GetIntensive([FromUri] GetIntensiveRequest request)
        {
            var result = request.RunRequest(request);
            return result;
        }

        [HttpPost]
        public object AddIntensive([FromBody] AddIntensiveRequest request)
        {
            request.UserId = User.Identity.GetUserId();
            var result = request.RunRequest(request);
            return result;
        }

        [HttpPost]
        public object EditIntensive([FromBody] EditIntensiveRequest request)
        {
            request.UserId = User.Identity.GetUserId();
            var result = request.RunRequest(request);
            return result;
        }
    }
}