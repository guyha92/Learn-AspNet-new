//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrainingManagmentSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class TrainingSubSectors
    {
        public int TrainingSubSectorID { get; set; }
        public int TrainingID { get; set; }
        public int SubSectorID { get; set; }
    
        public virtual SubSectors1 SubSectors { get; set; }
        public virtual Trainings Trainings { get; set; }
    }
}
