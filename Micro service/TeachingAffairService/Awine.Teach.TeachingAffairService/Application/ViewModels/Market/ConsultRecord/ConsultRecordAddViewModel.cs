using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 咨询记录 -> 添加 -> 视图模型
    /// </summary>
    public class ConsultRecordAddViewModel
    {
        /// <summary>
        /// 咨询人
        /// </summary>
        [Required(ErrorMessage = "姓名必填"), MaxLength(6, ErrorMessage = "姓名太长啦"), MinLength(2, ErrorMessage = "姓名太啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "姓名必须是中文")]
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
        [Required(ErrorMessage = "学生年龄必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "学生年龄只能输入正整数")]
        [Range(1, 100, ErrorMessage = "学生年龄只能是大于1且小于100")]
        public int Age { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号码必填")]
        [RegularExpression("^[1]+[3,4,5,7,8]+\\d{9}", ErrorMessage = "不合法的电话号码格式")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 咨询课程标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "咨询课程标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "咨询课程标识不正确")]
        public string CounselingCourseId { get; set; }

        /// <summary>
        /// 客户基本情况
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "客户基本情况必填")]
        [StringLength(64, ErrorMessage = "客户基本情况长度为2-64个字符", MinimumLength = 2)]
        public string BasicSituation { get; set; }

        /// <summary>
        /// 营销渠道标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "营销渠道标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "营销渠道标识不正确")]
        public string MarketingChannelId { get; set; }

        /// <summary>
        /// 成交意向星级
        /// </summary>
        [Required(ErrorMessage = "成交意向星级必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "成交意向星级只能输入正整数")]
        public int ClinchIntentionStar { get; set; }
    }
}
