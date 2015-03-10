using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper
{
    /// <summary>
    /// 字符编码的扩展
    /// </summary>
    public class EncodingEx
    {
        /// <summary>
        /// 表示中文的字符编码
        /// </summary>
        public static Encoding GB2312
        {
            get { return Encoding.GetEncoding("GB2312"); }
        }
    }
}
