using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.Extension;

namespace Utility
{
    class EnumDescriptionInfo
    {
        /// <summary>
        /// 枚举常数值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 枚举常数名称
        /// </summary>
        public string EnumName { get; set; }
        /// <summary>
        /// 枚举描述
        /// </summary>
        public string Description { get; set; }
    }

    
    public static class EnumHelper<TEnum> where TEnum:struct
    {
        private static TEnum GetEnumByName(string enumName)
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         where e.ToString() == enumName
                         select e;
            return values.FirstOrDefault();
        }

        /// <summary>
        /// 获取枚举信息
        /// </summary>
        /// <returns></returns>
        private static List<EnumDescriptionInfo> GetEnumDescriptionInfo()
        {
            //var values = from TEnum e in Enum.GetValues(typeof(TEnum))
            //             select new EnumDescriptionInfo
            //             {
            //                 Value = e.GetHashCode(),
            //                 EnumName = e.ToString(),
            //                 Description = e.GetDescription()
            //             };

            //return values.ToList();

            Type t = typeof(TEnum);

            var values = from FieldInfo field in t.GetFields()
                         where field.FieldType.IsEnum
                         select new EnumDescriptionInfo
                         {
                             Value = field.GetHashCode(),
                             EnumName = field.Name,
                             Description = GetEnumByName(field.Name).GetDescription()
                         };

            return values.ToList();
        }

        /// <summary>
        /// 获取枚举类型所有枚举值，描述信息
        /// </summary>
        /// <returns></returns>
        public static List<KeyValuePair<int, string>> GetValueAndDescList()
        {
            var values = from enumInfo in GetEnumDescriptionInfo()
                         select new KeyValuePair<int, string>(enumInfo.Value, enumInfo.Description);

            return values.ToList();          
        }

        /// <summary>
        /// 获取枚举类型所有枚举名称，描述信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetNameAndDescList()
        {
            var values = from enumInfo in GetEnumDescriptionInfo()
                         select new KeyValuePair<string, string>(enumInfo.EnumName, enumInfo.Description);

            return values.ToDictionary(key => key.Key, value => value.Value); 
        }

        /// <summary>
        /// 获取枚举类型所有枚举名称，枚举值
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> GetNameAndValueList()
        {
            var values = from enumInfo in GetEnumDescriptionInfo()
                         select new KeyValuePair<string, int>(enumInfo.EnumName, enumInfo.Value);

            return values.ToDictionary(key => key.Key, value => value.Value);
        }


        public static string GetEnumName(int value)
        {
            return Enum.GetName(typeof(TEnum), value) ?? string.Empty;
        }

        /// <summary>
        /// 根据枚举名称获取枚举
        /// </summary>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static TEnum? Parse(string enumName)
        {
            var enumList = from TEnum c in Enum.GetValues(typeof(TEnum))
                                where c.ToString() == enumName
                                select c;

            TEnum? enumResult = null;
            enumResult = enumList.Count() > 0 ? enumList.First() : enumResult;

            return enumResult;
        }
    }
}
