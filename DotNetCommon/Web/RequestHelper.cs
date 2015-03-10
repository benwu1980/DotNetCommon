using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DotNetCommon.Helper;

namespace DotNetCommon.Web
{
    public static class RequestHelper
    {
        /// <summary>
        /// 获取web地址QueryString或post过来的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public static T Get<T>(string queryName, T defaultValue = default(T))
        {
            var obj = HttpContext.Current.Request[queryName];
            return obj == null ? defaultValue : ObjectHelper.ChangeType<T>(obj, defaultValue);
        }

        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
    }
}
