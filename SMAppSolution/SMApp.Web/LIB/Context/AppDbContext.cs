using Microsoft.AspNet.Identity.EntityFramework;
using SMApp.Web.LIB.Models.SchoolEN;

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
    }
}