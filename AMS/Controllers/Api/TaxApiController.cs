using AMS.Models.Requests.Tax;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class TaxApiController : ApiController
    {
        [HttpGet]
        public object GetTaxList([FromUri] GetTaxListRequest request) {
            var result = request.RunRequest(request);
            return result;
        }

        [HttpGet]
        public object GetTax([FromUri] GetTaxRequest request)
        {
            var result = request.RunRequest(request);
            return result;
        }

        [HttpPost]
        public object AddTax([FromBody] AddTaxRequest request)
        {
            request.UserId = User.Identity.GetUserId();
            var result = request.RunRequest(request);
            return result;
        }

        [HttpPost]
        public object EditTax([FromBody] EditTaxRequest request)
        {
            request.UserId = User.Identity.GetUserId();
            var result = request.RunRequest(request);
            return result;
        }
    }
}