using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 更新 -> 班级招生状态 -> 视图模型
    /// </summary>
    public class ClassesUpdateRecruitStatusViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 招生状态 1-开放招生 2-暂停招生 3-授课结束
        /// </summary>
        [Required(ErrorMessage = "课程状态必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "课程状态只能输入正整数")]
        public int RecruitStatus { get; set; }
    }
}
