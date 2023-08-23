using RMS.Auth;
using RMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Controllers
{
    [Logged]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult RequestList()
        {
            RMSEntities1 context = new RMSEntities1();
            User user = (User)Session["user"];
            var reqs = (from r in context.Requests
                        where r.AssignedTo == user.Id
                        select r).ToList();
            return View(reqs);
        }
        public ActionResult Edit(int id)
        {
            RMSEntities1 context = new RMSEntities1();
            var req = (from r in context.Requests
                       where r.Id == id
                       select r).FirstOrDefault();
            if (req.StatusId == 2)
            {
                ViewBag.StatusIds = (from u in context.Status
                                     where u.Id == 3 || u.Id == 4
                                     select u).ToList();
            }
            else if (req.StatusId == 3)
            {
                ViewBag.StatusIds = (from u in context.Status
                                     where u.Id == 4
                                     select u).ToList();
            }

            return View(req);

        }
        [HttpPost]
        public ActionResult Edit(Request request)
        {
            RMSEntities1 context = new RMSEntities1();
            var req = (from r in context.Requests
                       where r.Id == request.Id
                       select r).FirstOrDefault();
            req.StatusId = request.StatusId;
            context.SaveChanges();
            return RedirectToAction("RequestList");

        }
    }
}