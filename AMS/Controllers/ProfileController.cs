using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace AMS.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize(Roles = "Employee")]
        public ActionResult Index()
        {
            return View();
        }
    }
}