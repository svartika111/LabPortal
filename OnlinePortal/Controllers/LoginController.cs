using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlinePortal.Models;

namespace OnlinePortal.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult LoginStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginStudent(PortralUser u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (Registeruserentities dc = new Registeruserentities())
                {
                    var v = dc.PortralUsers.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.UserId.ToString();
                        Session["LogedUserFullname"] = v.Username.ToString();
                        return Redirect("/FileUpload/Upload");
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