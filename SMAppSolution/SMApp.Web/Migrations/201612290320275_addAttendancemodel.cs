namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttendancemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StuAttendances", "SchoolProfileId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StuAttendances", "SchoolProfileId");
        }
    }
}
