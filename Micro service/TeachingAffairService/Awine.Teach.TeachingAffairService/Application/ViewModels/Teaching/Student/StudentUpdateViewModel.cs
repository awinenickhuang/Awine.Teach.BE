using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 学生 -> 更新 -> 视图模型
    /// </summary>
    public class StudentUpdateViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "性别必填")]
        [StringLength(2, ErrorMessage = "性别长度为1-2个字符", MinimumLength = 1)]
        public string Gender { get; set; }

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
        /// 营销渠道标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "营销渠道标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "营销渠道标识不正确")]
        public string MarketingChannelId { get; set; }

        /// <summary>
        /// 销售人标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "销售人标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "销售人标识不正确")]
        public string SalesStaffId { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string NoteInformation { get; set; }
    }
}
