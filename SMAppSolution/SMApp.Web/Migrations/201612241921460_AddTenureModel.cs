namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTenureModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TenureTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolProfileId = c.String(nullable: false),
                        TenureYearName = c.Int(nullable: false),
                        TenureStartDate = c.DateTime(),
                        TenureEndDate = c.DateTime(),
                        IsSet = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TenureTimes");
        }
    }
}
