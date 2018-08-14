using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingManagmentSystem.Models;

namespace TrainingManagmentSystem.ViewModels
{
    public class EmployeeTrainingsVM
    {
        public string EmployeeName { get; set; }

        public List<EmployeeInTraining> InternalTrainings { get; set; }

        public List<EmployeeInExternalTraining> ExternalTrainings { get; set; }
    }
}