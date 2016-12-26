using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMApp.Web.LIB.Models.StudentEN;
using SMApp.Web.LIB.ViewModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace SMApp.Web.LIB.ViewModels.StudentVM
{
    public class StudentVM
    {

        public string RegId { get; set; }
        public string StudentId { get; set; }
        public string SchoolId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Previous Education")]
        public string PreEduInfo { get; set; }

        [Required]
        [Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }

        [Display(Name = "Guardian Mobile")]
        public string GuardianMobile { get; set; }

        [Display(Name = "Guardian Email")]
        public string GuardialEmail { get; set; }

        [Display(Name = "Qualification")]
        public Qualification GuardianQualification { get; set; }

        [Display(Name = "Occupation")]
        public string GuardianOcupation { get; set; }

        [Display(Name = "Relation")]
        public Relation RelationWithGuardian { get; set; }
        public string MotherName { get; set; }
        public string MotherQualification { get; set; }
        public string MotherOcupation { get; set; }

        //Address
        [Required]
        [Display(Name = "Address Line 1")]
        public string AddL1 { get; set; }


        //[StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Address Line 2")]
        public string AddL2 { get; set; }

        [Required]
        [Display(Name = "Pin Code")]
        public int Pin { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        public State State { get; set; }

        //
        public string StudentProfileId { get; set; }


        [Required]
        [Display(Name = "Session")]
        public TenureYear TenureYear { get; set; }

        [Required]
        [Display(Name = "Class")]
        public SClass Stuclass { get; set; }

        [Required]
        [Display(Name = "Section")]
        public SSectionEnum StuSection { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Admission Date")]
        public string AdmissioinDate { get; set; }

        public StudentClub ClubName { get; set; }
    }
}