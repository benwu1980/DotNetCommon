using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace DotNetCommon.Helper
{
    public class StringHelper
    {
        public static string UrlEncode(string target)
        {
            return HttpUtility.UrlEncode(target);
        }

        public static string UrlDecode(string target)
        {
            return HttpUtility.UrlDecode(target);
        }

        public static string AttributeEncode(string target)
        {
            return HttpUtility.HtmlAttributeEncode(target);
        }

        public static string HtmlEncode(string target)
        {
            return HttpUtility.HtmlEncode(target);
        }

        public static string HtmlDecode(string target)
        {
            return HttpUtility.HtmlDecode(target);
        }

        /// <summary>
        /// 将字符串编码
        /// </summary>
        /// <param name="encodingName">编码</param>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        public static string EncodeToBase64(string encodingName, string text)
        {
            if (text == "")
            {
                return "";
            }
            byte[] bytes = Encoding.GetEncoding(encodingName).GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 将字符串解码
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="text">解码字符串</param>
        /// <returns></returns>
        public static string DecodeFromBase64(string encodingName, string text)
        {
            if (text == "")
            {
                return "";
            }
            byte[] bytes = Convert.FromBase64String(text);
            return Encoding.GetEncoding(encodingName).GetString(bytes);
        }

        /// <summary>
        /// 取得传入的汉字的首个拼音大写字母
        /// </summary>
        /// <param name="chineseStr"></param>
        /// <returns></returns>
        public static string FirstSpell(string chineseStr)
        {
            StringBuilder sb = new StringBuilder();
            int length = chineseStr.Length;
            for (int i = 0; i < length; i++)
            {
                char chineseChar = chineseStr[i];
                sb.Append(GetPinYing(chineseChar));
            }
            return sb.ToString();
        }

        private static string GetPinYing(char chineseChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(chineseChar.ToString());
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "O";
            }
            else
                return chineseChar.ToString();
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string CheckSqlString(string strText)
        {
            strText = strText.Replace("\\", "");
            strText = strText.Replace("[", "");
            strText = strText.Replace("]", "");
            strText = strText.Replace("(", "");
            strText = strText.Replace(")", "");
            strText = strText.Replace("{", "");
            strText = strText.Replace("}", "");
            strText = strText.Replace("'", "''");
            strText = strText.Replace("UNION", "");
            strText = strText.Replace("TABLE", "");
            strText = strText.Replace("WHERE", "");
            strText = strText.Replace("DROP", "");
            strText = strText.Replace("EXECUTE", "");
            strText = strText.Replace("EXEC ", "");
            strText = strText.Replace("FROM ", "");
            strText = strText.Replace("CMD ", "");
            strText = strText.Replace(";", "");
            strText = strText.Replace("--", "");

            return strText;
        }
    }
}
