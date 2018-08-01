using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        [DisplayName("מחלקה:")]
        public string Name { get; set; }
        public virtual ICollection<Report> Reports { get; set; }



    }
}