using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SMApp.Web.LIB.Context;
using SMApp.Web.LIB.Models.StudentEN;
using SMApp.Web.LIB.ViewModels;
using SMApp.Web.LIB.ViewModels.AccountVM;
using SMApp.Web.LIB.ViewModels.Enums;
using WebGrease.Css.Ast.Selectors;

namespace SMApp.Web.LIB.BL.Attendance
{
    public class Present
    {
        private readonly AppDbContext _context;
        private readonly GetAppUserInfo _crUserInfo;

        public Present()
        {
            _context = new AppDbContext();
            _crUserInfo = new GetAppUserInfo();
        }
        

        public async Task PresentStudentData(masterStudent ms)
        {
            var context = new AppDbContext();
            

            //var isPresent = EnumUtil.ParseEnum<PresentStatus>(ms.PresentStatus);
            //switch (isPresent)
            //{
            //    case PresentStatus.Absent:
            //        DoAbcent(ms);
            //        break;
            //    case PresentStatus.Present:
            //        DoPresent(ms);
            //        break;
            //}
            //Stard
            #region Present Function
            var isPresent = EnumUtil.ParseEnum<PresentStatus>(ms.PresentStatus);
            string jsonString = ms.Ids;
            DateTime? pdate = DateTimeConvert.GetDate(ms.Pdate);

            //Student present
            var studentPresentlist = JsonConvert.DeserializeObject<List<StudentIdName>>(jsonString);
            var crusr = _crUserInfo.CurrentUserId;

            var sclass = EnumUtil.ParseEnum<SClass>(studentPresentlist[0].StuClass);
            var ssection = EnumUtil.ParseEnum<SSectionEnum>(studentPresentlist[0].StuSection);
            var tenure = EnumUtil.ParseEnum<TenureYear>(studentPresentlist[0].Tyear);

            // All registered Student who is sam class section and tenure
            var allRegisterdStudent = _context.StudentRegs.Where(s => s.SchoolProfileId == crusr &&
                                                s.TenureYear == tenure &&
                                                s.StuClass == sclass &&
                                                s.StuSection == ssection).ToList();

            //Creating list of Id
            var tempIdList = studentPresentlist.Select(q => q.Id).ToList();

            // All Absent Students list Which id is not present in tempList id
            var studentAbsentlist = allRegisterdStudent.Where(q => !tempIdList.Contains(q.RegId));

            //Fetch Attendance module
            var attendanceModel = _context.StuAttendances.Where(a => a.SchoolProfileId == crusr).ToList();
            //Create List for Saving new data model at one time
            List<StuAttendance> attendanceList = null;

            //Iterate through All Registered Student 
            //on 
            //same School
            //same Class
            //same Section 
            foreach (var presentStudent in studentPresentlist)
            {
                //Checking that any exist data is here 
                // if present
                // then Update
                //Or Add to a list for Add save changes
                if (attendanceModel.Any(a => a.PresentDate == pdate && a.StudentRegId == presentStudent.Id))
                {
                    StuAttendance st = attendanceModel.First(a => a.PresentDate == pdate && a.StudentRegId == presentStudent.Id);
                    if (isPresent == PresentStatus.Absent)
                    {
                        st.IsPresent = false;
                    }
                    else if (isPresent == PresentStatus.Present)
                    {
                        st.IsPresent = true;
                    }

                    //   attendanceList.Add(st);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var sa = new StuAttendance();
                    if (isPresent == PresentStatus.Absent)
                    {
                        sa.IsPresent = false;
                    }
                    else if (isPresent == PresentStatus.Present)
                    {
                        sa.IsPresent = true;
                    }
                    sa.PresentDate = pdate;
                    sa.SchoolProfileId = crusr;
                    sa.StudentRegId = presentStudent.Id;
                    _context.StuAttendances.Add(sa);
                }
            }
            // This is for updating remain student
            foreach (var presentStudent in studentAbsentlist)
            {
                //Checking that any exist data is here 
                // if present
                // then Update
                //Or Add to a list for Add save changes
                if (attendanceModel.Any(a => a.PresentDate == pdate && a.StudentRegId == presentStudent.RegId))
                {
                    StuAttendance st = attendanceModel.First(a => a.PresentDate == pdate && a.StudentRegId == presentStudent.RegId);
                    if (isPresent == PresentStatus.Absent)
                    {
                        st.IsPresent = true;
                    }
                    else if (isPresent == PresentStatus.Present)
                    {
                        st.IsPresent = false;
                    }

                    //   attendanceList.Add(st);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var sa = new StuAttendance();
                    if (isPresent == PresentStatus.Absent)
                    {
                        sa.IsPresent = true;
                    }
                    else if (isPresent == PresentStatus.Present)
                    {
                        sa.IsPresent = false;
                    }
                    sa.PresentDate = pdate;
                    sa.SchoolProfileId = crusr;
                    sa.StudentRegId = presentStudent.RegId;
                    _context.StuAttendances.Add(sa);
                }
            }

            // _context.StuAttendances.AddRange(attendanceList);
            await _context.SaveChangesAsync();

            #endregion

            //End
        }

