using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.StudentFeeEN
{
    public class ConseOrFineAmountClass
    {
        public int Id { get; set; }

        public string SchoolId { get; set; }

        public FeeConsessionOrFineCategory FeeConsessionCategory { get; set; }

        //On which Category this concession will apply
        public FeeMainCategory OnFeeMainCategory { get; set; }

        //This Amount Will be slashed on Amount of MainFeeAmountClass
        public double OnAmount { get; set; }

        public DaOrPa DaOrPa { get; set; }

        public FeeOrConcession FeeOrConcession { get; set; }

        public DateTime? CreateDateTime { get; set; }

    }
}