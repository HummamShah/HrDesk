using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class PayRecordController : Controller
    {
        // GET: AgentAttendance
        [Authorize(Roles = "Employee")]
        public ActionResult Index()
        {
            return View();
        }
    }
}