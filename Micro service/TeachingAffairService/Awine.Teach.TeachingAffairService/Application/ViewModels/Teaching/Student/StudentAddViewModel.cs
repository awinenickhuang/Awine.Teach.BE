using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 学生 -> 添加 -> 视图模型
    /// </summary>
    public class StudentAddViewModel
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
        [Required(ErrorMessage = "姓名必填"), MaxLength(6, ErrorMessage = "姓名太长啦"), MinLength(2, ErrorMessage = "姓名太短啦")]
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
        /// 备注信息
        /// </summary>
        public string NoteInformation { get; set; }
    }
}
