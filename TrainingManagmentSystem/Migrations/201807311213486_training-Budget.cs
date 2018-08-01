namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trainingBudget : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "TrainingBudget", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "TrainingBudget");
        }
    }
}
