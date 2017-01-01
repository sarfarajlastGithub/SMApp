using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.BL.Account;
using SMApp.Web.LIB.Models.SchoolEN;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.StudentFeeEN
{
    public class MainFeeAmountClass
    {
        public int Id { get; set; }
        public SClass SClass { get; set; }
        public FeeMainCategory FeeMainCategory { get; set; }
        public double Amount { get; set; }
        public string SchoolId { get; set; }
        public DateTime? CreateDateTime { get; set; }
    }
}