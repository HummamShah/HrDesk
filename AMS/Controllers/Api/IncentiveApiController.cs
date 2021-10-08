using AMS.Models.Requests.Incentive;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class IncentiveApiController : ApiController
    {
        [HttpGet]
        public object GetIncentiveList([FromUri] GetIncentiveListRequest request)
        {
            var result = request.RunRequest(request);
            return result;
        }

        [HttpGet]
        public object GetIncentive([FromUri] GetIncentiveRequest request)
        {
            var result = request.RunRequest(request);
            return result;
        }

        [HttpPost]
        public object AddIncentive([FromBody] AddIncentiveRequest request)
        {
            request.UserId = User.Identity.GetUserId();
            var result = request.RunRequest(request);
            return result;
        }

        [HttpPost]
        public object EditIncentive([FromBody] EditIncentiveRequest request)
        {
            request.UserId = User.Identity.GetUserId();
            var result = request.RunRequest(request);
            return result;
        }
    }
}