using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class AgentAttendanceController : Controller
    {
        // GET: AgentAttendance
        [Authorize(Roles = "HR,Admin,SuperAdmin, Employee")]
        public ActionResult Index()
        {
            return View();
        }

        // GET Individual Agent Attendance
        [Authorize(Roles = "HR,SuperAdmin")]
        public ActionResult AgentAttendance()
        {
            return View();
        }

        // GET All Agents Attendance
        [Authorize(Roles = "HR,SuperAdmin")]
        public ActionResult StaffAttendance()
        {
            return View();
        }
    }
}