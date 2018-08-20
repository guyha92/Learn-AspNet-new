using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class EmployeeQualification
    {
        [Key]
        public int EmployeeQualificationID { get; set; }

        [DisplayName("עובד")]
        public int EmployeeID { get; set; }

        [DisplayName("הכשרה")]
        public int QualificationID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DisplayName("תאריך תפוגה")]
        public DateTime ExpirationDate { get; set; }

        public Qualification Qualification { get; set; }

        public Employee Employee { get; set; }
    }
}