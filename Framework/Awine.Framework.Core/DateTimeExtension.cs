using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Awine.Framework.Core
{
    /// <summary>
    /// 日期时间扩展
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// //注意这里一周是以星期一为第一天
        /// 用法：Week = x.CreateTime.Date.GetWeekOfYear()
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetWeekOfYear(this DateTime dt)
        {
            GregorianCalendar calendar = new GregorianCalendar();
            return calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }
}
