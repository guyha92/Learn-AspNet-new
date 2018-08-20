using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrainingManagmentSystem.Models;


namespace TrainingManagmentSystem.DAL
{
    public class OrganizationContext : DbContext
    {

        public OrganizationContext() : base("OrganizationContext")
        {
            Database.SetInitializer(new OrganizationInitilaizer());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ExternalTraining> ExternalTrainings { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<Qualification> Qualification { get; set; }

        public DbSet<EmployeeQualification> EmployeeQualification { get; set; }

        public DbSet<Degree> Degrees { get; set; }
        public DbSet<EmployeeInTraining> EmployeeInTrainings { get; set; }
        public DbSet<EmployeeInExternalTraining> EmployeeInExternalTrainings { get; set; }

        public DbSet<TrainingSubSector> TrainingSubSectors { get; set; }

        public DbSet<LoginModel> LoginModels { get; set; }

        public DbSet<SubSector> SubSectors { get; set; }
    }


}