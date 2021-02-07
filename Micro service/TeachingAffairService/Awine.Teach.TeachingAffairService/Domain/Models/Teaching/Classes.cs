using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 班级管理
    /// </summary>
    public class Classes : Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 班级容量（人数限制）
        /// </summary>
        public int ClassSize { get; set; } = 1;

        /// <summary>
        /// 当前拥有学生数量
        /// </summary>
        public int OwnedStudents { get; set; } = 0;

        /// <summary>
        /// 授课老师标识
        /// </summary>
        public string TeacherId { get; set; }

        /// <summary>
        /// 授课老师姓名
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 开班日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 招生状态 1-开放招生 2-暂停招生 3-授课结束
        /// </summary>
        public int RecruitStatus { get; set; }

        /// <summary>
        /// 默认上课教室标识
        /// </summary>
        public string ClassRoomId { get; set; }

        /// <summary>
        /// 默认上课教室
        /// </summary>
        public string ClassRoomName { get; set; }

        /// <summary>
        /// 班级类型 1-one to many 2-one to one
        /// </summary>
        public int TypeOfClass { get; set; } = 1;

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
