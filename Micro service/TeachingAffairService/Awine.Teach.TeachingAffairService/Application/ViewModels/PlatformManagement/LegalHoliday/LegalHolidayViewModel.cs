using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 法定假日 -> 视图模型
    /// </summary>
    public class LegalHolidayViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 法定假日名称
        /// </summary>
        public string HolidayName { get; set; }

        /// <summary>
        /// 法定假日日期
        /// </summary>
        public DateTime HolidayDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
