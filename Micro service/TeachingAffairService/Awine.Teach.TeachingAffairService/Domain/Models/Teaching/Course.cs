using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 课程
    /// </summary>
    public class Course : Entity
    {
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
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
