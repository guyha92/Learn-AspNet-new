using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class EmployeeInTraining
    {

        [Key]
        public int EmployeeInTrainingID { get; set; }
        public int EmployeeID { get; set; }
        public int TrainingID { get; set; }
        [DisplayName("עבר/לא עבר")]
        public bool IfPass { get; set; }      

        public virtual Employee Employee { get; set; }
        public virtual Training Training { get; set; }

    }
}