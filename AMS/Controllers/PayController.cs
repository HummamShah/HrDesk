using AMS.Models.Requests.Pay;
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
        // POST: Edit PaySlip
        [Authorize(Roles = "HR")]
        public ActionResult Edit()
        {
            return View();
        }
        [Authorize(Roles = "HR")]
        public ActionResult PaySlipList()
        {
            return View();
        }
        [Authorize(Roles = "HR")]
        public ActionResult EditPayslip()
        {
            return View();
        }
        public ActionResult PrintPaySlip(int Id)
        {
            var req = new GetPaySlipByIdRequest { Id = Id };
            var resp = req.RunRequest(req);
            return View(resp);
        }
    }
}