using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class EmployeeInExternalTraining
    {
        public enum TrainingStatuses : int
        {
            ממתין = 0,
            אושר=1,
            נדחה=2,
            עבר=3
        }

        [Key]
        public int EmployeeInExternalTrainingsID { get; set; }
        public int EmployeeID { get; set; }
        public int ExternalTrainingID { get; set; }

        [DisplayName("סטטוס הדרכה")]
        public TrainingStatuses TrainingStatus { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ExternalTraining Training { get; set; }
    }


}