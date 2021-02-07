using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 学生选课信息
    /// </summary>
    public class StudentCourseItem : Entity
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
        /// 课程标识
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 所在班级标识
        /// </summary>
        public string ClassesId { get; set; }

        /// <summary>
        /// 所在班级名称
        /// </summary>
        public string ClassesName { get; set; }

        /// <summary>
        /// 购买数量（总计）
        /// </summary>
        public int PurchaseQuantity { get; set; }

        /// <summary>
        /// 已消耗数量（总计）
        /// </summary>
        public int ConsumedQuantity { get; set; }

        /// <summary>
        /// 剩余数量（总计）
        /// </summary>
        public int RemainingNumber { get; set; }

        #region -----------------------------------学生报名时采用的收费标准信息---------------------------------

        /// <summary>
        /// 收费方式 1-按课时收费 2-按月收费
        /// </summary>
        public int ChargeManner { get; set; }

        /// <summary>
        /// 课程授课时长 单位（收费方按课时时为课时，按月时为月）
        /// </summary>
        public int CourseDuration { get; set; }

        /// <summary>
        /// 课程总价（元）
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 课程单价（元）
        /// </summary>
        public decimal UnitPrice { get; set; }

        #endregion

        /// <summary>
        /// 学习进度 1-已报名（未分班） 2-已报名（已分班）3-停课 4-退费 5-毕业
        /// </summary>
        public int LearningProcess { get; set; } = 1;

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
