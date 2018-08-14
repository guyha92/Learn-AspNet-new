namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeBudget : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "NumberOfTrainings", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "RemainingTrainings", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "RemainingBudget", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "RemainingBudget");
            DropColumn("dbo.Employees", "RemainingTrainings");
            DropColumn("dbo.Employees", "NumberOfTrainings");
        }
    }
}
