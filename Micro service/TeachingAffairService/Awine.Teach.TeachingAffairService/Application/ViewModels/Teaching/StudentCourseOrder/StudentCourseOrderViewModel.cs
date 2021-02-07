using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 订单信息 -> 视图模型
    /// </summary>
    public class StudentCourseOrderViewModel
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
        /// 收费项目标识
        /// </summary>
        public string StudentCourseItemId { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal ReceivableAmount { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal RealityAmount { get; set; }

        /// <summary>
        /// 支付方式标识
        /// </summary>
        public string PaymentMethodId { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string PaymentMethodName { get; set; }

        /// <summary>
        /// 交费信息备注
        /// </summary>
        public string NoteInformation { get; set; }

        /// <summary>
        /// 经办人员标识
        /// </summary>
        public string OperatorId { get; set; }

        /// <summary>
        /// 经办人员姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 销售人员标识
        /// </summary>
        public string SalesStaffId { get; set; }

        /// <summary>
        /// 销售人员姓名
        /// </summary>
        public string SalesStaffName { get; set; }

        /// <summary>
        /// 营销渠道标识
        /// </summary>
        public string MarketingChannelId { get; set; }

        /// <summary>
        /// 营销渠道名称
        /// </summary>
        public string MarketingChannelName { get; set; }

        /// <summary>
        /// 购买数量（当前订单）
        /// </summary>
        public int PurchaseQuantity { get; set; }

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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
