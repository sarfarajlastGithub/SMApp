using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMApp.Web.LIB.Models.StudentFeeEN
{
    public class ScheduleFee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public DateTime? NotifyDateTime { get; set; }
        public IEnumerable<MainFeeAmountClass> MainFeeAmountClasses { get; set; }
        public IEnumerable<ConseOrFineAmountClass> ConseOrFineAmountClasses { get; set; }
    }
}