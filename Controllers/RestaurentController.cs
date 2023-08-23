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
    public class RestaurentController : Controller
    {
        // GET: Restaurent
        public ActionResult Request()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Request(Request request)
        {
            RMSEntities1 context = new RMSEntities1();
            User user = (User)Session["user"];
            request.RequestedBy = user.Id;
            request.RequestedOn = DateTime.Now;
            request.AssignedTo = null;
            request.StatusId = 1;
            context.Requests.Add(request);
            context.SaveChanges();

            return RedirectToAction("RequestList");
        }
        public ActionResult RequestList()
        {
            RMSEntities1 context = new RMSEntities1();
            User user = (User)Session["user"];
            var reqs = (from r in context.Requests
                        where r.RequestedBy == user.Id
                        select r).ToList();
            return View(reqs);
        }
    }
}