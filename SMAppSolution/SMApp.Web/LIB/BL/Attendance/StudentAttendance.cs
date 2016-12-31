using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SMApp.Web.LIB.Context;
using SMApp.Web.LIB.Models.SchoolEN;
using SMApp.Web.LIB.Models.StudentEN;
using SMApp.Web.LIB.ViewModels;
using SMApp.Web.LIB.ViewModels.Enums;
using Microsoft.AspNet.Identity;
using SMApp.Web.LIB.ViewModels.AccountVM;

namespace SMApp.Web.LIB.BL.Attendance
{
    public class StudentAttendance
    {
        private readonly AppDbContext _dataSource;

        public StudentAttendance(AppDbContext dataSource)
        {
            _dataSource = dataSource;
        }

        public StudentAttendance()
        {
            _dataSource = AppDbContext.Create();
        }

        public List<StudentReg> GetAllStudents()
        {
            return _dataSource.StudentRegs.OrderBy(s => s.StudentName).ToList();
        }
        

        public List<StudentReg> GetStudentsByFilter(string name, string stuclass, string stuSection, int startIndex, int count, string sorting)
        {
            var user = new GetAppUserInfo();
            var crid = user.CurrentUserId;

            IEnumerable<TenureTime> tenure = _dataSource.TenureTimes.Where(s => s.SchoolProfileId == crid);
            TenureYear tenureYear = 0;
            foreach (var t in tenure)
            {
                if (t.TenureEndDate > DateTime.Now && t.TenureStartDate < DateTime.Now)
                {
                    tenureYear = t.TenureYearName;
                    break;
                }
            }
            if (!_dataSource.StudentRegs.Any(s => s.SchoolProfileId == crid && s.TenureYear == tenureYear && s.IsActive == true))
            {
                return new List<StudentReg>();
            }

            IEnumerable<StudentReg> query = _dataSource.StudentRegs.Where(s => s.SchoolProfileId == crid && s.TenureYear == tenureYear && s.IsActive == true);

            //Filters
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.StudentName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (Convert.ToInt32(stuclass) > 0)
            {
                var stuclasss = EnumUtil.ParseEnum<SClass>(stuclass);
                query = query.Where(p => p.StuClass == stuclasss);
            }
            if (Convert.ToInt32(stuSection) > 0)
            {
                var stuSections = EnumUtil.ParseEnum<SSectionEnum>(stuSection);
                query = query.Where(p => p.StuSection == stuSections);
            }
            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(p => p.StudentName);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(p => p.StudentName);
            }
            else
            {
                query = query.OrderBy(p => p.StudentName); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        public int GetStudentCountByFilter(string name, string stuclass, string stuSection)
        {
            var user = new GetAppUserInfo();
            var crid = user.CurrentUserId;
            IEnumerable<TenureTime> tenure = _dataSource.TenureTimes.Where(s => s.SchoolProfileId == crid);
            TenureYear tenureYear = 0;
            foreach (var t in tenure)
            {
                if (t.TenureEndDate > DateTime.Now && t.TenureStartDate < DateTime.Now)
                {
                    tenureYear = t.TenureYearName;
                    break;
                }
            }
            if (!_dataSource.StudentRegs.Any(s => s.SchoolProfileId == crid && s.TenureYear == tenureYear && s.IsActive == true))
            {
                return 0;
            }

            IEnumerable<StudentReg> query = _dataSource.StudentRegs.Where(s => s.SchoolProfileId == crid && s.TenureYear == tenureYear && s.IsActive == true);

            //Filters
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.StudentName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (Convert.ToInt32(stuclass) > 0)
            {
                var stuclasss = EnumUtil.ParseEnum<SClass>(stuclass);
                query = query.Where(p => p.StuClass == stuclasss);
            }
            if (Convert.ToInt32(stuSection) > 0)
            {
                var stuSections = EnumUtil.ParseEnum<SSectionEnum>(stuSection);
                query = query.Where(p => p.StuSection == stuSections);
            }
            return query.Count();
        }


        //Here the code for Attendance View

        public List<StuAttendance> GetAttendanceByFilter(string name, string tenureYear, string stuclass, string stuSection,string selecteDate, int startIndex, int count, string sorting)
        {
            var user = new GetAppUserInfo();
            var crid = user.CurrentUserId;
            IEnumerable<StuAttendance> query = _dataSource.StuAttendances.Include(i => i.StudentReg).Where(s => s.SchoolProfileId == crid);

            //Filters
            if (!string.IsNullOrEmpty(selecteDate))
            {
                var date = DateTimeConvert.GetDate(selecteDate);
                query = query.Where(p => p.PresentDate == date);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.StudentReg.StudentName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (Convert.ToInt32(tenureYear) > 0)
            {
                var TenureTimes = EnumUtil.ParseEnum<TenureYear>(tenureYear);
                query = query.Where(p => p.StudentReg.TenureYear == TenureTimes);
            }
            if (Convert.ToInt32(stuclass) > 0)
            {
                var stuclasss = EnumUtil.ParseEnum<SClass>(stuclass);
                query = query.Where(p => p.StudentReg.StuClass == stuclasss);
            }
            if (Convert.ToInt32(stuSection) > 0)
            {
                var stuSections = EnumUtil.ParseEnum<SSectionEnum>(stuSection);
                query = query.Where(p => p.StudentReg.StuSection == stuSections);
            }
            //Sorting
            //This ugly code is used just for demonstration.
            //Normally, Incoming sorting text can be directly appended to an SQL query.
            if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
            {
                query = query.OrderBy(p => p.StudentReg.StudentName);
            }
            else if (sorting.Equals("Name DESC"))
            {
                query = query.OrderByDescending(p => p.StudentReg.StudentName);
            }
            else
            {
                query = query.OrderBy(p => p.StudentReg.StudentName); //Default!
            }

            return count > 0
                       ? query.Skip(startIndex).Take(count).ToList() //Paging
                       : query.ToList(); //No paging
        }

        public int GetAttendanceCountByFilter(string name, string tenureYear, string stuclass, string stuSection , string selecteDate)
        {
            var user = new GetAppUserInfo();
            var crid = user.CurrentUserId;
            IEnumerable<StuAttendance> query = _dataSource.StuAttendances.Include(i => i.StudentReg).Where(s => s.SchoolProfileId == crid);

            //Filters
            if (!string.IsNullOrEmpty(selecteDate))
            {
                var date = DateTimeConvert.GetDate(selecteDate);
                query = query.Where(p => p.PresentDate == date);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.StudentReg.StudentName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            if (Convert.ToInt32(tenureYear) > 0)
            {
                var TenureTimes = EnumUtil.ParseEnum<TenureYear>(tenureYear);
                query = query.Where(p => p.StudentReg.TenureYear == TenureTimes);
            }
            if (Convert.ToInt32(stuclass) > 0)
            {
                var stuclasss = EnumUtil.ParseEnum<SClass>(stuclass);
                query = query.Where(p => p.StudentReg.StuClass == stuclasss);
            }
            if (Convert.ToInt32(stuSection) > 0)
            {
                var stuSections = EnumUtil.ParseEnum<SSectionEnum>(stuSection);
                query = query.Where(p => p.StudentReg.StuSection == stuSections);
            }
            return query.Count();
        }

        


        //Here the code for Attendance View

        //public List<StuAttendance> GetAttendanceByFilter(string name, string stuclass, string stuSection, string selecteDate, int startIndex, int count, string sorting)
        //{
        //    var user = new GetAppUserInfo();
        //    var crid = user.CurrentUserId;

        //    IEnumerable<TenureTime> tenure = _dataSource.TenureTimes.Where(s => s.SchoolProfileId == crid);
        //    TenureYear tenureYear = 0;
        //    foreach (var t in tenure)
        //    {
        //        if (t.TenureEndDate > DateTime.Now && t.TenureStartDate < DateTime.Now)
        //        {
        //            tenureYear = t.TenureYearName;
        //            break;
        //        }
        //    }

        //    if (!_dataSource.StuAttendances.Include(i => i.StudentReg).Any(s => s.SchoolProfileId == crid && s.StudentReg.TenureYear == tenureYear))
        //    {
        //        return new List<StuAttendance>();
        //    }

        //    IEnumerable<StuAttendance> query = _dataSource.StuAttendances.Include(i => i.StudentReg).Where(s => s.SchoolProfileId == crid && s.StudentReg.TenureYear == tenureYear);

        //    //Filters
        //    if (!string.IsNullOrEmpty(selecteDate))
        //    {
        //        var date = DateTimeConvert.GetDate(selecteDate);
        //        query = query.Where(p => p.PresentDate == date);
        //    }
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        query = query.Where(p => p.StudentReg.StudentName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
        //    }
        //    if (Convert.ToInt32(stuclass) > 0)
        //    {
        //        var stuclasss = EnumUtil.ParseEnum<SClass>(stuclass);
        //        query = query.Where(p => p.StudentReg.StuClass == stuclasss);
        //    }
        //    if (Convert.ToInt32(stuSection) > 0)
        //    {
        //        var stuSections = EnumUtil.ParseEnum<SSectionEnum>(stuSection);
        //        query = query.Where(p => p.StudentReg.StuSection == stuSections);
        //    }
        //    //Sorting
        //    //This ugly code is used just for demonstration.
        //    //Normally, Incoming sorting text can be directly appended to an SQL query.
        //    if (string.IsNullOrEmpty(sorting) || sorting.Equals("Name ASC"))
        //    {
        //        query = query.OrderBy(p => p.StudentReg.StudentName);
        //    }
        //    else if (sorting.Equals("Name DESC"))
        //    {
        //        query = query.OrderByDescending(p => p.StudentReg.StudentName);
        //    }
        //    else
        //    {
        //        query = query.OrderBy(p => p.StudentReg.StudentName); //Default!
        //    }

        //    return count > 0
        //               ? query.Skip(startIndex).Take(count).ToList() //Paging
        //               : query.ToList(); //No paging
        //}

        //public int GetAttendanceCountByFilter(string name, string stuclass, string stuSection, string selecteDate)
        //{
        //    var user = new GetAppUserInfo();
        //    var crid = user.CurrentUserId;
        //    IEnumerable<TenureTime> tenure = _dataSource.TenureTimes.Where(s => s.SchoolProfileId == crid);
        //    TenureYear tenureYear = 0;
        //    foreach (var t in tenure)
        //    {
        //        if (t.TenureEndDate > DateTime.Now && t.TenureStartDate < DateTime.Now)
        //        {
        //            tenureYear = t.TenureYearName;
        //            break;
        //        }
        //    }

        //    if (!_dataSource.StuAttendances.Include(i => i.StudentReg).Any(s => s.SchoolProfileId == crid && s.StudentReg.TenureYear == tenureYear))
        //    {
        //        return 0;
        //    }
        //    IEnumerable<StuAttendance> query = _dataSource.StuAttendances.Include(i => i.StudentReg).Where(s => s.SchoolProfileId == crid && s.StudentReg.TenureYear == tenureYear);

        //    //Filters
        //    if (!string.IsNullOrEmpty(selecteDate))
        //    {
        //        var date = DateTimeConvert.GetDate(selecteDate);
        //        query = query.Where(p => p.PresentDate == date);
        //    }
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        query = query.Where(p => p.StudentReg.StudentName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);
        //    }

        //    if (Convert.ToInt32(stuclass) > 0)
        //    {
        //        var stuclasss = EnumUtil.ParseEnum<SClass>(stuclass);
        //        query = query.Where(p => p.StudentReg.StuClass == stuclasss);
        //    }
        //    if (Convert.ToInt32(stuSection) > 0)
        //    {
        //        var stuSections = EnumUtil.ParseEnum<SSectionEnum>(stuSection);
        //        query = query.Where(p => p.StudentReg.StuSection == stuSections);
        //    }
        //    return query.Count();
        //}

    }
}