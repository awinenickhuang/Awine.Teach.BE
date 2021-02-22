using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 课程 -> 视图模型
    /// </summary>
    public class CourseViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程介绍
        /// </summary>
        public string CourseIntroduction { get; set; }

        /// <summary>
        /// 课程名师
        /// </summary>
        public string TeacherId { get; set; }

        /// <summary>
        /// 课程名师
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 课程状态 1-启用 2-停用
        /// </summary>
        public int EnabledStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
