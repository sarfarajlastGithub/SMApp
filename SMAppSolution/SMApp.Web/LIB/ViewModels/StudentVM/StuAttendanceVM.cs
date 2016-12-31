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

        public string RolNo { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name = "Session")]
        public TenureYear TenureYear { get; set; }

        [Required]
        [Display(Name = "Class")]
        public SClass Stuclass { get; set; }

        [Required]
        [Display(Name = "Section")]
        public SSectionEnum StuSection { get; set; }

        [Display(Name = "Present/Absent")]
        public PresentStatus PresentStatus { get; set; }

        
        [Display(Name = "Date")]
        public DateTime SelecteDateTime { get; set; }
    }
}