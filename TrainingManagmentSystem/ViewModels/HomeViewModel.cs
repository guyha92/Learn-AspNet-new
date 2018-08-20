using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingManagmentSystem.Models;

namespace TrainingManagmentSystem.ViewModels
{
    public class HomeViewModel
    {
        public List<EmployeeQualification> QualificationsNearExpiration { get; set; }
    }
}