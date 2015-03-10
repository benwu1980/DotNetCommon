using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace DotNetCommon.Web
{
    /// <summary>
    /// 用 ASP.NET Cache作为缓存
    /// </summary>
    public static class WebCache
    {
        /// <summary>
        /// 增加一个缓存
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpiration">缓存时间</param>
        public static object Get(string key)
        {
            return HttpContext.Current.Cache.Get(key);
        }

        public static void Add(string key, object value, TimeSpan slidingExpiration)
        {
            HttpContext.Current.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration);
        }


    }
}
