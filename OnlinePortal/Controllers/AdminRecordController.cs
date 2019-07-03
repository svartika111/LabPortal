using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlinePortal.Controllers
{
    public class AdminRecordController : Controller
    {
        [HttpGet]
        // GET: StudentRecord
        public ActionResult Index()
        {

            return View();
        }
        // GET: AdminRecord
        [HttpGet]
        public ActionResult ViewAll()
        {
            return View(GetAllProfessordetails());
        }

        IEnumerable<OnlinePortal.Models.Professor> GetAllProfessordetails()
        {
            using (OnlinePortal.Models.FinalProfessorEntities db = new OnlinePortal.Models.FinalProfessorEntities())
            {
                return db.Professors.ToList<OnlinePortal.Models.Professor>();
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


            OnlinePortal.Models.Professor emp = new OnlinePortal.Models.Professor();
            if (id != 0)
            {
                using (OnlinePortal.Models.FinalProfessorEntities db = new OnlinePortal.Models.FinalProfessorEntities())
                {
                   
                    emp = db.Professors.Where(x => x.id == id).FirstOrDefault<OnlinePortal.Models.Professor>();
                }
            }
            return View(emp);

        }

        [HttpPost]
        public ActionResult AddOrEdit(OnlinePortal.Models.Professor emp)
        {
            try
            {

                using (OnlinePortal.Models.FinalProfessorEntities db = new OnlinePortal.Models.FinalProfessorEntities())
                {
                    
                    if (emp.id== 0)
                    {
                        db.Professors.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllProfessordetails()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
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
                using (OnlinePortal.Models.FinalProfessorEntities db = new OnlinePortal.Models.FinalProfessorEntities())
                {
                    OnlinePortal.Models.Professor emp = db.Professors.Where(x => x.id == id).FirstOrDefault<OnlinePortal.Models.Professor>();
                    db.Professors.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllProfessordetails()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}