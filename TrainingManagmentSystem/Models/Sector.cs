using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{

    public class Sector
    {
        public Sector()
        {
            SubSector = new List<SubSector>();
            Ranks = new List<Ranking>();

        }

        public int SectorID { get; set; }
        [DisplayName("סקטור")]
        public string SectorType { get; set; }
        //public int Value { get; set; }
        //public virtual ICollection<Training> Training { get; set; }
        //public ICollection<SubSector> SubSectors = new List<SubSector>();

        public virtual ICollection<SubSector> SubSector { get; set; }
        public virtual ICollection<Ranking> Ranks { get; set; }
    }
}