namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassSectionTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassAndSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolProfileId = c.String(nullable: false),
                        SClass = c.Int(nullable: false),
                        SSection = c.Int(nullable: false),
                        NoOfStudentEachSection = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ClassAndSections");
        }
    }
}
