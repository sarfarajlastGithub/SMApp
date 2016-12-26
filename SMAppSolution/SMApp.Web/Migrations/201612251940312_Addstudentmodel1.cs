namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addstudentmodel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentRegs", "StudentProfile_Id", "dbo.StudentProfiles");
            DropIndex("dbo.StudentRegs", new[] { "StudentProfile_Id" });
            AlterColumn("dbo.StudentRegs", "StudentProfileId", c => c.String());
            DropColumn("dbo.StudentRegs", "StudentProfile_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentRegs", "StudentProfile_Id", c => c.Int());
            AlterColumn("dbo.StudentRegs", "StudentProfileId", c => c.String(nullable: false));
            CreateIndex("dbo.StudentRegs", "StudentProfile_Id");
            AddForeignKey("dbo.StudentRegs", "StudentProfile_Id", "dbo.StudentProfiles", "Id");
        }
    }
}
