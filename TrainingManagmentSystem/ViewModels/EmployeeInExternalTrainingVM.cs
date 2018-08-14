using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingManagmentSystem.Models;

namespace TrainingManagmentSystem.ViewModels
{
    public class EmployeeInExternalTrainingVM
    {
        public EmployeeInExternalTraining EmployeeInTraining { get; set; }

        public string Alert { get; set; }

        public EmployeeInExternalTrainingVM(EmployeeInExternalTraining empInTraining)
        {
            EmployeeInTraining = empInTraining;

            bool BudgetAlert = false;
            bool TrainingNumberAlert = false;

            if (empInTraining.TrainingStatus == EmployeeInExternalTraining.TrainingStatuses.ממתין)
            {
                if (empInTraining.Employee.RemainingBudget < empInTraining.Training.Cost)
                {
                    BudgetAlert = true;
                }
                if (empInTraining.Employee.RemainingTrainings <= 0)
                {
                    TrainingNumberAlert = true;
                }
            }
           

            if (BudgetAlert && TrainingNumberAlert)
            {
                Alert = "ההדרכה תגרום לחריגה בתקציב ההדרכות ובחריגה בכמות ההדרכות השנתית לעובד";
            } else if ( BudgetAlert)
            {
                Alert = "ההדרכה תגרום לחריגה בתקציב ההדרכות השנתי לעובד";

            } else if (TrainingNumberAlert)
            {
                Alert = "ההדרכה תגרום לחריגה בכמות ההדרכות השנתית לעובד";
            }

        }

    }
}