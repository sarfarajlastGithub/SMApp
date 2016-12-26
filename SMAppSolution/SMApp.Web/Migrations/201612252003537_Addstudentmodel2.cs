namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addstudentmodel2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentRegs", "SchoolProfileId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.StudentRegs", "SchoolProfileId");
            AddForeignKey("dbo.StudentRegs", "SchoolProfileId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentRegs", "SchoolProfileId", "dbo.AspNetUsers");
            DropIndex("dbo.StudentRegs", new[] { "SchoolProfileId" });
            AlterColumn("dbo.StudentRegs", "SchoolProfileId", c => c.String(nullable: false));
        }
    }
}
