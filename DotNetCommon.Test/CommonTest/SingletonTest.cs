using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCommon.Common;

namespace DotNetCommon.Test.CommonTest
{
    public class SingletonTest
    {
        public void Test()
        {
            var handler0 = Singleton<Handler>.Instance;
            var handler1 = Singleton<Handler>.Instance;

            var handler2 = new Handler();

            handler0.Message = "handler0";

            Console.WriteLine(handler0 == handler1);

            handler0.DoWork();
            handler1.DoWork();
            handler2.DoWork();
        }
    }
}
