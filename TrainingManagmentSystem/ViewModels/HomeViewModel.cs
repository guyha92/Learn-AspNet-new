using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingManagmentSystem.Models;

namespace TrainingManagmentSystem.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<EmployeeInTraining> EmployeesNotPassed { get; set; }
        public IEnumerable<EmployeeInExternalTraining> EmployeeWaitingForApproval { get; set; }
        public IEnumerable<EmployeeInExternalTraining> EmployeeNotApproved { get; set; }
        public IEnumerable<Training> TrainingsNearExpiration { get; set; }
    }
}