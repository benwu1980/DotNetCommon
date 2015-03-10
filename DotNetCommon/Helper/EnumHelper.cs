using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper
{
    
    /// <summary>
    /// 枚举类帮助
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 根据枚举值获取其名称
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="val">枚举值</param>
        /// <returns></returns>
        public string GetValue<T>(int val)
        {
            if (Enum.IsDefined(typeof(T), val))
            {
                return ((T)Enum.Parse(typeof(T), val.ToString())).ToString();
            }
            return "";
        }

        /// <summary>
        /// 将字符串转化为一个枚举
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="item"></param>
        /// <param name="ignorecase"></param>
        /// <returns></returns>
        public static TEnum ParseEnum<TEnum>(string item, bool ignorecase = default(bool)) where TEnum : struct
        {
            TEnum tenumResult = default(TEnum);
            return Enum.TryParse<TEnum>(item, ignorecase, out tenumResult)
                ? tenumResult
                : default(TEnum);
        }
    }
}
