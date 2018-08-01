using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingManagmentSystem.Models
{
    public class User:Person
    {
        public enum UsersRoles: int
        {
            רגיל=0,
            מנהל=1
        }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        [DisplayName("מס' טלפון")]
        [DisplayFormat(DataFormatString = "{0:0#########}")]
        public string PhoneNum { get; set; }
        [DisplayName("כתובת מייל")]
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        public string Email { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        [DisplayName("שם משתמש")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("סיסמה")]
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong Validation")]
        [DisplayName("אימות סיסמה")]
        public string ValidationPassword { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        [DisplayName("אירגון")]
        public string Organization { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 char")]
        [DisplayName("מס' רישיון")]
        public string LicenseNum { get; set; }

        [DisplayName("סוג עובד")]
        public UsersRoles Role { get; set; }
    }
 }

