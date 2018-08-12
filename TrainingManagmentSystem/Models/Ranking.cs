using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TrainingManagmentSystem.Models
{
    public class Ranking
    {
        public int RankingID { get; set; }

        [DisplayName("דירוג")]
        public string Name { get; set; }

        public int? SectorID { get; set; }
        Sector Sector { get; set; }
    }
}