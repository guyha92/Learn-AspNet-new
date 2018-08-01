using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class LoginModel
    {
        [Key]
        [Required]
        [DisplayName("שם משתמש")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("סיסמה")]
        [DataType("Password")]
        public string Password { get; set; }
    }
}
