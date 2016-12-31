using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.ViewModels.StudentVM
{
    public class StudentSearchVM
    {
        public string StudentRegId { get; set; }

        public string StudentName { get; set; }

        public string RolNo { get; set; }

        public SClass StuClass { get; set; }

        public SSectionEnum StuSection { get; set; }

        public TenureYear TenureName { get; set; }

        public string StuClassString { get; set; }

        public string StuSectionString { get; set; }

        public string TenureNameString { get; set; }
        
        public bool IsActive { get; set; }
    }
}