using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SMApp.Web.LIB.BL.Attendance;
using SMApp.Web.LIB.Context;
using SMApp.Web.LIB.ViewModels;

namespace SMApp.Web.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {

        #region Private/protected fields

        protected readonly StudentAttendance _repository = new StudentAttendance();

        #endregion


        // GET: Attendance
        #region Student actions

        public ActionResult PresentStudent()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> PresentStudent(masterStudent ms)
        {
            string status = null;
            string message = null;
            try
            {
                var present = new Present();
                await present.PresentStudentData(ms);
                message = "Success";
            }
            catch (Exception ex)
            {
                message = ex.Message;

            }



            return new JsonResult { Data = new { status = status, message = message } };
        }



        [HttpPost]
        public JsonResult StudentListByFiter(string name = "", string tenureyear = "", string stuclass = "", string stuSection= "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var studentCount = _repository.GetStudentCountByFilter(name, tenureyear, stuclass, stuSection);
                var students = _repository.GetStudentsByFilter(name, tenureyear, stuclass, stuSection, jtStartIndex, jtPageSize, jtSorting);
                return Json(new { Result = "OK", Records = students, TotalRecordCount = studentCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

    }

}