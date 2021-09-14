﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class LeaveController : Controller
    {
        [Authorize(Roles = "Employee")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Employee")]
        public ActionResult Apply()
        {
            return View();
        }

        [Authorize(Roles = "HR")]
        public ActionResult Requests()
        {
            return View();
        }
    }
}