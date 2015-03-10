using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DotNetCommon.Helper
{
    public class DateTimeHelper
    {
        /// <summary>
        /// 时间差
        /// 大于一天，则只显示startTime
        /// 否则显示：X小时前或X分钟前
        /// </summary>
        /// <returns></returns>
        public static string DateDiff(DateTime startTime, DateTime endTime)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = startTime - endTime;
                if (ts.Days >= 1)
                {
                    dateDiff = startTime.Month.ToString() + "月" + startTime.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }


        #region 一天中最开始时间和结束时间

        /// <summary>
        /// 一天中最早的时间:  2014-05-10 00:00:000
        /// </summary>
        /// <returns></returns>
        public static DateTime DayBegin()
        {
            return DayBegin(DateTime.Now);
        }

        /// <summary>
        /// 某个时间所在的当天中最早的时间:  2014-05-10 00:00:00
        /// </summary>
        /// <param name="date">指定的时间</param>
        /// <returns>新的时间</returns>
        public static DateTime DayBegin(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        /// <summary>
        ///  一天中最晚的时间:  2014-05-10 23:59:59.990
        /// </summary>
        /// <returns></returns>
        public static DateTime DayEnd()
        {
            return DayEnd(DateTime.Now);
        }

        /// <summary>
        /// 某个时间所在的当天中最晚的时间:  2014-05-10 23:59:59.990
        /// </summary>
        /// <param name="date">Date to change into last datetime</param>
        /// <returns>new datetime value</returns>
        public static DateTime DayEnd(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 990);
        }

        #endregion


        #region 星期的开始时间和最后时间

        /// <summary>
        /// 返回当前时间所在的周的最开始时间,以星期日为同的开始时间
        /// </summary>
        /// <returns>星期的开始时间</returns>
        public static DateTime WeekBegin()
        {
            return WeekBegin(DayOfWeek.Sunday);
        }

        /// <summary>
        /// 返回当前时间所在的周的最开始时间，需要设定周的第一天是星期几
        /// </summary>
        /// <param name="firstDayOfWeek">设置每周开始是哪一天,通常一周开始是星期天或星期一</param>
        /// <returns>星期的开始时间</returns>
        public static DateTime WeekBegin(DayOfWeek firstDayOfWeek)
        {
            return WeekBegin(DateTime.Now, firstDayOfWeek);
        }

        /// <summary>
        /// 返回指定时间所在的周的最开始时间(以星期日为周的开始时间)
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>星期的开始时间</returns>
        public static DateTime WeekBegin(DateTime date)
        {
            return WeekBegin(date, DayOfWeek.Sunday);
        }

        /// <summary>
        /// 返回指定时间所在的周的最开始时间，需要设定周的第一天是星期几
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <param name="firstDayOfWeek">设定周的第一天是星期几,通常一周开始是星期天或星期一</param>
        /// <returns>周的第一天</returns>
        public static DateTime WeekBegin(DateTime date, DayOfWeek firstDayOfWeek)
        {
            return WeekEnd(date, firstDayOfWeek).AddDays(-7);
        }


        /// <summary>
        ///返回当前时间所在的周的结束时间,以星期日为同的开始时间
        /// </summary>
        /// <remarks>周结束时间</remarks>
        public static DateTime WeekEnd()
        {
            return WeekEnd(DayOfWeek.Sunday);
        }

        /// <summary>
        ///返回当前时间所在的周的结束时间,需要设定周的第一天是星期几
        /// </summary>
        /// <param name="firstDayOfWeek">设定周的第一天是星期几,通常一周开始是星期天或星期一</param>
        /// <returns>周最后一天</returns>
        public static DateTime WeekEnd(DayOfWeek firstDayOfWeek)
        {
            return WeekEnd(DateTime.Now, firstDayOfWeek);
        }


        /// <summary>
        ///返回指定时间所在的周的结束时间,需要设定周的第一天是星期几
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <param name="firstDayOfWeek">设定周的第一天是星期几,通常一周开始是星期天或星期一</param>
        /// <returns>周最后一天</returns>
        public static DateTime WeekEnd(DateTime date, DayOfWeek firstDayOfWeek)
        {
            int delta = 0;
            switch (date.DayOfWeek)
            {
                case System.DayOfWeek.Sunday: delta = 6; break;
                case System.DayOfWeek.Monday: delta = 5; break;
                case System.DayOfWeek.Tuesday: delta = 4; break;
                case System.DayOfWeek.Wednesday: delta = 3; break;
                case System.DayOfWeek.Thursday: delta = 2; break;
                case System.DayOfWeek.Friday: delta = 1; break;
                case System.DayOfWeek.Saturday: delta = 0; break;
            }
            return DayEnd(date.AddDays(delta));
        }

        /// <summary>
        ///返回指定时间所在的周的结束时间,以星期日作为周的第一天
        /// </summary>
        /// <param name="date">指定时间</param>
        /// <returns>周最后一天</returns>
        public static DateTime WeekEnd(DateTime date)
        {
            return WeekEnd(date, DayOfWeek.Sunday);
        }

        #endregion


        #region 月的开始时间和最后时间
        /// <summary>
        /// 当前日期的每月的开始一天
        /// </summary>
        /// <returns>当前日期所在月的开始一天的时间</returns>
        public static DateTime MonthBegin()
        {
            return MonthBegin(DateTime.Now);
        }

        /// <summary>
        /// 获取指定日期的每月的最开始一天
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>此月最开始一天的时间</returns>
        public static DateTime MonthBegin(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0);
        }


        public static DateTime MonthEnd()
        {
            return MonthEnd(DateTime.Now);

        }

        /// <summary>
        /// 获取指定日期的每月的最后的一天
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>此月最后一天的时间</returns>
        public static DateTime MonthEnd(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 23, 59, 999);
        }

        #endregion

        
        #region 获取阴历的时间

        private static ChineseLunisolarCalendar cCalendar = new ChineseLunisolarCalendar();

        /// <summary>
        /// 十天干
        /// </summary>
        private const string TIAN_GAN = "甲乙丙丁戊己庚辛壬癸";

        /// <summary>
        /// 十二地支
        /// </summary>
        private const string DI_ZHI = "子丑寅卯辰巳午未申酉戌亥";

        /// <summary>
        /// 十二生肖
        /// </summary>
        private const string SHEN_XIAO = "鼠牛虎免龙蛇马羊猴鸡狗猪";

        /// <summary>
        /// 大写数字
        /// </summary>
        private const string ChineseNumber = "一二三四五六七八九十";

        /// <summary>
        /// 阴历日期前缀
        /// </summary>
        private const string DAYS_PREFIX = "初十廿三";

        /// <summary>
        /// 农历月对应的大写
        /// </summary>
        private static string[] _months = { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二(腊)" };

        /// <summary>
        /// 根据公历日期获取相应的农历日期
        /// </summary>
        /// <param name="datetime">公历日期</param>
        /// <returns></returns>
        public static string GetChineseDateTime(DateTime datetime)
        {
            int lyear = cCalendar.GetYear(datetime);
            int lmonth = cCalendar.GetMonth(datetime);
            int lday = cCalendar.GetDayOfMonth(datetime);

            //获取闰月， 0 则表示没有闰月
            int leapMonth = cCalendar.GetLeapMonth(lyear);

            bool isleap = false;

            if (leapMonth > 0)
            {
                if (leapMonth == lmonth)
                {
                    isleap = true;//闰月
                    lmonth--;
                }
                else if (lmonth > leapMonth)
                {
                    lmonth--;
                }
            }

            return string.Concat(GetLunisolarYear(lyear), "年", isleap ? "闰" : string.Empty, GetLunisolarMonth(lmonth), "月", GetLunisolarDay(lday));
        }

        /// <summary>
        /// 返回农历天干地支年 
        /// </summary>
        /// <param name="year">农历年</param>
        /// <returns></returns>
        public static string GetLunisolarYear(int year)
        {
            if (year > 3)
            {
                int tgIndex = (year - 4) % 10;
                int dzIndex = (year - 4) % 12;

                return string.Concat(TIAN_GAN[tgIndex], DI_ZHI[dzIndex], "[", SHEN_XIAO[dzIndex], "]");

            }

            throw new ArgumentOutOfRangeException("无效的年份!");
        }

        /// <summary>
        /// 返回农历月
        /// </summary>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public static string GetLunisolarMonth(int month)
        {
            if (month < 13 && month > 0)
            {
                return _months[month - 1];
            }

            throw new ArgumentOutOfRangeException("无效的月份!");
        }

        /// <summary>
        /// 返回农历日
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetLunisolarDay(int day)
        {
            if (day > 0 && day < 32)
            {
                if (day != 20 && day != 30)
                {
                    return string.Concat(DAYS_PREFIX[(day - 1) / 10], ChineseNumber[(day - 1) % 10]);
                }
                else
                {
                    return string.Concat(ChineseNumber[(day - 1) / 10], DAYS_PREFIX[1]);
                }
            }

            throw new ArgumentOutOfRangeException("无效的日!");
        }

        #endregion

    }
}
