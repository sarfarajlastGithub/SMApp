using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.ViewModels.AccountVM
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        
        [Required]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [Required]
        [Display(Name = "Stay Facility")]
        public SchoolFType SchoolFType { get; set; }

        [Required]
        [Display(Name = "School Type")]
        public SchoolGType SchoolGType { get; set; }

        [Required]
        [Display(Name = "Total Students")]
        [StringLength(6, ErrorMessage = "Digit has to be less then 7")]
        public string TotalStudent { get; set; }

        [Required]
        [Display(Name = "School Phone Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string SchoolPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Contact Name")]
        public string CpName { get; set; }

        [Required]
        [Display(Name = "Contact Mobile")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string CpPhone { get; set; }

        [Required]
        public SBoard Board { get; set; }

        [Display(Name = "Month of Financial Year")]
        public string AnnulDateOfExam { get; set; }

        [Required]
        public Medium Medium { get; set; }

        [Display(Name = "Established Date")]
        public string EstablishedDate { get; set; }

        public IEnumerable<SClassVM> ClassAndSections { get; set; }
        public SClassVM ClassAndSection { get; set; }

        public SClass SClassEnum { get; set; }
        public SSectionEnum SSectionEnum { get; set; }
        public string RegistarDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string AddL1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string AddL2 { get; set; }

        [Required]
        [Display(Name = "Pin Code")]
        public int Pin { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        public State State { get; set; }

    }
}