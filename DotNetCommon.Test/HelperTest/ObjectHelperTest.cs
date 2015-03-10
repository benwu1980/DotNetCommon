using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetCommon.Helper;
using NUnit.Framework;

namespace DotNetCommon.Test.HelperTest
{
    [TestFixture]
    public class ObjectHelperTest
    {
        [Test]
        public void ChangeTypeTest()
        {
            var i = ObjectHelper.ChangeType<int>(null);
            Assert.AreEqual(i, 0);

            var s = ObjectHelper.ChangeType<string>(null);
            Assert.AreEqual(s, null);

            var nulldate = ObjectHelper.ChangeType<DateTime>(null, DateTime.MinValue);
            Assert.AreEqual(nulldate, DateTime.MinValue);

            var date = ObjectHelper.ChangeType<DateTime>("2012-02-03");
            Assert.AreEqual(2012, date.Year);

            var e = ObjectHelper.ChangeType<EU_Category>("Apple");
            Assert.AreEqual(e, EU_Category.Apple);

            var e1 = ObjectHelper.ChangeType<EU_Category>(1);
            Assert.AreEqual(e1, EU_Category.Orange);

            var boolv = ObjectHelper.ChangeType<bool>(1);
            Assert.AreEqual(boolv, true);

            var boolv1 = ObjectHelper.ChangeType<bool>(-100);
            Assert.AreEqual(boolv1, true);

            var nullbool = ObjectHelper.ChangeType<bool?>(null);
            Assert.AreEqual(nullbool.HasValue, false);

            var decimalv = ObjectHelper.ChangeType<decimal>(null);
            Assert.AreEqual(decimalv, 0m);

            var nulldecimal = ObjectHelper.ChangeType<decimal?>(1.052m);
            Assert.AreEqual(nulldecimal.HasValue, true);
            if (nulldecimal.HasValue)
            {
                Console.Out.WriteLine(nulldecimal.Value);
            }
        }
    }

    public enum EU_Category : short
    {
        Apple = 0,
        Orange,
        Banana
    }
}
