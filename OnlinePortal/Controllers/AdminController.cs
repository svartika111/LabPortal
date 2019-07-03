using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlinePortal.Models;

namespace OnlinePortal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (AdminEntities dc = new AdminEntities())
                {
                    var v = dc.Admins.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {

                        Session["LogedUserFullname"] = v.Username.ToString();
                        return Redirect("/AfterAdminLogin/Index");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Invalid login credentials.");
                    }
                }
            }

            return View(u);
        }

    }
}