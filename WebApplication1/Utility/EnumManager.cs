using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EnumManager<TEnum>
    {
        
        public static List<KeyValuePair<string, string>> GetEnumList(bool isAll)
        {
            var result = EnumManager<TEnum>.GetDictionary().ToList();
            if (isAll) result.Insert(0, new KeyValuePair<string, string>(" ", "所有"));
            return result;
        }
        public static Dictionary<string, string> GetDictionary()
        {
            var enumType = typeof(TEnum);
            var dic = new Dictionary<string, string>();
            dic = GetEnumDic(enumType);
            return dic.ToDictionary(str => GetEnumValue(str.Key), str => str.Value);
        }


        /// <summary>
        /// 查询枚举name(string)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumValue(int value)
        {
            return Enum.GetName(typeof(TEnum), value) ?? string.Empty;           
        }
        /// <summary>
        /// 枚举值不能超过30,查询枚举value(int)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetEnumValue(string name)
        {
            


            string retstr = "";
            for (int i = 0; i < 100; i++)
            {
                if (name == Enum.GetName(typeof(TEnum), i))
                {
                    retstr = i.ToString();
                }
            }
            return retstr;
        }
        /// <summary>
        /// 查询枚举描述(string)
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetEnumDesc(int i)
        {
            string retstr = "";
            Type enumType = typeof(TEnum);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic = GetEnumDic(enumType);
            foreach (KeyValuePair<string, string> entry in dic)
            {
                if (GetEnumValue(entry.Key) == i.ToString())
                {
                    retstr = entry.Value;
                }
            }
            return retstr;
        }

        ///<summary>
        /// 返回 Dic<枚举项，描述>
        ///</summary>
        ///<param name="enumType"></param>
        ///<returns>Dic<枚举项，描述></returns>
        public static Dictionary<string, string> GetEnumDic(Type enumType)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            FieldInfo[] fieldinfos = enumType.GetFields();
            foreach (FieldInfo field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {                   
                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    dic.Add(field.Name, ((DescriptionAttribute)objs[0]).Description);
                }
            }

            return dic;
        }

    }
}
