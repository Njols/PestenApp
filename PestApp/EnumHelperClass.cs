using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp
{
    public static class EnumHelperClass
    {
        public static string EnumDisplayNameFor(Enum item)
        {
            var type = item.GetType();
            var member = type.GetMember(item.ToString());
            DisplayAttribute displayname = (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (displayname != null)
            {
                return new string(displayname.Name);
            }

            return new string(item.ToString());
        }
    }
}
