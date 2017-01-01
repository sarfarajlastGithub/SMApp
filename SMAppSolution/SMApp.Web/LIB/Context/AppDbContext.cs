using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SMApp.Web.LIB.Models.SchoolEN;
using SMApp.Web.LIB.Models.StudentEN;
using SMApp.Web.LIB.Models.StudentFeeEN;

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

        public DbSet<StuAttendance> StuAttendances { get; set; }


        //For Fee
        public DbSet<ConseOrFineAmountClass> ConseOrFineAmountClasses { get; set; }

        public DbSet<FeeConsessionOrFineCategory> FeeConsessionOrFineCategories { get; set; }

        public DbSet<FeeMainCategory> FeeMainCategories { get; set; }

        public DbSet<MainFeeAmountClass> MainFeeAmountClasses { get; set; }

        public DbSet<ScheduleFee> ScheduleFees { get; set; }

        public DbSet<StudentWiseFineOrConsession> StudentWiseFineOrConsessions { get; set; }
    }
}