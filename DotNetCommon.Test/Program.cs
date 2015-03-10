using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCommon.Helper;
using System.Net;
using DotNetCommon.Data;
using System.Linq.Expressions;
using DotNetCommon.Test.HelperTest;
using DotNetCommon.Test.Web;
using DotNetCommon.Test.CommonTest;
using DotNetCommon.Test.Data;
using DotNetCommon.Web.Compressor;
using DotNetCommon.Test.Cache;


namespace DotNetCommon.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CacheTest test1 = new CacheTest();
                test1.AddCache();
                Console.WriteLine("=====================");

                CacheTest test0 = new CacheTest();
                test0.ShowResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Read();
        }


    }


}
