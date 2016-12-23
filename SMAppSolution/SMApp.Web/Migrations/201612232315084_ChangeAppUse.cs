namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAppUse : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SAddresses", "AddL1", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SAddresses", "AddL1", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
