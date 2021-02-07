using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 教室管理 -> 添加 -> 视图模型
    /// </summary>
    public class ClassRoomAddViewModel
    {
        /// <summary>
        /// 教室名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "教室名称必填")]
        [StringLength(32, ErrorMessage = "教室名称长度为2-32个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 教室备注
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "教室备注必填")]
        [StringLength(32, ErrorMessage = "教室备注长度为2-32个字符", MinimumLength = 2)]
        public string NoteContent { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [Required(ErrorMessage = "显示顺序必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "显示顺序只能输入正整数")]
        public string DisplayOrder { get; set; }
    }
}
