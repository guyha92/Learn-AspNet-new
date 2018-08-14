namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loginattempts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FailedLoginAttempts", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FailedLoginAttempts");
        }
    }
}
