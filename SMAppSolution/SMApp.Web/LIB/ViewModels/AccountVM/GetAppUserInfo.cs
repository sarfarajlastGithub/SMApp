using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SMApp.Web.LIB.Context;
using SMApp.Web.LIB.Models.SchoolEN;

namespace SMApp.Web.LIB.ViewModels.AccountVM
{
    public class GetAppUserInfo
    {
        public GetAppUserInfo()
        {
            var context = new AppDbContext();
            CurrentUserId = HttpContext.Current.User.Identity.GetUserId();
            CurrentAppUser = context.Users.First(u => u.Id == CurrentUserId);
            ApplicationUsers = context.Users.ToList();

        }
        
        public string CurrentUserId { get; private set; }
        public ApplicationUser CurrentAppUser { get; private set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}