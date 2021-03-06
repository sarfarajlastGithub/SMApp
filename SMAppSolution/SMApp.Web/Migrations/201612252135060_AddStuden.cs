namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStuden : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentProfiles", "DateOfAdded", c => c.DateTime());
            AlterColumn("dbo.Addresses", "AddL1", c => c.String());
            AlterColumn("dbo.Addresses", "AddL2", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "AddL2", c => c.String(maxLength: 300));
            AlterColumn("dbo.Addresses", "AddL1", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.StudentProfiles", "DateOfAdded");
        }
    }
}
