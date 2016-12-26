using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.Models.StudentEN
{
    public class Address
    {
        public int Id { get; set; }
         public string AddL1 { get; set; }

         public string AddL2 { get; set; }

        public int Pin { get; set; }

        public City City { get; set; }

        public State State { get; set; }
        public string StudentProfileId { get; set; }
    }
}