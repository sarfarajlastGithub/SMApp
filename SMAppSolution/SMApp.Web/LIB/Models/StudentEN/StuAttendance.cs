using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMApp.Web.LIB.Models.StudentEN
{
    public class StuAttendance
    {
        public int Id { get; set; }

        public string SchoolProfileId { get; set; }
        public virtual StudentReg StudentReg { get; set; }

        [Required]
        public string StudentRegId { get; set; }

        public string RolNo { get; set; }
        public DateTime? PresentDate { get; set; }
        public bool IsPresent { get; set; }
    }
}