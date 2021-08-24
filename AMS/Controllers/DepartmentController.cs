using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        // GET: Department
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }
    }
}