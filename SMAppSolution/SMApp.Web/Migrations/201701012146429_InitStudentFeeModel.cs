namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitStudentFeeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConseOrFineAmountClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolId = c.String(),
                        OnAmount = c.Double(nullable: false),
                        DaOrPa = c.Int(nullable: false),
                        FeeOrConcession = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(),
                        FeeConsessionCategory_Id = c.Int(),
                        OnFeeMainCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FeeConsessionOrFineCategories", t => t.FeeConsessionCategory_Id)
                .ForeignKey("dbo.FeeMainCategories", t => t.OnFeeMainCategory_Id)
                .Index(t => t.FeeConsessionCategory_Id)
                .Index(t => t.OnFeeMainCategory_Id);
            
            CreateTable(
                "dbo.FeeConsessionOrFineCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SchoolId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeeMainCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SchoolId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MainFeeAmountClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SClass = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        SchoolId = c.String(),
                        CreateDateTime = c.DateTime(),
                        FeeMainCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FeeMainCategories", t => t.FeeMainCategory_Id)
                .Index(t => t.FeeMainCategory_Id);
            
            CreateTable(
                "dbo.ScheduleFees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDateTime = c.DateTime(),
                        EndDateTime = c.DateTime(),
                        NotifyDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentWiseFineOrConsessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(),
                        Amount = c.Double(nullable: false),
                        DaOrPa = c.Int(nullable: false),
                        SchoolId = c.String(),
                        FeeMainCategory_Id = c.Int(),
                        StudentReg_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FeeMainCategories", t => t.FeeMainCategory_Id)
                .ForeignKey("dbo.StudentRegs", t => t.StudentReg_Id)
                .Index(t => t.FeeMainCategory_Id)
                .Index(t => t.StudentReg_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentWiseFineOrConsessions", "StudentReg_Id", "dbo.StudentRegs");
            DropForeignKey("dbo.StudentWiseFineOrConsessions", "FeeMainCategory_Id", "dbo.FeeMainCategories");
            DropForeignKey("dbo.MainFeeAmountClasses", "FeeMainCategory_Id", "dbo.FeeMainCategories");
            DropForeignKey("dbo.ConseOrFineAmountClasses", "OnFeeMainCategory_Id", "dbo.FeeMainCategories");
            DropForeignKey("dbo.ConseOrFineAmountClasses", "FeeConsessionCategory_Id", "dbo.FeeConsessionOrFineCategories");
            DropIndex("dbo.StudentWiseFineOrConsessions", new[] { "StudentReg_Id" });
            DropIndex("dbo.StudentWiseFineOrConsessions", new[] { "FeeMainCategory_Id" });
            DropIndex("dbo.MainFeeAmountClasses", new[] { "FeeMainCategory_Id" });
            DropIndex("dbo.ConseOrFineAmountClasses", new[] { "OnFeeMainCategory_Id" });
            DropIndex("dbo.ConseOrFineAmountClasses", new[] { "FeeConsessionCategory_Id" });
            DropTable("dbo.StudentWiseFineOrConsessions");
            DropTable("dbo.ScheduleFees");
            DropTable("dbo.MainFeeAmountClasses");
            DropTable("dbo.FeeMainCategories");
            DropTable("dbo.FeeConsessionOrFineCategories");
            DropTable("dbo.ConseOrFineAmountClasses");
        }
    }
}
