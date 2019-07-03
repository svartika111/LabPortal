using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlinePortal.Controllers
{
    public class RetreiveDataController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<SelectListItem> ProfList = GetProfessors();
            return View(ProfList);
        }

        [HttpPost]
        public ActionResult Index(string ddlCustomers)
        {
            List<SelectListItem> ProfList = GetProfessors();
            
            if (!string.IsNullOrEmpty(ddlCustomers))
            {
               return RedirectToAction("Students");

            }

            return View(ProfList);
        }

        private static List<SelectListItem> GetProfessors()
        {
            OnlinePortal.Models.ProfessorNameEntities entities = new OnlinePortal.Models.ProfessorNameEntities();
            List<SelectListItem> ProfList = (from p in entities.Professor_Credential.AsEnumerable()
                                             select new SelectListItem
                                             {
                                                 Text = p.Prof_Name,
                                                 Value = p.Prof_ID.ToString()
                                             }).ToList();


            //Add Default Item at First Position.
            ProfList.Insert(0, new SelectListItem { Text = "--Professor Name--", Value = "" });
            return ProfList;
        }

        // GET: RetreiveData
        public ActionResult Students()
        {
            OnlinePortal.Models.FacultyDBEntities1 entities = new OnlinePortal.Models.FacultyDBEntities1 ();
            return View(from students in entities.Candidates
                        select students);
        }
    }
}