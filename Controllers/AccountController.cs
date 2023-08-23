using RMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RMS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User user)
        {
            RMSEntities1 context = new RMSEntities1();
            context.Users.Add(user);
            context.SaveChanges();
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string Email, string Password)
        {
            RMSEntities1 context = new RMSEntities1();
            var user = (from u in context.Users
                        where u.Email == Email && u.Password == Password
                        select u).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("SignUp");
            }
            Session["user"] = user;
            if (user.TypeId == 1)
            {
                return RedirectToAction("RequestList", "Admin");
            }
            else if (user.TypeId == 2)
            {
                return RedirectToAction("RequestList", "Restaurent");
            }
            else if (user.TypeId == 3)
            {
                return RedirectToAction("RequestList", "Employee");
            }
            return View();


        }
        public ActionResult LogOut()
        {
            Session["user"] = null;
            return RedirectToAction("LogIn", "Account");
        }
    }
}