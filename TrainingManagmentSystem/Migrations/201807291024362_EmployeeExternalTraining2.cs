namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeExternalTraining2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeInExternalTrainings",
                c => new
                    {
                        EmployeeInExternalTrainingsID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        ExternalTrainingID = c.Int(nullable: false),
                        TrainingStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeInExternalTrainingsID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.ExternalTrainings", t => t.ExternalTrainingID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.ExternalTrainingID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeInExternalTrainings", "ExternalTrainingID", "dbo.ExternalTrainings");
            DropForeignKey("dbo.EmployeeInExternalTrainings", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeInExternalTrainings", new[] { "ExternalTrainingID" });
            DropIndex("dbo.EmployeeInExternalTrainings", new[] { "EmployeeID" });
            DropTable("dbo.EmployeeInExternalTrainings");
        }
    }
}
