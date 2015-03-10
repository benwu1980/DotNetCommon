using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCommon.Caching;

namespace DotNetCommon.Test.Cache
{
    public class CacheTest
    {
        public void AddCache()
        {
            DotNetCommon.Caching.Cache.Set<string>("aaa", "aaaaaaaa");

          
        }

        public void ShowResult()
        {
            var result = DotNetCommon.Caching.Cache.Get<string>("aaa");

            Console.WriteLine(result);
        }
    }
}
