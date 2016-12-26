namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClubName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentProfiles", "ClubName", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentProfiles", "ClubName");
        }
    }
}
