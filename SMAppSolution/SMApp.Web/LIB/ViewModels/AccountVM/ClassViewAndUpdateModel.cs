using System.Collections.Generic;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.ViewModels.AccountVM
{
    public class ClassViewAndUpdateModel
    {
        public bool A { get; set; }

        public bool B { get; set; }

        public bool C { get; set; }

        public bool D { get; set; }

        public bool E { get; set; }

        public bool F { get; set; }

        public int SchoolProfileId { get; set; }

        public SClass ClassName { get; set; }

        public SSectionEnum SectionName { get; set; }

        public List<SClassVM> SClassList { get; set; }


        // this Section for Tenure 

        public TenureYear TenureYearName { get; set; }

        public IEnumerable<TenureTimeVM> TenureYearList { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}