namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeequalification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeQualifications",
                c => new
                    {
                        EmployeeQualificationID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        QualificationID = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeQualificationID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Qualifications", t => t.QualificationID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.QualificationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeQualifications", "QualificationID", "dbo.Qualifications");
            DropForeignKey("dbo.EmployeeQualifications", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeQualifications", new[] { "QualificationID" });
            DropIndex("dbo.EmployeeQualifications", new[] { "EmployeeID" });
            DropTable("dbo.EmployeeQualifications");
        }
    }
}
