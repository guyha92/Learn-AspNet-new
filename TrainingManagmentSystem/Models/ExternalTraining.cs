using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class ExternalTraining
    {
        public int ExternalTrainingID { get; set; }
        //public int EmployeeZehut { get; set; } 
        [DisplayName("שם הדרכה")]
        public string Name { get; set; }
        [DisplayName("סוג השתלמות")]
        public string type { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DisplayName("תאריך התחלה")]
        public DateTime TrainingDate { get; set; }
        [DisplayName("משך בימים")]
        public double Duration { get; set; }    
        [DisplayName("מספר מפגשים")]
        public int NumberOfMeetings { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DisplayName("תאריך סיום ")]
        public DateTime TrainingEnd { get; set; }
        [DisplayName("מיקום הדרכה")]
        public string Location { get; set; }
        [DisplayName("עלות")]
        [DataType(DataType.Currency)]
        public float? Cost { get; set; }
        //public virtual Employee Employee { get; set; }
    }
}