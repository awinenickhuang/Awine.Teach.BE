using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 补课学生 -> 视图模型
    /// </summary>
    public class MakeupMissedLessonStudentViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

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
        /// 建造时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 学生报读课程信息
        /// </summary>
        public StudentCourseItemViewModel StudentCourseItemViewModel { get; set; } = new StudentCourseItemViewModel();
    }
}
