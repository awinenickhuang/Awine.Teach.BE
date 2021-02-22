using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 班级相册 -> 更新视图模型
    /// </summary>
    public class ClassPhotoalbumUpdateViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "ID必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "ID不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "班级标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "班级标识不正确")]
        public string ClassId { get; set; }

        /// <summary>
        /// 相册名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "名称必填")]
        [StringLength(32, ErrorMessage = "名称长度为2-32个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "封面图片必填")]
        [StringLength(128, ErrorMessage = "封面图片长度为2-128个字符", MinimumLength = 2)]
        public string CoverPhoto { get; set; }

        /// <summary>
        /// 相册描述
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "描述必填")]
        [StringLength(200, ErrorMessage = "描述长度为2-200个字符", MinimumLength = 2)]
        public string Describe { get; set; }

        /// <summary>
        /// 可见范围 1-仅机构可见 2-机构及学生可见 3-完全公开
        /// </summary>
        [Required(ErrorMessage = "可见范围必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "可见范围只能输入正整数")]
        public int VisibleRange { get; set; }
    }
}
