namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueZehut : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Employees", "EmployeeZehut", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "EmployeeZehut" });
        }
    }
}
