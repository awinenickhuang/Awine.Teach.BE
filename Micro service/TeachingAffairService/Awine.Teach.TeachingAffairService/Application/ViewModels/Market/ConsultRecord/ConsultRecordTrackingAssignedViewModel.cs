using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 跟进任务指派 -> 视图模型
    /// </summary>
    public class ConsultRecordTrackingAssignedViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string ConsultRecordId { get; set; }

        /// <summary>
        /// 跟进人标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "跟进人标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "跟进人标识不正确")]
        public string TrackingStafferId { get; set; }

        /// <summary>
        /// 跟进人姓名
        /// </summary>
        [Required(ErrorMessage = "跟进人姓名必填"), MaxLength(6, ErrorMessage = "跟进人姓名太长啦"), MinLength(2, ErrorMessage = "跟进人姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "跟进人姓名必须是中文")]
        public string TrackingStafferName { get; set; }
    }
}
