namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttendancemodel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StuAttendances", "StudentReg_Id", c => c.Int());
            CreateIndex("dbo.StuAttendances", "StudentReg_Id");
            AddForeignKey("dbo.StuAttendances", "StudentReg_Id", "dbo.StudentRegs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StuAttendances", "StudentReg_Id", "dbo.StudentRegs");
            DropIndex("dbo.StuAttendances", new[] { "StudentReg_Id" });
            DropColumn("dbo.StuAttendances", "StudentReg_Id");
        }
    }
}
