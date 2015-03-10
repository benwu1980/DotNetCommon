using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DotNetCommon.Helper;

namespace DotNetCommon.Test.HelperTest
{
    public class DateTimeHelperTest
    {
        public void LunisolarCalendar()
        {
            DateTime date = DateTime.Now;
            Console.WriteLine("当前日期：" + date);
            Console.WriteLine("对应阴历日期：" + DateTimeHelper.GetChineseDateTime(DateTime.Now));
        }
    }
}
