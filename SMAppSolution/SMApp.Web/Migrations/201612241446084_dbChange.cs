namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UpdateViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        SchoolName = c.String(nullable: false),
                        SchoolFType = c.Int(nullable: false),
                        SchoolGType = c.Int(nullable: false),
                        TotalStudent = c.String(nullable: false, maxLength: 6),
                        SchoolPhoneNumber = c.String(nullable: false),
                        CpName = c.String(nullable: false),
                        CpPhone = c.String(nullable: false),
                        Board = c.Int(nullable: false),
                        AnnulDateOfExam = c.String(),
                        Medium = c.Int(nullable: false),
                        EstablishedDate = c.String(),
                        ClassAndSection_Name = c.Int(nullable: false),
                        SClassEnum = c.Int(nullable: false),
                        SSectionEnum = c.Int(nullable: false),
                        RegistarDate = c.DateTime(nullable: false),
                        LastUpdatedDate = c.String(),
                        PhoneNumber = c.String(),
                        AddL1 = c.String(nullable: false, maxLength: 300),
                        AddL2 = c.String(maxLength: 300),
                        Pin = c.Int(nullable: false),
                        City = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UpdateViewModels");
        }
    }
}
