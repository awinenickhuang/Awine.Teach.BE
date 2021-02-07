using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.OperationService.Application.ViewModels
{
    /// <summary>
    /// 平台公告 -> 添加 -> 视图模型
    /// </summary>
    public class AnnouncementAddViewModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "标题必填")]
        [StringLength(64, ErrorMessage = "标题长度为2-64个字符", MinimumLength = 2)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "内容必填")]
        [StringLength(1024, ErrorMessage = "内容长度为2-1024个字符", MinimumLength = 2)]
        public string Content { get; set; }

        /// <summary>
        /// 消息类型 1-公告（全部可见） 2-私信（租户可见）
        /// </summary>
        [Required(ErrorMessage = "消息类型必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "消息类型只能输入正整数")]
        public int AnnouncementType { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "租户标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "租户标识不正确")]
        public string TenantId { get; set; }
    }
}
