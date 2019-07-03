using OnlinePortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlinePortal.Controllers
{
    public class StudentRecordController : Controller
    {
        [HttpGet]
        // GET: StudentRecord
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Profrecord()
        {
            OnlinePortal.Models.FinalProfessorEntities db = new OnlinePortal.Models.FinalProfessorEntities();
            List<SelectListItem> blogCategories = new List<SelectListItem>();
            blogCategories.Add(new SelectListItem { Text = "Select Professor", Value = "0", Selected = true });
            var categories = db.Professors.ToList();
            
            foreach (var c in categories)
            {
                blogCategories.Add(new SelectListItem { Text = c.Prof_Name, Value = c.Prof_ID.ToString() });
            } 
            ViewBag.CategoryList = blogCategories;
            return View();

        }
      
        public JsonResult GetBlogDetailByCategoryID( int Prof_id)
        {
            var profId = Convert.ToString(Prof_id);
            OnlinePortal.Models.FacultyDBEntities1 db = new OnlinePortal.Models.FacultyDBEntities1();
            List<OnlinePortal.Models.Candidate> blogs = new List<OnlinePortal.Models.Candidate>();
            blogs = db.Candidates.Where(x => x.Prof_Id == profId).ToList();

          var  blog = (from candidates in db.Candidates
                    where candidates.Prof_Id == profId
                    select new Test
                    {
                        FirstName = candidates.FirstName,
                        LastName = candidates.LastName,
                        EmailID = candidates.EmailID,
                        Resume = candidates.Resume,
                 
        }).ToList();
            return Json(new { result = blog }, JsonRequestBehavior.AllowGet );
        }

        [HttpGet]
        
        public ActionResult Download(string file)
        {
            //get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/App_Data/uploads"), file);
          
            return File(fullPath, "Application/msword", file);
        }
        [HttpGet]
        public ActionResult ViewAll()
        {
          return View(GetAllStudentsdetails());
        }

        IEnumerable<OnlinePortal.Models.Candidate> GetAllStudentsdetails()
        {
            using (OnlinePortal.Models.FacultyDBEntities1 db = new OnlinePortal.Models.FacultyDBEntities1())
            {
                return db.Candidates.ToList<OnlinePortal.Models.Candidate>();
            }
        }

        IEnumerable<OnlinePortal.Models.PortralUser> GetAllStudents()
        {
            using (OnlinePortal.Models.Registeruserentities db = new OnlinePortal.Models.Registeruserentities())
            {
                return db.PortralUsers.ToList<OnlinePortal.Models.PortralUser>();
            }
        }
        

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
       
       
            OnlinePortal.Models.Candidate emp = new OnlinePortal.Models.Candidate();
            if (id != 0)
            {
                using (OnlinePortal.Models.FacultyDBEntities1 db = new OnlinePortal.Models.FacultyDBEntities1())
                {
                    emp = db.Candidates.Where(x => x.ID == id).FirstOrDefault<OnlinePortal.Models.Candidate>();
                }
            }
            return View(emp);
        
       }

        [HttpPost]
        public ActionResult AddOrEdit(OnlinePortal.Models.Candidate emp)
        {
            try
            {
                
                using (OnlinePortal.Models.FacultyDBEntities1 db = new OnlinePortal.Models.FacultyDBEntities1())
                {
                    if (emp.ID == 0)
                    {
                        db.Candidates.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllStudentsdetails()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                using (OnlinePortal.Models.FacultyDBEntities1  db = new OnlinePortal.Models.FacultyDBEntities1())
                {
                    OnlinePortal.Models.Candidate emp = db.Candidates.Where(x => x.ID == id).FirstOrDefault<OnlinePortal.Models.Candidate>();
                    db.Candidates.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllStudentsdetails()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}