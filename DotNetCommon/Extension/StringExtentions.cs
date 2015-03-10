using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace DotNetCommon.Extension
{
    /// <summary>
    /// 字符串的扩展方法
    /// </summary>
    public static class StringExtentions
    {
        private static readonly Regex WebUrlExpression = new Regex(
           @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex EmailExpression =
            new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$",
                      RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex StripHTMLExpression = new Regex("<\\S[^><]*>",
                                                                      RegexOptions.IgnoreCase | RegexOptions.Singleline |
                                                                      RegexOptions.Multiline |
                                                                      RegexOptions.CultureInvariant |
                                                                      RegexOptions.Compiled);

        private static readonly Regex NumberExpression = new Regex(@"^\d+(.\d{1,2})?$");

        private static readonly Regex DigitExpression = new Regex(@"^\d+$");

        private static readonly Regex ChinaMobileExpression = new Regex(@"^(13[0-9]|15[0-9]|18[0-9])\d{8}$");


        public static string FormatWith(this string source, params object[] args)
        {
            return string.Format(source, args);
        }

        public static bool IsWebUrl(this string target)
        {
            return !string.IsNullOrEmpty(target) && WebUrlExpression.IsMatch(target);
        }

        /// <summary>
        /// 检查是否是数字，可以包含两位小数
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsNumber(string target)
        {
            return NumberExpression.IsMatch(target);
        }

        /// <summary>
        /// 检查目标字符串中所包含的字符全是数字
        /// </summary>
        /// <param name="target">目标字符串</param>
        /// <returns></returns>
        public static bool IsDigitStr(this string target)
        {
            return DigitExpression.IsMatch(target);
        }

        /// <summary>
        /// 是否是邮箱地址
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsEmail(this string target)
        {
            return !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target);
        }

        /// <summary>
        /// 转为非null的字符串
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToNotNullString(this string target)
        {
            return (target ?? string.Empty).Trim();
        }

        public static string WrapAt(this string target, int index)
        {
            const int dotCount = 3;
            return (target.Length <= index)
                       ? target
                       : string.Concat(target.Substring(0, index - dotCount), new string('.', dotCount));
        }

        public static string StripHtml(this string target)
        {
            return StripHTMLExpression.Replace(target, string.Empty);
        }

        public static Guid ToGuid(this string target)
        {
            Guid result = Guid.Empty;

            if ((!string.IsNullOrEmpty(target)) && (target.Trim().Length == 36))
            {
                try
                {
                    result = new Guid(target);
                }
                catch (FormatException)
                {
                }
            }
            return result;
        }


        public static DateTime ToDateTime(this string target)
        {
            DateTime result;
            bool sucess = DateTime.TryParse(target, out result);
            return !sucess ? DateTime.MinValue : result;
        }

        public static DateTime? ToNullableDateTime(this string target)
        {
            DateTime? result = null;
            if (!string.IsNullOrEmpty(target))
            {
                DateTime temp;
                bool sucess = DateTime.TryParse(target, out temp);
                if (sucess)
                {
                    result = temp;
                }
            }

            return result;
        }


        public static bool? ToNullBool(this string target)
        {
            bool? result = null;
            if (!string.IsNullOrEmpty(target))
            {
                bool temp;
                bool sucess = bool.TryParse(target, out temp);
                if (sucess)
                {
                    result = temp;
                }
            }
            return result;
        }


        /// <summary>
        /// 批量替换
        /// </summary>
        /// <param name="target">要替换的字符串</param>
        /// <param name="oldValues">要替换的旧值</param>
        /// <param name="newValue">要替换的新值</param>
        /// <returns></returns>
        public static string Replace(this string target, ICollection<string> oldValues, string newValue)
        {
            oldValues.ForEach(oldValue => target = target.Replace(oldValue, newValue));
            return target;
        }

        public static int[] ToIntArray(this string target, string split)
        {
            if (string.IsNullOrEmpty(target))
            {
                return new int[0];
            }

            string[] stringArray = target.ToStringArray(split);
            return stringArray.Select(s => s.ToInt()).ToArray();
        }

        public static int[] ToIntArray(this string target)
        {
            return target.ToIntArray(",");
        }

        public static string[] ToStringArray(this string target, string split)
        {
            if (string.IsNullOrEmpty(target))
            {
                return new string[0];
            }

            return target.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] ToStringArray(this string target)
        {
            return target.ToStringArray(",");
        }

        /// <summary>
        /// 将指定的整数数组串联，用linker所指定的字符串分隔
        /// </summary>
        /// <param name="src">整数数组</param>
        /// <param name="linker">分隔字符串</param>
        /// <returns>System.String</returns>
        public static string ToLinkString(this int[] src, string linker)
        {
            return src.Select(s => s.ToString()).ToArray().ToLinkString(linker);
        }

        public static string ToLinkString(this int[] src)
        {
            return src.ToLinkString(",");
        }

        /// <summary>
        /// 将指定的字符串数据组串联，用linker所指定的字符串分隔
        /// </summary>
        /// <param name="src">字符串数据组</param>
        /// <param name="linker">分隔字符串</param>
        /// <returns>System.String</returns>
        public static string ToLinkString(this string[] src, string linker)
        {
            if (src.Length > 0)
            {
                return string.Join(linker, src);
            }

            return string.Empty;
        }

        public static string ToLinkString(this string[] src)
        {
            return src.ToLinkString(",");
        }

        public static string ToJavaScriptArrayString(this string[] src)
        {
            return src.Select(c => string.Format("'{0}'", c)).ToArray().ToLinkString();
        }

        public static string ToSqlLinkString(this string[] src)
        {
            return src.Select(s => "'{0}'".FormatWith(s)).ToArray().ToLinkString(",");
        }

        public static bool IsNotEmpty(this string src)
        {
            return !string.IsNullOrEmpty(src) && src.Trim().Length > 0;
        }

        public static bool IsNullOrEmpty(this string target)
        {
            return target == null || target.Trim() == string.Empty;
        }

        /// <summary>
        /// 获得手机号码运行商名称
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static string ToMobileOperator(this string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
                return "错号";
            }

            if (mobileNumber.StartsWith("0"))
            {
                mobileNumber = mobileNumber.Remove(0, 1);
            }

            if (!mobileNumber.IsMobileNumber())
            {
                return "其他";
            }

            var chinaMobile = new[] { 134, 135, 136, 137, 138, 139, 150, 151, 152, 157, 158, 159, 187, 188 }; /*移动*/
            var unionMobile = new[] { 130, 131, 132, 155, 156, 185, 186 }; /*联通*/
            var telMobile = new[] { 133, 153, 180, 189 }; /*电信*/

            int indicator = mobileNumber.Substring(0, 3).ToInt();
            if (chinaMobile.Contains(indicator))
            {
                return "移动";
            }
            if (unionMobile.Contains(indicator))
            {
                return "联通";
            }
            if (telMobile.Contains(indicator))
            {
                return "电信";
            }

            return "其他";
        }

        /// <summary>
        /// 检查字符串是否为手机号码
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static bool IsMobileNumber(this string mobileNumber)
        {
            return !string.IsNullOrEmpty(mobileNumber) && ChinaMobileExpression.IsMatch(mobileNumber);
        }


        public static decimal ToDecimal(this string target)
        {
            decimal result = 0;
            if (decimal.TryParse(target, out result))
            {
                return result;
            }
            return 0;
        }
        /// <summary>
        /// 转化为双精度浮点数
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double ToDouble(this string target)
        {
            double result = 0;
            if (double.TryParse(target, out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// 转化为长整型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static long ToBigInt(this string target)
        {
            long result = 0;
            if (long.TryParse(target, out result))
            {
                return result;
            }
            return 0;
        }

        public static decimal? ToNullableDecimal(this string target)
        {
            decimal? result = null;
            if (!string.IsNullOrEmpty(target))
            {
                decimal temp;
                bool sucess = decimal.TryParse(target, out temp);
                if (sucess)
                {
                    result = temp;
                }
            }

            return result;
        }

        public static int? ToNullableInt(this string target)
        {
            int? result = null;
            if (!string.IsNullOrEmpty(target))
            {
                int temp;
                bool sucess = int.TryParse(target, out temp);
                if (sucess)
                {
                    result = temp;
                }
            }

            return result;
        }

        public static int ToInt(this string target)
        {
            int result;
            bool sucess = int.TryParse(target, out result);
            return !sucess ? 0 : result;
        }

        /// <summary>
        /// 转化一个整数，如果不成功，则返回-1;如果转化结果小于-1,则返回-1
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int ToUInt(this string target)
        {
            int result = -1;
            bool sucess = int.TryParse(target, out result);
            result = !sucess ? -1 : result;
            return Math.Max(result, -1);
        }

    }
}
