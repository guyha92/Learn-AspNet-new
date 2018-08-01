using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingManagmentSystem.Models;

namespace TrainingManagmentSystem.DAL
{

    

    public class OrganizationInitilaizer : System.Data.Entity.DropCreateDatabaseIfModelChanges<OrganizationContext>
    {
       
        

        protected override void Seed(OrganizationContext context)
        {



            var departments = new List<Department>
            {
            new Department{DepartmentID=1,Name="ילדים"},
            new Department{DepartmentID=2,Name="מוח"}

            };
            departments.ForEach(s => context.Departments.Add(s));
            context.SaveChanges();

            var degrees = new List<Degree>
            {
            new Degree{DegreeID=1,Name="דוק'"},
            };
            degrees.ForEach(s => context.Degrees.Add(s));
            context.SaveChanges();


            var ranks = new List<Ranking>
            {
            new Ranking{RankingID=1,Name="מנהלה" },
            new Ranking{RankingID=2,Name="בכיר"}
            };
            ranks.ForEach(s => context.Rankings.Add(s));
            context.SaveChanges();

            var sectors = new List<Sector>
            {
            new Sector{SectorID=1,SectorType="רופאים"},
            new Sector{SectorID=2,SectorType="סיעוד"},
            new Sector{SectorID=3,SectorType="פרא-רפואי"},
            new Sector{SectorID=4,SectorType="מנהל-ומשק"}
            };
            sectors.ForEach(s => context.Sectors.Add(s));
            context.SaveChanges();

            var subSectors = new List<SubSector>
            {
            new SubSector{SubSectorID=1,SubSectortype="ברך",SectorID=1},
            new SubSector{SubSectorID=2,SubSectortype="לב",SectorID=1},
            new SubSector{SubSectorID=3,SubSectortype="עיניים",SectorID=1},
            new SubSector{SubSectorID=5,SubSectortype="גב",SectorID=1},
            new SubSector{SubSectorID=6,SubSectortype="אחות מילדת",SectorID=2},
            new SubSector{SubSectorID=7,SubSectortype="אחות טיפול נמרץ",SectorID=2},
            new SubSector{SubSectorID=8,SubSectortype="אחות סיעודית",SectorID=2},
            new SubSector{SubSectorID=9,SubSectortype="אחות מטפלת",SectorID=2}
            };

            subSectors.ForEach(s => context.SubSectors.Add(s));
            context.SaveChanges();



            var users = new List<User>
            {
                new User{UserId=1,BirthDate=DateTime.Parse("01-09-1992"),ValidationPassword="123", Email="guyha92@gmail.com",FirstName="גיא",LastName="חקון",LicenseNum="123",Password="123",Organization="בית חולים",PhoneNum="0524666423",UserName="guyha",Role=User.UsersRoles.מנהל},
                new User{UserId=1,BirthDate=DateTime.Parse("01-09-1992"),ValidationPassword="123", Email="guyha10@gmail.com",FirstName="מארק",LastName="חכם",LicenseNum="123",Password="123",Organization="בית חולים",PhoneNum="0524666423",UserName="yaniv",Role=User.UsersRoles.רגיל}
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();


            var trainings = new List<Training>
            {

                
                new Training{TrainingID=1,ExpireDate=2,Name="החייאה", NumberOfMeetings=2,Duration=12,TrainingEnd=DateTime.Parse("18-11-2017") ,Location="חדר 1", TrainingDate=DateTime.Parse("11-11-2017"),ExpirationDate =DateTime.Parse("11-11-2017") },
                new Training{TrainingID=2,ExpireDate=1 ,Name="כיבוי אש", NumberOfMeetings=2,Location="חדר 2",Duration=12,TrainingEnd=DateTime.Parse("5-6-2018") ,TrainingDate=DateTime.Parse("29-05-2018"),ExpirationDate =DateTime.Parse("11-11-2017") },

            };
            trainings.ForEach(s => context.Trainings.Add(s));
            context.SaveChanges();

            var ExrernalTrainings = new List<ExternalTraining>
            {
            new ExternalTraining{ExternalTrainingID=1,type="השתלמות 2",Name="השתלמות רופאים",Cost=200,NumberOfMeetings=2, Duration=7,TrainingEnd=DateTime.Parse("18-11-2017") ,Location="חדר 1", TrainingDate=DateTime.Parse("11-11-2017")},
            new ExternalTraining{ExternalTrainingID=2,type="השתלמות 1",Name="הטמעת מערכות",Cost=200,NumberOfMeetings=2, Location="חדר 2",Duration=3,TrainingEnd=DateTime.Parse("14-11-2017") ,TrainingDate=DateTime.Parse("11-12-2017")},
            };
            ExrernalTrainings.ForEach(s => context.ExternalTrainings.Add(s));
            context.SaveChanges();



            var employee = new List<Employee>
            {
            new Employee{EmployeeID=1,FirstName="ירין",LastName="דולב",BirthDate=DateTime.Parse("11-11-1992"),DepartmentID=1,Gender=Gender.זכר,EmployeeZehut=203958262,PositionPercentage=0.5,RankingID=1,SubSectorID=1,StartDate=DateTime.Parse("11-11-2017"),TrainingBudget=200},
            new Employee{EmployeeID=2,FirstName="שמואל",LastName="לביא",BirthDate=DateTime.Parse("11-11-1993"),DepartmentID=2,Gender=Gender.זכר,EmployeeZehut=203958263,PositionPercentage=0.5,RankingID=2,SubSectorID=2,StartDate=DateTime.Parse("11-11-2017"),TrainingBudget=200},


            };

            employee.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();


            var employeeTraining = new List<EmployeeInTraining>
            {
            new EmployeeInTraining{EmployeeInTrainingID=1,TrainingID=1,EmployeeID=1,IfPass=false }
            };
            employeeTraining.ForEach(s => context.EmployeeInTrainings.Add(s));

            var employeeInExternalTraining = new List<EmployeeInExternalTraining>
            {
            new EmployeeInExternalTraining{EmployeeInExternalTrainingsID=1,ExternalTrainingID=1,EmployeeID=1,TrainingStatus=EmployeeInExternalTraining.TrainingStatuses.ממתין }
            };
            employeeInExternalTraining.ForEach(s => context.EmployeeInExternalTrainings.Add(s));

            context.SaveChanges();

        }
    }
}
