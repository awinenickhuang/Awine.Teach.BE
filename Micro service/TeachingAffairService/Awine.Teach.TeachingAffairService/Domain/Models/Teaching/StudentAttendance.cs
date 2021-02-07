using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 学生考勤
    /// </summary>
    public class StudentAttendance : Entity
    {
        /// <summary>
        /// 学员标识
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 班级标识
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 报读课程标识
        /// </summary>
        public string StudentCourseItemId { get; set; }

        /// <summary>
        /// 课节标识
        /// </summary>
        public string ClassCourseScheduleId { get; set; }

        /// <summary>
        /// 课节类型 1-正常课节 2-试听课节 3-补课课节
        /// </summary>
        public int ScheduleIdentification { get; set; }

        /// <summary>
        /// 出勤状态 1-出勤 2-缺勤 3-请假
        /// </summary>
        public int AttendanceStatus { get; set; }

        /// <summary>
        /// 记录状态 1-正常 2-取消
        /// </summary>
        public int RecordStatus { get; set; }

        /// <summary>
        /// 课消数量
        /// </summary>
        public int ConsumedQuantity { get; set; }

        /// <summary>
        /// 补课状态 1-无需补课 2-需要补课 3-已安排补课 4-已完成补课
        /// </summary>
        public int ProcessingStatus { get; set; }

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
