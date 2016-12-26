namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StuAttendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentRegId = c.String(),
                        PresentDate = c.DateTime(),
                        IsPresent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StuAttendances");
        }
    }
}
