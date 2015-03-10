using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using System.Net.Security;
using System.Text.RegularExpressions;

namespace DotNetCommon.Web
{
    /// <summary>
    /// 网页地址访问
    /// </summary>
    public class HtmlHelper
    {
        /// <summary>
        /// 清除指定文本的html标识
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static string ClearHTMLTag(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
                return string.Empty;

            const string pattern = @"<(.|\n)*?>";
            return Regex.Replace(htmlText, pattern, string.Empty).Trim();
        }

        /// <summary>
        /// 过滤html标识
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static bool HasHTML(string htmlText)
        {
            if (string.IsNullOrEmpty(htmlText))
                return false;

            const string pattern = @"<(.|\n)*?>";
            return Regex.IsMatch(htmlText, pattern);
        }

        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string htmlText)
        {
            htmlText = Regex.Replace(htmlText, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return htmlText;
        }
    }
}
