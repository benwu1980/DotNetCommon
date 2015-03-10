using System;
using System.Collections;
using DotNetCommon.Helper;

namespace DotNetCommon.Extension
{

    /// <summary>
    /// 字典的扩展方法
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 根据键值从字典中获取其值，如果值为null,则返回默认的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(this IDictionary dict, string key, T defaultValue)
        {
            if (!dict.Contains(key))
            {
                throw new Exception("字典不包括键值“" + key + "”。");
            }

            object result = dict[key];
            if (result == null)
                return defaultValue;

            return ObjectHelper.ChangeType<T>(result);
        
        }


        public static T Get<T>(this IDictionary dict, string key)
        {
            return Get<T>(dict, key, default(T));
        }


        public static T GetOrDefault<T>(this IDictionary dict, string key, T defaultValue)
        {
            if (!dict.Contains(key))
            {
                return defaultValue;
            }

            return Get<T>(dict, key);
        }
    }
}
