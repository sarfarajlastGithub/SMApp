using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.SchoolEN
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public SAddress SAddress { get; set; }
        public int SAddressId { get; set; }
        public string SchoolName { get; set; }
        public string Password { get; set; }
        public string CpName { get; set; }
        public string CpPhone { get; set; }
        public SBoard Board { get; set; }
        public string TotalStudent { get; set; }
        public SchoolFType SchoolFType { get; set; }
        public SchoolGType SchoolGType { get; set; }
        public SClass SClass { get; set; }
        public DateTime RegistarDate { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string SchoolPhoneNumber { get; set; }
        public DateTime? AnnulDateOfExam { get; set; }
        public Medium Medium { get; set; }
        public DateTime? EstablishedDate { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}