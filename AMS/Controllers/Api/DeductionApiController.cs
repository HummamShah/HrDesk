using AMS.Models.Requests.Deduction;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class DeductionApiController : ApiController
    {
        [HttpGet]
        public object GetDeductionList([FromUri] GetDeductionListRequest request)
        {
            var result = request.RunRequest(request);
            return result;
        }

        [HttpGet]
        public object GetDeduction([FromUri] GetDeductionRequest request)
        {
            var result = request.RunRequest(request);
            return result;
        }

        [HttpPost]
        public object AddDeduction([FromBody] AddDeductionRequest request)
        {
            request.UserId = User.Identity.GetUserId();
            var result = request.RunRequest(request);
            return result;
        }

        [HttpPost]
        public object EditDeduction([FromBody] EditDeductionRequest request)
        {
            request.UserId = User.Identity.GetUserId();
            var result = request.RunRequest(request);
            return result;
        }
    }
}