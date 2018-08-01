using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingManagmentSystem.Models;

namespace TrainingManagmentSystem.DAL
{
    public static class ExampleTrainingsRepository
    {
        public static void Add(Training training)
        {
            using(OrganizationContext context = new OrganizationContext())
            {
                context.Trainings.Add(training);
                context.SaveChanges();
            }
        }

        
    }
}