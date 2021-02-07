using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 法定假日 -> 更新 -> 视图模型
    /// </summary>
    public class LegalHolidayUpdateViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "唯一标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "唯一标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        [Required(ErrorMessage = "年份必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "年份只能输入正整数")]
        public int Year { get; set; }

        /// <summary>
        /// 法定假日名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "法定假日名称必填")]
        [StringLength(32, ErrorMessage = "法定假日名称长度为2-32个字符", MinimumLength = 2)]
        public string HolidayName { get; set; }

        /// <summary>
        /// 法定假日日期
        /// </summary>
        [Required(ErrorMessage = "法定假日日期必填")]
        public DateTime HolidayDate { get; set; }
    }
}
