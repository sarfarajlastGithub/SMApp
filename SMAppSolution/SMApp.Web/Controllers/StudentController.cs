using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMApp.Web.LIB.ViewModels;
using SMApp.Web.LIB.ViewModels.Enums;
using SMApp.Web.LIB.ViewModels.StudentVM;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity;
using SMApp.Web.LIB.Context;
using SMApp.Web.LIB.Models.StudentEN;


namespace SMApp.Web.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {

        public ActionResult StudentSearch()
        {

            return View();
        }

        // GET: Student
        [HttpPost]
        public ActionResult LoadStudent()
        {
            var crid = User.Identity.GetUserId();
            var context = new AppDbContext();
            var studentdb = context.StudentRegs.Where(s => s.SchoolProfileId == crid);
            //var students = studentdb.Select(s =>
            //new StudentSearchVM
            //{
            //    StudentName = s.StudentName,
            //    StuClass = s.StuClass.ToString(),
            //    StuSection = s.StuSection.ToString(),
            //    TenureYear = s.TenureYear.ToString(),
            //    IsActive = s.IsActive
            //}).ToList();

            //return Json(new { data = students, JsonRequestBehavior.AllowGet });

            //jQuery DataTables Param
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find order columns info
            var sortColumn =
                Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault()
                                       + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //find search columns info
            var studentName = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault();
            var sClass = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault();
            var sSection = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var tenure = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault();
            var active = Request.Form.GetValues("columns[4][search][value]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt16(start) : 0;
            int recordsTotal = 0;

            // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            var v = studentdb;

            //SEARCHING...
            if (!string.IsNullOrEmpty(tenure) && Convert.ToInt32(tenure) > 0)
            {
                TenureYear y = EnumUtil.ParseEnum<TenureYear>(tenure);
                v = v.Where(a => a.TenureYear == y);
            }
            if (!string.IsNullOrEmpty(studentName))
            {
                v = v.Where(a => a.StudentName.Contains(studentName));
            }
            if (!string.IsNullOrEmpty(sClass) && Convert.ToInt32(sClass) > 0)
            {
                SClass c = EnumUtil.ParseEnum<SClass>(sClass);
                v = v.Where(a => a.StuClass == c);
            }
            if (!string.IsNullOrEmpty(sSection) && Convert.ToInt32(sSection) > 0)
            {
                SSectionEnum s = EnumUtil.ParseEnum<SSectionEnum>(sSection);
                v = v.Where(a => a.StuSection == s);
            }
            if (!string.IsNullOrEmpty(active))
            {
                bool activee = Convert.ToBoolean(active);
                v = v.Where(a => a.IsActive == activee);
            }
            //SORTING...  (For sorting we need to add a reference System.Linq.Dynamic)
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                switch (sortColumn)
                {
                    case "StuClassString":
                        sortColumn = "StuClass";
                        break;
                    case "StuSectionString":
                        sortColumn = "StuSection";
                        break;
                    case "TenureNameString":
                        sortColumn = "TenureYear";
                        break;
                    case "":
                        sortColumn = "RegId";
                        break;
                }
                v = v.OrderBy(sortColumn + " " + sortColumnDir);
            }

            recordsTotal = v.Count();
            var data = v.Skip(skip).Take(pageSize).Select(s => new StudentSearchVM
            {

                StudentRegId = s.RegId,
                StudentName = s.StudentName,
                StuClassString = s.StuClass.ToString(),
                StuSectionString = s.StuSection.ToString(),
                TenureNameString = s.TenureYear.ToString(),
                IsActive = s.IsActive,

            }).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data },
                JsonRequestBehavior.AllowGet);

        }

        public ActionResult UpgradeStudent(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return View("StudentDetails");
            }

            var crid = User.Identity.GetUserId();
            var context = new AppDbContext();

            if (!context.StudentRegs.Any(s => s.RegId == id))
            {
                return View("StudentSearch");
            }
            var stuReg = context.StudentRegs.First(s => s.RegId == id);
            StudentVM vm = new StudentVM();
            vm.RegId = id;
            vm.AdmissioinDate = DateTimeConvert.GetString(stuReg.AdmissioinDate);
            vm.TenureYear = stuReg.TenureYear;
            vm.Stuclass = stuReg.StuClass;
            vm.StuSection = stuReg.StuSection;
            vm.IsActive = stuReg.IsActive;
            vm.ClubName = stuReg.ClubName;
            vm.StudentProfileId = stuReg.StudentProfileId;
            vm.Name = stuReg.StudentName;
            vm.SchoolId = stuReg.SchoolProfileId;

            return View(vm);
        }

        [HttpPost]
        public ActionResult UpgradeStudent(StudentVM vm)
        {

            var context = new AppDbContext();

            if (context.StudentRegs.Any(s => s.RegId == vm.RegId && s.StuClass == vm.Stuclass))
            {
                var studentReg = context.StudentRegs.First(s => s.RegId == vm.RegId && s.StuClass == vm.Stuclass);
                studentReg.TenureYear = vm.TenureYear;
                studentReg.StuSection = vm.StuSection;
                studentReg.IsActive = vm.IsActive;
                studentReg.AdmissioinDate = DateTimeConvert.GetDate(vm.AdmissioinDate);
                studentReg.ClubName = vm.ClubName;
                context.SaveChanges();
                return View("StudentSearch");
            }

            StudentReg streg = new StudentReg
            {
                RegId = Guid.NewGuid().ToString(),
                AdmissioinDate = DateTimeConvert.GetDate(vm.AdmissioinDate),
                ClubName = vm.ClubName,
                StudentProfileId = vm.StudentProfileId,
                StudentName = vm.Name,
                IsActive = vm.IsActive,
                TenureYear = vm.TenureYear,
                StuClass = vm.Stuclass,
                StuSection = vm.StuSection,
                SchoolProfileId = vm.SchoolId
            };

            context.StudentRegs.Add(streg);
            context.SaveChanges();

            return View("StudentSearch");
        }

        public ActionResult DeleteStudent(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return View("StudentDetails");
            }

            var crid = User.Identity.GetUserId();
            var context = new AppDbContext();

            if (!context.StudentRegs.Any(s => s.RegId == id))
            {
                return View("StudentSearch");
            }
            //While Deleting Student Registration,
            // We Need to delete all respective data which belongs to That Student

            var stuReg = context.StudentRegs.First(s => s.RegId == id);
            var stuProfile = context.StudentProfiles.First(s => s.StudentId == stuReg.StudentProfileId);
            var allstuReg = context.StudentRegs.Where(s => s.StudentProfileId == stuProfile.StudentId);
            context.StudentProfiles.Remove(stuProfile);

            context.StudentRegs.RemoveRange(allstuReg);

            context.SaveChanges();


            return View("StudentSearch");
        }
        public ActionResult StudentDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("StudentDetails");
            }

            return View("StudentDetails", GetStudentVmData(id));
        }


        protected StudentVM GetStudentVmData(string id)
        {
            var student = new StudentVM();

            var crid = User.Identity.GetUserId();
            var context = new AppDbContext();


            //Main Unique id is Student registration Id
            var vm1 = context.StudentRegs.First(s => s.RegId == id);

            student.RegId = vm1.RegId;
            student.SchoolId = vm1.SchoolProfileId;
            student.StudentProfileId = vm1.StudentProfileId;
            student.Name = vm1.StudentName ?? "";
            student.Stuclass = vm1.StuClass;
            student.StuSection = vm1.StuSection;
            student.TenureYear = vm1.TenureYear;
            student.IsActive = vm1.IsActive;
            student.AdmissioinDate = DateTimeConvert.GetString(vm1.AdmissioinDate);
            student.ClubName = vm1.ClubName;


            //Fetch student profile data from student profile id of registration table
            var vm = context.StudentProfiles.Include(x => x.Address).First(s => s.StudentId == vm1.StudentProfileId);


            student.StudentId = vm.StudentId;
            student.StudentProfileId = vm.StudentId;
            student.Name = vm.Name ?? "";
            student.Gender = vm.Gender;
            student.DateOfBirth = DateTimeConvert.GetString(vm.DateOfBirth);
            student.PreEduInfo = vm.PreEduInfo ?? "";
            student.GuardianName = vm.GuardianName ?? "";
            student.GuardianMobile = vm.GuardianMobile ?? "";
            student.GuardialEmail = vm.GuardialEmail ?? "";
            student.GuardianQualification = vm.GuardianQualification;
            student.GuardianOcupation = vm.GuardianOcupation ?? "";
            student.RelationWithGuardian = vm.RelationWithGuardian;
            student.AddL1 = vm.Address.AddL1 ?? "";
            student.AddL2 = vm.Address.AddL2 ?? "";
            student.City = vm.Address.City;
            student.Pin = vm.Address.Pin;
            student.State = vm.Address.State;

            student.SchoolId = crid;


            return student;
        }

        public ActionResult AddStudent(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            return View(GetStudentVmData(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(StudentVM vm)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Mandatory fields Are -Name , -Gender, -DateOfBirth, -GuardianName, -Address Line1, -Pin,City,State -Session, -Class, -Section ");
                return View(vm);
            }

            var stId = Guid.NewGuid().ToString();
            var regId = Guid.NewGuid().ToString();
            //Getting Current
            var crid = User.Identity.GetUserId();
            var context = new AppDbContext();

            //Checking Exist Students
            if (String.IsNullOrEmpty(vm.RegId))
            {
                vm.RegId = "0";
            }
            //checking Exist Registered Student
            if (context.StudentRegs.Any(s => s.RegId == vm.RegId))
            {

                //From Student registration Update
                var studentReg = context.StudentRegs.First(s => s.StudentProfileId == vm.StudentId);


                studentReg.StudentName = vm.Name;
                studentReg.StuClass = vm.Stuclass;
                studentReg.StuSection = vm.StuSection;
                studentReg.TenureYear = vm.TenureYear;
                studentReg.IsActive = vm.IsActive;
                studentReg.AdmissioinDate = DateTimeConvert.GetDate(vm.AdmissioinDate);
                studentReg.ClubName = vm.ClubName;

                //For Student Profile Update
                var student = context.StudentProfiles.First(s => s.StudentId == vm.StudentId);

                student.Name = vm.Name;
                student.Gender = vm.Gender;
                student.DateOfBirth = DateTimeConvert.GetDate(vm.DateOfBirth);
                student.PreEduInfo = vm.PreEduInfo;
                student.GuardianName = vm.GuardianName;
                student.GuardianMobile = vm.GuardianMobile;
                student.GuardialEmail = vm.GuardialEmail;
                student.GuardianQualification = vm.GuardianQualification;
                student.GuardianOcupation = vm.GuardianOcupation;
                student.RelationWithGuardian = vm.RelationWithGuardian;



                student.Address = new Address
                {
                    AddL1 = vm.AddL1,
                    AddL2 = vm.AddL2,
                    City = vm.City,
                    Pin = vm.Pin,
                    State = vm.State
                };
                //Saving Changes
                context.SaveChanges();

                return View("StudentSearch");
            }




            StudentProfile stp = new StudentProfile();

            stp.StudentId = stId;
            stp.Name = vm.Name;
            stp.Gender = vm.Gender;
            stp.DateOfBirth = string.IsNullOrEmpty(vm.DateOfBirth) ? DateTime.Today : DateTimeConvert.GetDate(vm.DateOfBirth);
            stp.PreEduInfo = vm.PreEduInfo;
            stp.GuardianName = vm.GuardianName;
            stp.GuardianMobile = vm.GuardianMobile;
            stp.GuardialEmail = vm.GuardialEmail;
            stp.GuardianQualification = vm.GuardianQualification;
            stp.GuardianOcupation = vm.GuardianOcupation;
            stp.RelationWithGuardian = vm.RelationWithGuardian;


            stp.Address = new Address
            {
                AddL1 = vm.AddL1,
                AddL2 = vm.AddL2,
                City = vm.City,
                Pin = vm.Pin,
                State = vm.State
            };

            context.StudentProfiles.Add(stp);
            context.SaveChanges();

            //Saving into Student registration table

            StudentReg stg = new StudentReg()
            {
                RegId = regId,
                SchoolProfileId = crid,
                StudentProfileId = stId,
                StudentName = vm.Name,
                StuClass = vm.Stuclass,
                StuSection = vm.StuSection,
                TenureYear = vm.TenureYear,
                IsActive = vm.IsActive,
                AdmissioinDate = string.IsNullOrEmpty(vm.AdmissioinDate) ? DateTime.Today : DateTimeConvert.GetDate(vm.AdmissioinDate),
                ClubName = vm.ClubName
            };
            context.StudentRegs.Add(stg);
            context.SaveChanges();

            return View("StudentSearch");
        }
    }
}