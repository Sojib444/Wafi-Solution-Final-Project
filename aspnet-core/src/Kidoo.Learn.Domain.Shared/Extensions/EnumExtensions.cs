using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Kidoo.Learn.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            if (value == null || Convert.ToInt16(value) == 0) return "Not Specified";

            var enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            var member = enumType.GetMember(enumValue)[0];
            var attr = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (!attr.Any()) return enumValue;

            var outString = ((DisplayAttribute)attr[0]).Name;
            if (((DisplayAttribute)attr[0]).ResourceType != null) outString = ((DisplayAttribute)attr[0]).GetName();
            return outString;
        }
    }
}
