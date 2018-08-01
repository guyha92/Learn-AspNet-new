using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class Report
    {
        public int ReportID {get; set;}
        public string Name { get; set; }
        public string DepartmentID { get; set; }

        public virtual Department Department { get; set; }





    }
}