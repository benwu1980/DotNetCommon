using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace DotNetCommon.Test.Extension
{
    [TestFixture]
    public class DateTimeTest
    {
        [Test]
        public void JavascriptDateTest()
        {
            var date = DateTime.Now;
            //Console.Out.WriteLine(date.ToJSDate());
        }

    }
}
