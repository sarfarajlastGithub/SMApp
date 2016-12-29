using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.ViewModels.StudentVM
{
    public class StuAttendanceVM
    {
        public string StudentRegId { get; set; }
        public DateTime? PresentDate { get; set; }
        public bool IsPresent { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "Session / Tenure Year")]
        public TenureYear TenureYear { get; set; }

        [Required]
        [Display(Name = "Class")]
        public SClass Stuclass { get; set; }

        [Required]
        [Display(Name = "Section")]
        public SSectionEnum StuSection { get; set; }

        [Display(Name = "Do you want to present / absent Selected Student")]
        public PresentStatus PresentStatus { get; set; }

        
        [Display(Name = "Select Date for which you want to present")]
        public DateTime SelecteDateTime { get; set; }
    }
}