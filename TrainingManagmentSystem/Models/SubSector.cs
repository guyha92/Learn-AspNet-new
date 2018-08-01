using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class SubSector
    {
        public SubSector()
        {
            TrainingSubSectors = new List<TrainingSubSector>();
            Employees = new List<Employee>();
        }
        public int SubSectorID { get; set; }
        [DisplayName("תת סקטור")]
        public string SubSectortype { get; set; }
        //public int Value { get; set; }
        public int SectorID { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual ICollection<TrainingSubSector> TrainingSubSectors { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}