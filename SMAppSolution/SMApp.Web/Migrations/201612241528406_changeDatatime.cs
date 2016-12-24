namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDatatime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "RegistarDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "RegistarDate", c => c.DateTime(nullable: false));
        }
    }
}
