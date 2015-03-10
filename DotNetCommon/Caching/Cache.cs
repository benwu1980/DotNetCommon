using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCommon.Common;

namespace DotNetCommon.Caching
{
    /// <summary>
    /// 缓存管理类
    /// </summary>
    public static class Cache
    {
        public static int Count
        {
            get
            {
                return InternalCache.Count;
            }
        }

        private static ICache InternalCache
        {
            get
            {
                return new EntLibCache("DefaultCacheManager");
            }
        }

        public static void Clear()
        {
            InternalCache.Clear();
        }

        public static bool Contains(string key)
        {
            return InternalCache.Contains(key);
        }

        public static T Get<T>(string key)
        {
            return InternalCache.Get<T>(key);
        }

        public static bool TryGet<T>(string key, out T value)
        {
            return InternalCache.TryGet(key, out value);
        }

        public static void Set<T>(string key, T value)
        {
            InternalCache.Set(key, value);
        }

        public static void Set<T>(string key, T value, DateTime absoluteExpiration)
        {
            InternalCache.Set(key, value, absoluteExpiration);
        }

        public static void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            InternalCache.Set(key, value, slidingExpiration);
        }

        public static void Remove(string key)
        {
            InternalCache.Remove(key);
        }
    }
}
