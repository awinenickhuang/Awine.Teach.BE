using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 学生报名 -> 新生报名 -> 视图模型
    /// </summary>
    public class StudentRegistrationViewModel
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "学生标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "学生标识不正确")]
        public string StudentId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "姓名必填")]
        [StringLength(6, ErrorMessage = "姓名长度为2-6个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "性别只能输入正整数")]
        public int Gender { get; set; }

        /// <summary>
        /// 学生年龄
        /// </summary>
        [Required(ErrorMessage = "年龄必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "年龄只能输入正整数")]
        public int Age { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "电话号码必填")]
        [StringLength(16, ErrorMessage = "电话号码长度为11-16个字符", MinimumLength = 11)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课程标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课程标识不正确")]
        public string CourseId { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        [Required(ErrorMessage = "应收金额必填")]
        [Range(0, 99999.99, ErrorMessage = "应收金额在 0 到 99999.99 元以内")]
        public decimal ReceivableAmount { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        [Required(ErrorMessage = "优惠金额必填")]
        [Range(0, 99999.99, ErrorMessage = "优惠金额在 0 到 99999.99 元以内")]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        [Required(ErrorMessage = "实收金额必填")]
        [Range(0, 99999.99, ErrorMessage = "实收金额在 0 到 99999.99 元以内")]
        public decimal RealityAmount { get; set; }

        /// <summary>
        /// 支付方式标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "支付方式标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "支付方式标识不正确")]
        public string PaymentMethodId { get; set; }

        /// <summary>
        /// 交费信息备注
        /// </summary>
        public string NoteInformation { get; set; }

        /// <summary>
        /// 销售人员标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "销售人标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "销售人标识不正确")]
        public string SalesStaffId { get; set; }

        /// <summary>
        /// 销售人员姓名
        /// </summary>
        [Required(ErrorMessage = "销售人员姓名必填"), MaxLength(6, ErrorMessage = "销售人员姓名太长啦"), MinLength(2, ErrorMessage = "销售人员姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "销售人员姓名必须是中文")]
        public string SalesStaffName { get; set; }

        /// <summary>
        /// 营销渠道标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "营销渠道标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "营销渠道标识不正确")]
        public string MarketingChannelId { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        [Required(ErrorMessage = "购买数量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "购买数量只能输入正整数")]
        public int PurchaseQuantity { get; set; }

        /// <summary>
        /// 收费方式标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "收费方式标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "收费方式标识不正确")]
        public string ChargeMannerId { get; set; }
    }
}
