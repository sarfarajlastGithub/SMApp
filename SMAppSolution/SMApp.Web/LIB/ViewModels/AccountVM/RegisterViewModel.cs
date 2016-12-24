using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMApp.Web.LIB.Models.SchoolEN;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.ViewModels.AccountVM
{
    public class RegisterViewModel
    {
        public int Id { get; set; }  
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Required]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        
        [Display(Name = "Stay Facility")]
        public SchoolFType SchoolFType { get; set; }

        
        [Display(Name = "School Type")]
        public SchoolGType SchoolGType { get; set; }

        [Required]
        [Display(Name = "Total Students")]
        [StringLength(6, ErrorMessage = "Digit has to be less then 7")]
        public string TotalStudent { get; set; }

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
        
        public string PhoneNumber { get; set; }
        
        [Required]
        public City City { get; set; }

        [Required]
        public State State { get; set; }


    }
    

}