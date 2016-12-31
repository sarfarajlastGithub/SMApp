using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.Models.SchoolEN;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.StudentEN
{
    public class StudentReg
    {
        public int Id { get; set; }

        public string RegId { get; set; }

        public string RolNo { get; set; }

        public ApplicationUser SchoolProfile { get; set; }

        [Required]
        public string SchoolProfileId { get; set; }
        
        public string StudentProfileId { get; set; }

        public string StudentName { get; set; }

        public SClass StuClass { get; set; }

        public SSectionEnum StuSection { get; set; }

        public TenureYear TenureYear { get; set; }

        //public FeeAccount FeeAccount { get; set; }

        //public int FeeAccountId { get; set; }

        public bool IsActive { get; set; }

        public DateTime? AdmissioinDate { get; set; }

        public StudentClub ClubName { get; set; }

        public virtual IEnumerable<StuAttendance> StuAttendances { get; set; }
    }
}