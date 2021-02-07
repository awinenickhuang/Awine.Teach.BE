using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 部门 -> 更新 -> 视图模型
    /// </summary>
    public class DepartmentsUpdateViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "租户标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "租户标识不正确")]
        public string TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称必填")]
        [StringLength(20, ErrorMessage = "名称长度为2-20个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "描述必填")]
        [StringLength(32, ErrorMessage = "描述长度为2-32个字符", MinimumLength = 2)]
        public string Describe { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public int DisplayOrder { get; set; }
    }
}
