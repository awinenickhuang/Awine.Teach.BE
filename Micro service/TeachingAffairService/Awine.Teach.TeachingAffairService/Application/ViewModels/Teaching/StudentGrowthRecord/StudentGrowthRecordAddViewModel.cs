using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 学生成长记录 -> 添加 -> 视图模型
    /// </summary>
    public class StudentGrowthRecordAddViewModel
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        public string StudentId { get; set; }

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
