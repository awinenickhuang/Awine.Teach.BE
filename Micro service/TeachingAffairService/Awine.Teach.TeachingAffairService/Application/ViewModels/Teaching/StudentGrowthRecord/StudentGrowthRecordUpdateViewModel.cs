using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 学生成长记录
    /// </summary>
    public class StudentGrowthRecordUpdateViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 主题话题
        /// </summary>
        public string Topics { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }
    }
}
