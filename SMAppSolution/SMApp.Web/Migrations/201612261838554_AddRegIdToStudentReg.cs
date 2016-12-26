namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegIdToStudentReg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentRegs", "RegId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentRegs", "RegId");
        }
    }
}
