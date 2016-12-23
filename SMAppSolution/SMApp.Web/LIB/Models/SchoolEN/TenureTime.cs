using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.SchoolEN
{
    public class TenureTime
    {
        public int Id { get; set; }

        [Required]
        public int SchoolProfileId { get; set; }

        public TenureYear TenureYearName { get; set; }

        public DateTime? TenureStartDate { get; set; }

        public DateTime? TenureEndDate { get; set; }

        public bool IsSet { get; set; }
    }
}