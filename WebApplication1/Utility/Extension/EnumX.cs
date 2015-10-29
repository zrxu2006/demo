using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extension
{
    public static class EnumX
    {
        public static string GetDescription<TEnum>(this TEnum Enum) where TEnum:struct
        {
            return GetDescription<TEnum>(Enum, string.Empty);
        }

        private static string GetDescription<TEnum>(TEnum e,string defaultDesc)
        {
            var em = e.ToString();
            FieldInfo fieldInfo = e.GetType().GetField(em);
            if (fieldInfo == null) return defaultDesc;
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length < 1) return defaultDesc;
            return attributes[0].Description;
        }
    }
}
