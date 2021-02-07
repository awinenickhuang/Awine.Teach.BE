using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 班级 -> 排课信息 -> 视图模型
    /// </summary>
    public class CourseScheduleViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 排课年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 班级标识
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

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
        /// 授课老师标识
        /// </summary>
        public string TeacherId { get; set; }

        /// <summary>
        /// 授课老师姓名
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
        /// 课节状态 1-待上课 2-已结课
        /// </summary>
        public int ClassStatus { get; set; }

        /// <summary>
        /// 课节识标 1-正常课节 2-试听课节 3-补课课节
        /// </summary>
        public int ScheduleIdentification { get; set; }

        /// <summary>
        /// 上课教室标识
        /// </summary>
        public string ClassRoomId { get; set; }

        /// <summary>
        /// 上课教室
        /// </summary>
        public string ClassRoom { get; set; }

        /// <summary>
        /// 出勤人数
        /// </summary>
        public int ActualAttendanceNumber { get; set; }

        /// <summary>
        /// 请假人数
        /// </summary>
        public int ActualleaveNumber { get; set; }

        /// <summary>
        /// 缺课人数
        /// </summary>
        public int ActualAbsenceNumber { get; set; }

        /// <summary>
        /// 课消数量
        /// </summary>
        public int ConsumedQuantity { get; set; } = 0;

        /// <summary>
        /// 课消金额
        /// </summary>
        public decimal ConsumedAmount { get; set; } = 0.00M;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
