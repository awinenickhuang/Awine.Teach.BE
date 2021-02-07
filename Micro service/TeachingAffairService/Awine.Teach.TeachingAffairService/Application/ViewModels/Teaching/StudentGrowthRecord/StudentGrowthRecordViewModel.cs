using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 学生成长记录 -> 视图模型
    /// </summary>
    public class StudentGrowthRecordViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 学生标识
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 主题话题
        /// </summary>
        public string Topics { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 创建者标识
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
