namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTenure : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StuAttendances", "RolNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StuAttendances", "RolNo");
        }
    }
}
