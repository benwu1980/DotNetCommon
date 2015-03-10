using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DotNetCommon.Helper
{
    public class PathHelper
    {
        /// <summary>
        /// 合并路径
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static string Combine(params string[] paths)
        {
            return Path.GetFullPath(Path.Combine(paths));
        }
    }
}
