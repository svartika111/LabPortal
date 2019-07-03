using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlinePortal.Models
{
    public class Student
    {
        public int Id
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
       
        public string EmailID
        {
            get;
            set;
        }
      
      
        public string Prof_Id
        {
            get;
            set;
        }
        public HttpPostedFileBase LabFile
        {
            get;
            set;
        }

        
        [NotMapped]
        public List<Prof_Details> Professor { get; set; }
    }
}