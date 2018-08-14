using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public enum Gender
    {
        זכר, נקבה
    }

    public class Employee : Person
    {
        public Employee()
        {
            EmployeeInTrainings=new List<EmployeeInTraining>();
        }
      

        [Key]
        public int EmployeeID { get; set; }

        [DisplayName("תעודת זהות")]
        [Index(IsUnique =true)]
        public int EmployeeZehut { get; set; }
        [DisplayName("מין")]
        public Gender Gender { get; set; }
        [DisplayName("מחלקה")]
        public int DepartmentID { get; set; }
        [DisplayName("תת סקטור")]
        public int SubSectorID { get; set; }
        [DisplayName("דירוג")]
        public int RankingID { get; set; }
        [Required] 
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DisplayName("תאריך כניסה")]
        public DateTime StartDate { get; set; }
        [DisplayName("אחוז משרה")]
        public double PositionPercentage { get; set; }

        [DisplayName("תקציב להדרכה")]
        public int TrainingBudget { get; set; }

        public virtual ICollection<EmployeeInTraining> EmployeeInTrainings { get; set; }


        //public int TrainingBudget { get; set; }
        public virtual Department מחלקה { get; set; }
        public virtual SubSector Subsector { get; set; }
        public virtual Ranking דירוג { get; set; }
        public virtual Degree תואר { get; set; }



    }
}

    