using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SMApp.Web.LIB.Models.SchoolEN;
using SMApp.Web.LIB.Models.StudentEN;

namespace SMApp.Web.LIB.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
        
        public DbSet<SAddress> SAddresses { get; set; }

        public DbSet<ClassAndSection> ClassAndSection { get; set; }

        public DbSet<TenureTime> TenureTimes { get; set; }

        public DbSet<StudentProfile> StudentProfiles { get; set; }

        public DbSet<StudentReg> StudentRegs { get; set; }
    }
}