using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HoneymoonShop { 
    public static class Helpers
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example>string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;</example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Any()) ? (T)attributes.ElementAt(0) : null;
        }

        public static string GetQueryString(this object obj)
        {
            Type t = obj.GetType();
            StringBuilder sb = new StringBuilder();
            foreach (var prop in t.GetProperties())
            {
                var value = prop.GetValue(obj);
                if (value != null)
                {
                    if (value is IList)
                    {
                        var list = value as IList;
                        foreach (var listItem in list)
                        {
                            sb.Append(prop.Name)
                                .Append("=")
                                .Append(System.Net.WebUtility.HtmlEncode(listItem.ToString()))
                                .Append("&");
                        }
                    }
                    else
                        sb.Append(prop.Name)
                            .Append("=")
                            .Append(System.Net.WebUtility.HtmlEncode(value.ToString()))
                            .Append("&");
                }
            }
            return sb.ToString();
        }
    }
}
