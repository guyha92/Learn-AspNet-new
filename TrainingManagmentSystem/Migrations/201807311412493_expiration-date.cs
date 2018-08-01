namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class expirationdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainings", "ExpirationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trainings", "ExpirationDate");
        }
    }
}
