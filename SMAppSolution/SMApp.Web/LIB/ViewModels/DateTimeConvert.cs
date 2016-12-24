using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMApp.Web.LIB.ViewModels
{
    public class DateTimeConvert
    {

        public static DateTime? GetDate(string date)
        {
            DateTime? datee = Convert.ToDateTime(date);
            return datee;
        }

        public static string GetString(DateTime? date)
        {
            string dates = date.HasValue ? date.Value.ToString("dd MMMM, yyyy") : string.Empty;
            return dates;
        }

    }
}