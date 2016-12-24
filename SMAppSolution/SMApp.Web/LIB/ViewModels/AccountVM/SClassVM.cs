using System.Collections.Generic;
using SMApp.Web.LIB.ViewModels.Enums;

namespace SMApp.Web.LIB.ViewModels.AccountVM
{
    public class SClassVM
    {
        public SClass Name { get; set; }

        public IEnumerable<SSectionVM> ListOfSection { get; set; }
    }
}