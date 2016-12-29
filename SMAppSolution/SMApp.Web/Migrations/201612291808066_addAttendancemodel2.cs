namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttendancemodel2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StuAttendances", "StudentRegId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StuAttendances", "StudentRegId", c => c.String());
        }
    }
}
