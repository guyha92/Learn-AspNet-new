using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public abstract class Person
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        [DisplayName("שם משפחה")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        [DisplayName("שם פרטי")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DisplayName("תאריך לידה")]
        public DateTime BirthDate { get; set; }
      

    }
}

