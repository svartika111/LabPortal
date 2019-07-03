using OnlinePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Mail;


namespace OnlinePortal.Controllers
{
    public class HomeController : Controller
    {
        //for GET
        public ActionResult Index()
        {
            return View();
        }

        //This is for HTTP Post request
      
        [HttpPost]
        public ActionResult Index(PortralUser user)
        {
            Registeruserentities usersEntities = new Registeruserentities();
          
            user.CreatedDate = DateTime.Now;

            usersEntities.PortralUsers.Add(user);
            usersEntities.SaveChanges();
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                switch (user.UserId)
                {
                    case -1:
                        message = "Username already exists.\\nPlease choose a different username.";
                        break;
                    case -2:
                        message = "Supplied email address has already been used.";
                        break;
                    default:
                        message = "Registration successful Click Next ";
                        break;
                }
                ViewBag.Message = message;
            }
            return View(user);
        }
       
    }
}