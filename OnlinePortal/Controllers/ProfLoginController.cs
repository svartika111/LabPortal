using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlinePortal.Models;

namespace OnlinePortal.Controllers
{
    public class ProfLoginController : Controller
    {
        // GET: ProfLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ProfLogin u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (ProfLoginEntities dc = new ProfLoginEntities())
                {
                    var v = dc.ProfLogins.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        
                        Session["LogedUserFullname"] = v.Username.ToString();
                        return Redirect("/StudentRecord/profrecord");
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