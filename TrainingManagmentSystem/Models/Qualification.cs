using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class Qualification
    {
        public int QualificationID { get; set; }

        [DisplayName("הכשרה")]
        public string Name { get; set; }      

    }
}