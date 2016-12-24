using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.ViewModels.AccountVM
{
    public class TenureTimeVM
    {
        public int Id { get; set; }

        [Required]
        public int SchoolProfileId { get; set; }

        public TenureYear TenureYearName { get; set; }

        public string TenureStartDate { get; set; }

        public string TenureEndDate { get; set; }

        public bool IsSet { get; set; }
    }
}