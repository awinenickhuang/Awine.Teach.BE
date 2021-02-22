using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 班级 -> 视图模型
    /// </summary>
    public class ClassesViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

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
        public int ClassSize { get; set; }

        /// <summary>
        /// 当前拥有学生数量
        /// </summary>
        public int OwnedStudents { get; set; }

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
        /// 上课教室标识
        /// </summary>
        public string ClassRoomId { get; set; }

        /// <summary>
        /// 上课教室
        /// </summary>
        public string ClassRoomName { get; set; }

        /// <summary>
        /// 班级类型 1-one to many 2-one to one
        /// </summary>
        public int TypeOfClass { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
