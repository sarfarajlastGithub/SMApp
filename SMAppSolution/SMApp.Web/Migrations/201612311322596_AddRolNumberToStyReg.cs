namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRolNumberToStyReg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentRegs", "RolNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentRegs", "RolNo");
        }
    }
}
