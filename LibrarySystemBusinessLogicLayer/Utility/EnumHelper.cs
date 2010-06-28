using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace LibrarySystemBusinessLogicLayer.Utility
{
    public class EnumHelper<T>
    {
        public static string GetEnumDesc(T enumType)
        {
            string defDesc = string.Empty;
            FieldInfo finfo = enumType.GetType().GetField(enumType.ToString());

            if (finfo != null)
            {
                object[] attributes = finfo.GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (attributes != null && attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return defDesc;
        }

        public static T FromDescription(string desc)
        {
            Type t = typeof(T);

            foreach (FieldInfo finfo in t.GetFields())
            {
                object[] attributes = finfo.GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (attributes != null && attributes.Length > 0)
                {
                    foreach (DescriptionAttribute attr in attributes)
                    {
                        if (attr.Description.Equals(desc))
                            return (T)finfo.GetValue(null);
                    }
                }
            }

            return default(T);
        }
    }
}
