using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class Degree
    {

        public int DegreeID { get; set; }
        [DisplayName("דירוג:")]
        public string Name { get; set; }


    }
}