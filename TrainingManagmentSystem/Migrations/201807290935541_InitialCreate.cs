namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Degrees",
                c => new
                    {
                        DegreeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DegreeID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DepartmentID = c.String(),
                        Department_DepartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentID)
                .Index(t => t.Department_DepartmentID);
            
            CreateTable(
                "dbo.EmployeeInTrainings",
                c => new
                    {
                        EmployeeInTrainingID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        TrainingID = c.Int(nullable: false),
                        IfPass = c.Boolean(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeInTrainingID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.TrainingID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.TrainingID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeZehut = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                        SubSectorID = c.Int(nullable: false),
                        RankingID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        PositionPercentage = c.Double(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        תואר_DegreeID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.SubSectors", t => t.SubSectorID, cascadeDelete: true)
                .ForeignKey("dbo.Rankings", t => t.RankingID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Degrees", t => t.תואר_DegreeID)
                .Index(t => t.DepartmentID)
                .Index(t => t.SubSectorID)
                .Index(t => t.RankingID)
                .Index(t => t.תואר_DegreeID);
            
            CreateTable(
                "dbo.SubSectors",
                c => new
                    {
                        SubSectorID = c.Int(nullable: false, identity: true),
                        SubSectortype = c.String(),
                        SectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubSectorID)
                .ForeignKey("dbo.Sectors", t => t.SectorID, cascadeDelete: true)
                .Index(t => t.SectorID);
            
            CreateTable(
                "dbo.Sectors",
                c => new
                    {
                        SectorID = c.Int(nullable: false, identity: true),
                        SectorType = c.String(),
                    })
                .PrimaryKey(t => t.SectorID);
            
            CreateTable(
                "dbo.Rankings",
                c => new
                    {
                        RankingID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SectorID = c.Int(),
                    })
                .PrimaryKey(t => t.RankingID)
                .ForeignKey("dbo.Sectors", t => t.SectorID)
                .Index(t => t.SectorID);
            
            CreateTable(
                "dbo.TrainingSubSectors",
                c => new
                    {
                        TrainingSubSectorID = c.Int(nullable: false, identity: true),
                        TrainingID = c.Int(nullable: false),
                        SubSectorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrainingSubSectorID)
                .ForeignKey("dbo.SubSectors", t => t.SubSectorID, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.TrainingID, cascadeDelete: true)
                .Index(t => t.TrainingID)
                .Index(t => t.SubSectorID);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        TrainingID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TrainingDate = c.DateTime(nullable: false),
                        NumberOfMeetings = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        TrainingEnd = c.DateTime(nullable: false),
                        ExpireDate = c.Int(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.TrainingID);
            
            CreateTable(
                "dbo.ExternalTrainings",
                c => new
                    {
                        ExternalTrainingID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        type = c.String(),
                        TrainingDate = c.DateTime(nullable: false),
                        Duration = c.Double(nullable: false),
                        NumberOfMeetings = c.Int(nullable: false),
                        TrainingEnd = c.DateTime(nullable: false),
                        Location = c.String(),
                        Cost = c.Single(),
                    })
                .PrimaryKey(t => t.ExternalTrainingID);
            
            CreateTable(
                "dbo.LoginModels",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        PhoneNum = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        ValidationPassword = c.String(nullable: false, maxLength: 50),
                        Organization = c.String(maxLength: 50),
                        LicenseNum = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "תואר_DegreeID", "dbo.Degrees");
            DropForeignKey("dbo.Employees", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Employees", "RankingID", "dbo.Rankings");
            DropForeignKey("dbo.TrainingSubSectors", "TrainingID", "dbo.Trainings");
            DropForeignKey("dbo.EmployeeInTrainings", "TrainingID", "dbo.Trainings");
            DropForeignKey("dbo.TrainingSubSectors", "SubSectorID", "dbo.SubSectors");
            DropForeignKey("dbo.SubSectors", "SectorID", "dbo.Sectors");
            DropForeignKey("dbo.Rankings", "SectorID", "dbo.Sectors");
            DropForeignKey("dbo.Employees", "SubSectorID", "dbo.SubSectors");
            DropForeignKey("dbo.EmployeeInTrainings", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Reports", "Department_DepartmentID", "dbo.Departments");
            DropIndex("dbo.TrainingSubSectors", new[] { "SubSectorID" });
            DropIndex("dbo.TrainingSubSectors", new[] { "TrainingID" });
            DropIndex("dbo.Rankings", new[] { "SectorID" });
            DropIndex("dbo.SubSectors", new[] { "SectorID" });
            DropIndex("dbo.Employees", new[] { "תואר_DegreeID" });
            DropIndex("dbo.Employees", new[] { "RankingID" });
            DropIndex("dbo.Employees", new[] { "SubSectorID" });
            DropIndex("dbo.Employees", new[] { "DepartmentID" });
            DropIndex("dbo.EmployeeInTrainings", new[] { "TrainingID" });
            DropIndex("dbo.EmployeeInTrainings", new[] { "EmployeeID" });
            DropIndex("dbo.Reports", new[] { "Department_DepartmentID" });
            DropTable("dbo.Users");
            DropTable("dbo.LoginModels");
            DropTable("dbo.ExternalTrainings");
            DropTable("dbo.Trainings");
            DropTable("dbo.TrainingSubSectors");
            DropTable("dbo.Rankings");
            DropTable("dbo.Sectors");
            DropTable("dbo.SubSectors");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeInTrainings");
            DropTable("dbo.Reports");
            DropTable("dbo.Departments");
            DropTable("dbo.Degrees");
        }
    }
}
