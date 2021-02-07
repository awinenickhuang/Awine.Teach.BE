using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 试听管理
    /// </summary>
    public class TrialClass : Entity
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 性别 1-男 2-女
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 老师标识
        /// </summary>
        public string TeacherId { get; set; }

        /// <summary>
        /// 老师姓名
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 课节标识
        /// </summary>
        public string CourseScheduleId { get; set; }

        /// <summary>
        /// 课节信息
        /// </summary>
        public string CourseScheduleInformation { get; set; }

        /// <summary>
        /// 是否转化报班
        /// </summary>
        public bool IsTransformation { get; set; } = false;

        /// <summary>
        /// 试听状态 1-已创建 2-已到课 3-已失效
        /// </summary>
        public int ListeningState { get; set; } = 1;

        /// <summary>
        /// 试听类型 1-跟班试听 2-一对一试听
        /// </summary>
        public int TrialClassGenre { get; set; } = 1;

        /// <summary>
        /// 学生类型 1-意向学生 2-正式学生
        /// </summary>
        public int StudentCategory { get; set; }

        /// <summary>
        /// 操作人标识
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
