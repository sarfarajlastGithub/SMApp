using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.Models.StudentEN;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.StudentFeeEN
{
    public class StudentWiseFineOrConsession
    {
        public int Id { get; set; }
        public virtual StudentReg StudentReg { get; set; }
        public string StudentId { get; set; }
        public double Amount { get; set; }
        public DaOrPa DaOrPa { get; set; }
        public FeeMainCategory FeeMainCategory { get; set; }
        public string SchoolId { get; set; }
    }
}