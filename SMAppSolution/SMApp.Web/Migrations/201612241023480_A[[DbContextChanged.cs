namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADbContextChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SAddresses", "AddL1", c => c.String());
            AlterColumn("dbo.SAddresses", "AddL2", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SAddresses", "AddL2", c => c.String(maxLength: 300));
            AlterColumn("dbo.SAddresses", "AddL1", c => c.String(maxLength: 300));
        }
    }
}
