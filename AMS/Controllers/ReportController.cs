﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class ReportController : Controller
    {
        [Authorize]
        public ActionResult AgentAttendanceReport()
        {
            return View();
        }
    }
}