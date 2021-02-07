using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 学生成长记录评论
    /// </summary>
    public class StudentGrowthRecordCommentAddViewModel
    {
        /// <summary>
        /// 学生成长记录标识
        /// </summary>
        public string StudentGrowthRecordId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }
    }
}
