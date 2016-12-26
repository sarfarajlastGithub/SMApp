namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeClubProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentRegs", "ClubName", c => c.Int(nullable: false));
            DropColumn("dbo.StudentProfiles", "ClubName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentProfiles", "ClubName", c => c.Int(nullable: false));
            DropColumn("dbo.StudentRegs", "ClubName");
        }
    }
}
