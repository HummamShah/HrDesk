using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class PensionController : Controller
    {
        // GET: Pension
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();

        }
        public ActionResult Edit()
        {
            return View();
        }
    }
}