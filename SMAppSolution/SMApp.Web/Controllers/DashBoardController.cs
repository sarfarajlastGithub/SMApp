using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMApp.Web.LIB.ViewModels;

namespace SMApp.Web.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index(DashBoardVM model)
        {
            return View(model);
        }
    }
}