using AMS.Models.Requests.Holiday;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace AMS.Controllers.Api
{
    public class HolidayApiController : ApiController
    {
        [HttpPost]
        public object AddHoliday([FromBody] AddHolidayRequest req) {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }

        [HttpGet]
        public object GetHolidayList([FromUri] GetHolidayListRequest req)
        {
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object DeleteHoliday([FromBody] DeleteHolidayRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }

        [HttpPost]
        public object EditHoliday([FromBody] EditHolidayRequest req)
        {
            req.UserId = User.Identity.GetUserId();
            var result = req.RunRequest(req);
            return result;
        }
    } 
}