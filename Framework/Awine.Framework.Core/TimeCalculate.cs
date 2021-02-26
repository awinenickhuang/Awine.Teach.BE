using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Core
{
    /// <summary>
    /// 日期时间处理
    /// </summary>
    public static class TimeCalculate
    {
        /// <summary>
        /// 计算两个日期的相隔天数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int DateDiff(DateTime startTime, DateTime endTime)
        {
            TimeSpan startTimeSpan = new TimeSpan(startTime.Ticks);
            TimeSpan endTimeSpan = new TimeSpan(endTime.Ticks);
            TimeSpan timeSpan = startTimeSpan.Subtract(endTimeSpan).Duration();
            return timeSpan.Days;
        }

        /// <summary>
        /// 计算今天是星期几
        /// </summary>
        /// <param name="today"></param>
        /// <returns></returns>
        public static int DayOfWeek(DateTime today)
        {
            return Convert.ToInt32(today.DayOfWeek) < 1 ? 7 : Convert.ToInt32(today.DayOfWeek);
        }

        /// <summary>
        /// 当月第一天0时0分0秒
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTheFirstDayoftheCurrentMonth()
        {
            return DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
        }

        /// <summary>
        /// 当月最后一天23时59分59秒
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTheLastDayoftheCurrentMonth()
        {
            return DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
        }

        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).Date;
        }

        //// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime datetime)
        {
            return DateTime.Parse(datetime.AddDays(1 - datetime.Day).AddMonths(1).ToShortDateString()).AddSeconds(-1);
            //return datetime.AddDays(1 - datetime.Day).Date.AddMonths(1).AddDays(-1);
        }

        //// <summary>
        /// 取得上个月第一天
        /// </summary>
        /// <param name="datetime">要取得上个月第一天的当前时间</param>
        /// <returns></returns>
        public static DateTime FirstDayOfPreviousMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).Date.AddMonths(-1);
        }

        //// <summary>
        /// 取得上个月的最后一天
        /// </summary>
        /// <param name="datetime">要取得上个月最后一天的当前时间</param>
        /// <returns></returns>
        public static DateTime LastDayOfPrdviousMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).Date.AddDays(-1);
        }

        /// <summary>
        /// 取某年某个月份的总天数
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int DaysInMonth(DateTime datetime)
        {
            return DateTime.DaysInMonth(datetime.Year, datetime.Month);
        }
    }
}
