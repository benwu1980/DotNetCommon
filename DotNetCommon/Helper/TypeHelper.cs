using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DotNetCommon.Helper
{
    /// <summary>
    /// 类型的帮助方法
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// 返回的类型是否可以为 null
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableValueType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// 指定的类型是否允许null值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool TypeAllowsNullValue(Type type)
        {
            return (!type.IsValueType || IsNullableValueType(type));
        }

        /// <summary>
        /// 获取指指定类型默认的值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefaultValue(Type type)
        {
            return (TypeAllowsNullValue(type)) ? null : Activator.CreateInstance(type);
        }


        

       
    }
}
