using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 补课管理 -> 视图模型
    /// </summary>
    public class MakeupMissedLessonViewModel
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
        /// 授课老师标识
        /// </summary>
        public string TeacherId { get; set; }

        /// <summary>
        /// 授课老师姓名
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 上课教室标识
        /// </summary>
        public string ClassRoomId { get; set; }

        /// <summary>
        /// 上课教室
        /// </summary>
        public string ClassRoom { get; set; }

        /// <summary>
        /// 上课日期
        /// </summary>
        public DateTime CourseDates { get; set; }

        /// <summary>
        /// 上课开始时间
        /// </summary>
        public int StartHours { get; set; }

        /// <summary>
        /// 上课开始分钟
        /// </summary>
        public int StartMinutes { get; set; }

        /// <summary>
        /// 上课结束时间
        /// </summary>
        public int EndHours { get; set; }

        /// <summary>
        /// 上课结束分钟
        /// </summary>
        public int EndMinutes { get; set; }

        /// <summary>
        /// 状态 1-已创建 2-已结课
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 建造时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
