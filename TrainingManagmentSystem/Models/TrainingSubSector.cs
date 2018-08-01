using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class TrainingSubSector

    {
        public int TrainingSubSectorID { get; set; }
        public int TrainingID { get; set; }
        public int SubSectorID { get; set; }

        public virtual Training Training { get; set; }
        public virtual SubSector SubSector { get; set; }


    }
}