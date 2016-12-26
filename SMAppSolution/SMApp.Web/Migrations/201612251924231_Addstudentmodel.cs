namespace SMApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addstudentmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.String(),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                        FileId = c.Int(nullable: false),
                        PhotoLocation = c.String(),
                        PreEduInfo = c.String(),
                        GuardianName = c.String(),
                        GuardianMobile = c.String(),
                        GuardialEmail = c.String(),
                        GuardianQualification = c.Int(nullable: false),
                        GuardianOcupation = c.String(),
                        RelationWithGuardian = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        MotherName = c.String(),
                        MotherQualification = c.String(),
                        MotherOcupation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddL1 = c.String(nullable: false, maxLength: 300),
                        AddL2 = c.String(maxLength: 300),
                        Pin = c.Int(nullable: false),
                        City = c.Int(nullable: false),
                        state = c.Int(nullable: false),
                        StudentProfileId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentRegs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchoolProfileId = c.String(nullable: false),
                        StudentProfileId = c.String(nullable: false),
                        StudentName = c.String(),
                        StuClass = c.Int(nullable: false),
                        StuSection = c.Int(nullable: false),
                        TenureYear = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        AdmissioinDate = c.DateTime(),
                        StudentProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentProfiles", t => t.StudentProfile_Id)
                .Index(t => t.StudentProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentRegs", "StudentProfile_Id", "dbo.StudentProfiles");
            DropForeignKey("dbo.StudentProfiles", "AddressId", "dbo.Addresses");
            DropIndex("dbo.StudentRegs", new[] { "StudentProfile_Id" });
            DropIndex("dbo.StudentProfiles", new[] { "AddressId" });
            DropTable("dbo.StudentRegs");
            DropTable("dbo.Addresses");
            DropTable("dbo.StudentProfiles");
        }
    }
}
