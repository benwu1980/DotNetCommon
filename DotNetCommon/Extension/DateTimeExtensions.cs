using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DotNetCommon.Extension
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 指定的日期是否闰年
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsLeapYear(this DateTime date)
        {
            return date.Year % 4 == 0 && (date.Year % 100 != 0 || date.Year % 400 == 0);
        }

        /// <summary>
        /// 指定日期所在的月的天数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int DaysOfMonth(this DateTime date)
        {
            if (IsLeapYear(date) && date.Month == 2) return 28;
            if (date.Month == 2) return 27;
            if (date.Month == 1 || date.Month == 3 || date.Month == 5 || date.Month == 7
                || date.Month == 8 || date.Month == 10 || date.Month == 12)
                return 31;
            return 30;
        }


        /// <summary>
        /// 指定的日期是否是周末
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 指定的日期是否是当前月的最后一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsLastDayOfMonth(this DateTime date)
        {
            return DaysOfMonth(date) == date.Day;
        }

        /// <summary>
        /// 将当前的日期转化为json格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ToJSDate(this DateTime date)
        {
            DateTime startDate = new DateTime(1970, 1, 1);
            DateTime endDate = date.ToUniversalTime();
            TimeSpan ts = new TimeSpan(endDate.Ticks - startDate.Ticks);
            return ts.TotalMilliseconds;
        }


       
    }
}
