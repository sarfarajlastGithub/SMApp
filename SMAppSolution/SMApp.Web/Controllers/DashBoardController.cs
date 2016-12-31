using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMApp.Web.LIB.BL.Account;
using SMApp.Web.LIB.ViewModels;

namespace SMApp.Web.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index(DashBoardVM model)
        {
            var profile = new SchoolProfile();
            model.StudentCount = profile.GetTotalStudents();
            if (model.IsProfileComplete)
            {
                model.ColorClass = "panel-green";
            }
            else
            {
                model.ColorClass = "panel-red";
            }
            return View(model);
        }


    }
}