using System;
using System.Linq;
using System.Web.Mvc;
using SMApp.Web.LIB.BL.Attendance;
using SMApp.Web.LIB.ViewModels;
using SMApp.Web.LIB.ViewModels.StudentVM;

namespace SMApp.Web.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {

        #region Private/protected fields

        protected readonly StudentAttendance _repository = new StudentAttendance();

        #endregion



        public ActionResult ViewAttendance()
        {

            return View();
        }

        [HttpPost]
        public JsonResult AttendanceListByFiter(string name = "", string tenureyear = "", string stuclass = "", string stuSection = "",string selecteDate="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                var studentCount = _repository.GetAttendanceCountByFilter(name, tenureyear, stuclass, stuSection,selecteDate);
                var students = _repository.GetAttendanceByFilter(name, tenureyear, stuclass, stuSection,selecteDate, jtStartIndex, jtPageSize, jtSorting);
                var stStudents = students.Select(s => new ViewnAttendanceVM
                {
                    Name = s.StudentReg.StudentName,
                    Pdate = DateTimeConvert.GetString(s.PresentDate),
                    Sclass = s.StudentReg.StuClass.ToString(),
                    Ssection = s.StudentReg.StuSection.ToString(),
                    TenureName = s.StudentReg.TenureYear.ToString(),
                    Status = GetStatus(s.IsPresent)
                });
                return Json(new { Result = "OK", Records = stStudents, TotalRecordCount = studentCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private string GetStatus(bool isPresent)
        {
            if (isPresent)
            {
                return "Present";
            }
            else
            {
                return "Absent";
            }
        }

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
                message = "Attendance Complete";
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

                var stStudents = students.Select(s => new ViewnAttendanceVM
                {
                    RegId = s.RegId,
                    Name = s.StudentName,
                    Sclass = s.StuClass.ToString(),
                    Ssection = s.StuSection.ToString(),
                    TenureName = s.TenureYear.ToString()
                });
                return Json(new { Result = "OK", Records = stStudents, TotalRecordCount = studentCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

    }

}