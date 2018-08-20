namespace TrainingManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qualificationtraining : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainings", "QualificationID", c => c.Int(nullable: false,defaultValue: 1));
            CreateIndex("dbo.Trainings", "QualificationID");
            AddForeignKey("dbo.Trainings", "QualificationID", "dbo.Qualifications", "QualificationID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "QualificationID", "dbo.Qualifications");
            DropIndex("dbo.Trainings", new[] { "QualificationID" });
            DropColumn("dbo.Trainings", "QualificationID");
        }
    }
}
