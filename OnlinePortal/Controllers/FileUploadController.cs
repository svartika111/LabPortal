using OnlinePortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlinePortal.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpGet]
        public ActionResult Upload(int id = 0)
        {

           Student profmodel = new Student();
            using (FacultyDBEntities1 db = new FacultyDBEntities1())
                profmodel.Professor = db.Prof_Details.ToList<Prof_Details>();
            TempData["Student_details"] = profmodel;
            return View(profmodel);

        }

        [HttpPost]
        public ActionResult Upload(OnlinePortal.Models.Student employee)
        {
            using (OnlinePortal.Models.FacultyDBEntities1 entity = new OnlinePortal.Models.FacultyDBEntities1())
            {
                var candidate = new Candidate()
                {

                    EmailID = employee.EmailID,

                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Prof_Id = employee.Prof_Id,
                    Resume = SaveToPhysicalLocation(employee.LabFile),
                    CreatedOn = DateTime.Now
                };
                TempData.Add("MyTempData", employee.EmailID);
                entity.Candidates.Add(candidate);
                entity.SaveChanges();
                if (candidate.Prof_Id != null)
                {

                    return RedirectToAction("Students");
                }
            }

            return View(employee);
        }
        public ActionResult Students()
        {
            string Email = TempData["MyTempData"].ToString();
            OnlinePortal.Models.FacultyDBEntities1 entities = new OnlinePortal.Models.FacultyDBEntities1();
            var detail = from students in entities.Candidates
                         where students.EmailID==Email
                         select students;
            return View( detail);
        }
        /// <summary>  
        /// Save Posted File in Physical path and return saved path to store in a database  
        /// </summary>  
        /// <param name="file"></param>  
        /// <returns></returns> 

        [HttpPost]
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
                ViewBag.Message = "File uploaded successfully.";
                return path;
            }
            return string.Empty;
        }
    }
}