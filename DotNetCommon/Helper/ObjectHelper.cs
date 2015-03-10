using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security;

namespace DotNetCommon.Helper
{
    /// <summary>
    /// object的实用工具
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// 将value转化为其它类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T ChangeType<T>(object value, T defaultValue = default(T))
        {
            object obj = ChangeType(value, typeof(T));
            return obj == null ? defaultValue : (T)obj;
        }

        /// <summary>
        /// 将指定的值转化为type类型的值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type type)
        {
            if (value == null && type.IsGenericType)
                return Activator.CreateInstance(type);

            if (value == null) return null;

            if (type == value.GetType()) return value;

            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid))
                return new Guid(value as string);

            if (value is string && type == typeof(Version))
                return new Version(value as string);

            if (type == typeof(bool))
            {
                if (value is bool)
                {
                    return (bool)value;
                }
                if (value == null)
                {
                    return false;
                }
                var vn = value.ToString().ToLower();
                if (vn == "on" || vn == "true" || vn == "1")
                {
                    return true;
                }
                return false;
            }

            if (!(value is IConvertible))
                return value;

            return Convert.ChangeType(value, type);
        }
    }
}
