using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.SchoolEN
{
    public class SAddress
    {
        public int Id { get; set; }

        
        [Display(Name = "Address Line 1")]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string AddL1 { get; set; }

        [Display(Name = "Address Line 2")]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string AddL2 { get; set; }
        
        [Display(Name = "Pin Code")]
        public int Pin { get; set; }

        [Required]
        public City City { get; set; }

        [Required]
        public State state { get; set; }
        public string SchoolProfileId { get; set; }
    }
}