using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class PayController : Controller
    {
        // GET: Pay Roll
        [Authorize(Roles = "HR")]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Generate PaySlip
        [Authorize(Roles = "HR")]
        public ActionResult PaySlip()
        {
            return View();
        }
    }
}