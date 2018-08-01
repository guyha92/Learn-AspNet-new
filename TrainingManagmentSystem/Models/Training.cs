using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class Training
    {
        public Training()
        {
            TrainingSubSectors = new List<TrainingSubSector>();
            EmployeeInTrainings = new List<EmployeeInTraining>();
        }

        public int TrainingID { get; set; }
        [Required]
        [DisplayName("שם הדרכה")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DisplayName("תאריך התחלה")]
        public DateTime TrainingDate { get; set; }
        [Required]
        [DisplayName("מספר מפגשים")]
        public int NumberOfMeetings { get; set; }

        [DisplayName("משך בשעות")]
        public int Duration { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DisplayName("תאריך סיום")]
        public DateTime TrainingEnd { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [DisplayName("תאריך תפוגה")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [DisplayName("תוקף בשנים")]
        public int ExpireDate { get; set; }
        [DisplayName("מיקום הדרכה")]
        public string Location { get; set; }
        [DisplayName("תת סקטורים")]
        public virtual ICollection<TrainingSubSector> TrainingSubSectors { get; set; }
        public void AddSubSector(SubSector subsector)
        {
            TrainingSubSectors.Add(new TrainingSubSector()
                {
                SubSector= subsector
            });
        }

        public void RemoveSubSector(int subsectorId)
        {
            TrainingSubSectors.Remove(new TrainingSubSector() { SubSectorID=subsectorId});
        }

        public virtual ICollection<EmployeeInTraining> EmployeeInTrainings { get; set; }







}

}