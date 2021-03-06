using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "HR")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "HR")]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "HR")]
        public ActionResult Edit()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult AddAdmin()
        {
            return View();
        }
    }
}