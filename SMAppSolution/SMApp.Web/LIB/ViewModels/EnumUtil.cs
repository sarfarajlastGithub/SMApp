using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMApp.Web.LIB.ViewModels
{
    public class EnumUtil
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}