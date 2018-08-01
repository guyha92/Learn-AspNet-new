namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeExternalTraining : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EmployeeInTrainings", "IsSelected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeInTrainings", "IsSelected", c => c.Boolean(nullable: false));
        }
    }
}
