using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 咨询记录 -> 跟进记录 -> 添加 -> 视图模型
    /// </summary>
    public class CommunicationRecordAddViewModel
    {
        /// <summary>
        /// 咨询记录标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "咨询记录标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "咨询记录标识不正确")]
        public string ConsultRecordId { get; set; }

        /// <summary>
        /// 沟通内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "沟通内容必填")]
        [StringLength(64, ErrorMessage = "沟通内容长度为1-64个字符", MinimumLength = 1)]
        public string CommunicationContent { get; set; }

        /// <summary>
        /// 成交意向星级
        /// </summary>
        [Required(ErrorMessage = "成交意向星级必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "成交意向星级只能输入正整数")]
        public int ClinchIntentionStar { get; set; }

        /// <summary>
        /// 跟进状态 1-待跟进 2-跟进中 3-已邀约 4-已试听 5-已到访 6-已成交
        /// </summary>
        [Required(ErrorMessage = "跟进状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "跟进状态只能输入正整数")]
        public int TrackingState { get; set; }

        /// <summary>
        /// 沟通方式
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "沟通方式必填")]
        [StringLength(16, ErrorMessage = "沟通方式长度为2-16个字符", MinimumLength = 2)]
        public string CommunicateWay { get; set; }
    }
}
