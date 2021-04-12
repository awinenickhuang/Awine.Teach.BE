using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 租户信息设置 -> 更新
    /// </summary>
    public class TenantDefaultSettingsUpdateViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 最大分支机构数量
        /// </summary>
        [Required(ErrorMessage = "最大分支机构数量必填")]
        [RegularExpression(@"^\+?0|[1-9][0-9]*$", ErrorMessage = "最大分支机构数量只能输入整数")]
        public int MaxNumberOfBranch { get; set; }

        /// <summary>
        /// 最大部门数量
        /// </summary>
        [Required(ErrorMessage = "最大部门数量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "最大部门数量只能输入正整数")]
        public int MaxNumberOfDepartments { get; set; }

        /// <summary>
        /// 最大角色数量
        /// </summary>
        [Required(ErrorMessage = "最大角色数量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "最大角色数量只能输入正整数")]
        public int MaxNumberOfRoles { get; set; }

        /// <summary>
        /// 最大用户数量
        /// </summary>
        [Required(ErrorMessage = "最大用户数量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "最大用户数量只能输入正整数")]
        public int MaxNumberOfUser { get; set; }

        /// <summary>
        /// 最大课程数量
        /// </summary>
        [Required(ErrorMessage = "最大课程数量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "最大课程数量只能输入正整数")]
        public int MaxNumberOfCourse { get; set; }

        /// <summary>
        /// 最大班级数量
        /// </summary>

        [Required(ErrorMessage = "最大班级数量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "最大班级数量只能输入正整数")]
        public int MaxNumberOfClass { get; set; }

        /// <summary>
        /// 最大教室数量
        /// </summary>
        [Required(ErrorMessage = "最大教室数量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "最大教室数量只能输入正整数")]
        public int MaxNumberOfClassRoom { get; set; }

        /// <summary>
        /// 最大学生数量
        /// </summary>
        [Required(ErrorMessage = "最大学生数量必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "最大学生数量只能输入正整数")]
        public int MaxNumberOfStudent { get; set; }

        /// <summary>
        /// 最大存储空间
        /// </summary>
        [Required(ErrorMessage = "最大存储空间必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "最大存储空间只能输入正整数")]
        public long MaxStorageSpace { get; set; }

        /// <summary>
        /// SaaS版本标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "SaaS版本标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "SaaS版本标识不正确")]
        public string SaaSVersionId { get; set; }
    }
}
