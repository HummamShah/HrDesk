using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class UploadsController : Controller
    {
        // GET: Uploads
        public ActionResult Hiring()
        {
            return View();
        }
        public ActionResult Documents()
        {
            return View();
        }
        public ActionResult Performance()
        {
            return View();
        }
        public ActionResult ExitEmployee()
        {
            return View();
        }
    }
}