using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 补课学生管理
    /// </summary>
    public class MakeupMissedLessonStudent : Entity
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 报读课程标识
        /// </summary>
        public string StudentCourseItemId { get; set; }

        /// <summary>
        /// 课节标识
        /// </summary>
        public string ClassCourseScheduleId { get; set; }

        /// <summary>
        /// 考勤标识
        /// </summary>
        public string AttendanceId { get; set; }

        /// <summary>
        /// 班级标识
        /// </summary>
        public string MakeupMissedLessonId { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 学生报读课程信息
        /// </summary>
        public StudentCourseItem StudentCourseItem { get; set; } = new StudentCourseItem();
    }
}
