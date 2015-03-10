using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCommon.Helper
{
    /// <summary>
    /// 关于数字的一些工具类
    /// </summary>
    public class NumericHelper
    {
        /// <summary>
        /// 将数字大小简化，即转化为GB,MB,KB,Bytes
        /// </summary>
        /// <param name="fileSize">数字大小</param>
        /// <returns></returns>
        public static string FormatFileSize(int fileSize)
        {
            try
            {
                if (fileSize >= 1073741824)
                    return (fileSize / 1024.0 / 1024.0 / 1024.0).ToString("#0.00") + " GB";

                if (fileSize >= 1048576)
                    return (fileSize / 1024.0 / 1024.0).ToString("#0.00") + " MB";

                if (fileSize >= 1024)
                    return (fileSize / 1024.0).ToString("#0.00") + " KB";

                if (fileSize < 1024)
                    return fileSize + " Bytes";
            }
            catch (Exception ex)
            {
                return "0 Bytes";
            }

            return "0 Bytes";
        }
    }
}