        private async void DoPresent(masterStudent ms)
        {
            
        }

        private async void DoAbcent(masterStudent ms)
        {
            string jsonString = ms.Ids;
            DateTime? pdate = DateTimeConvert.GetDate(ms.Pdate);

            //Student present
            var studentAbsentlist = JsonConvert.DeserializeObject<List<StudentIdName>>(jsonString);
            var crusr = _crUserInfo.CurrentUserId;

            var sclass = EnumUtil.ParseEnum<SClass>(studentAbsentlist[0].StuClass);
            var ssection = EnumUtil.ParseEnum<SSectionEnum>(studentAbsentlist[0].StuSection);
            var tenure = EnumUtil.ParseEnum<TenureYear>(studentAbsentlist[0].Tyear);

            // All registered Student who is sam class section and tenure
            var allRegisterdStudent = _context.StudentRegs.Where(s => s.SchoolProfileId == crusr &&
                                                s.TenureYear == tenure &&
                                                s.StuClass == sclass &&
                                                s.StuSection == ssection);

            //Creating list of Id
            var tempIdList = studentAbsentlist.Select(q => q.Id).ToList();

            // All Absent Students list Which id is not present in tempList id
            var studentPresentlist = allRegisterdStudent.Where(q => !tempIdList.Contains(q.RegId));

            var attendanceModel = _context.StuAttendances.Where(a => a.SchoolProfileId == crusr);

            List<StuAttendance> attendanceList = null;
            foreach (var absentStudent in studentAbsentlist)
            {

                if (attendanceModel.Any(a => a.PresentDate == pdate && a.StudentRegId == absentStudent.Id))
                {
                    StuAttendance st = attendanceModel.First(a => a.PresentDate == pdate && a.StudentRegId == absentStudent.Id);
                    st.IsPresent = false;
                    //   attendanceList.Add(st);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var sa = new StuAttendance();
                    sa.IsPresent = false;
                    sa.PresentDate = pdate;
                    sa.SchoolProfileId = crusr;
                    sa.StudentRegId = absentStudent.Id;
                    attendanceList.Add(sa);
                }
            }

            foreach (var presentStudent in studentPresentlist)
            {
                if (attendanceModel.Any(a => a.PresentDate == pdate && a.StudentRegId == presentStudent.RegId))
                {
                    StuAttendance st = attendanceModel.First(a => a.PresentDate == pdate && a.StudentRegId == presentStudent.RegId);
                    st.IsPresent = true;
                    //   attendanceList.Add(st);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var sa = new StuAttendance();
                    sa.IsPresent = true;
                    sa.PresentDate = pdate;
                    sa.SchoolProfileId = crusr;
                    sa.StudentRegId = presentStudent.RegId;
                    attendanceList.Add(sa);
                }
            }

            _context.StuAttendances.AddRange(attendanceList);
            await _context.SaveChangesAsync();
        }
    }
}