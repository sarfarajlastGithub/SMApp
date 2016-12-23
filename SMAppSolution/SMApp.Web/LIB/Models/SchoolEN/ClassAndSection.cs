using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.SchoolEN
{
    public class ClassAndSection
    {
        public int Id { get; set; }

        [Required]
        public string SchoolProfileId { get; set; }

        public SClass SClass { get; set; }

        public SSectionEnum SSection { get; set; }

        public int NoOfStudentEachSection { get; set; }
    }
}