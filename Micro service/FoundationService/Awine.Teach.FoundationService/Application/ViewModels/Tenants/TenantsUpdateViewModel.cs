using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 租户信息 -> 更新 -> 视图模型
    /// </summary>
    public class TenantsUpdateViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Required(ErrorMessage = "联系人必填"), MaxLength(6, ErrorMessage = "联系人姓名太长啦"), MinLength(2, ErrorMessage = "联系人姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "联系人姓名必须是中文")]
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "联系电话必填")]
        [StringLength(16, ErrorMessage = "联系电话长度为11-16个字符", MinimumLength = 11)]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 省标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "省标识必填")]
        public string ProvinceId { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        [Required]
        [StringLength(32, ErrorMessage = "{0} 省名称的长度为 {2} 到 {32}个字符", MinimumLength = 2)]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "市标识必填")]
        public string CityId { get; set; }

        /// <summary>
        /// 市名称
        /// </summary>
        [Required]
        [StringLength(32, ErrorMessage = "{0} 市名称的长度为 {2} 到 {32}个字符", MinimumLength = 2)]
        public string CityName { get; set; }

        /// <summary>
        /// 区标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "区标识必填")]
        public string DistrictId { get; set; }

        /// <summary>
        /// 区名称
        /// </summary>
        [Required]
        [StringLength(32, ErrorMessage = "{0} 区名称的长度为 {2} 到 {32}个字符", MinimumLength = 2)]
        public string DistrictName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "地址必填")]
        [StringLength(128, ErrorMessage = "地址长度为2-128个字符", MinimumLength = 2)]
        public string Address { get; set; }

        /// <summary>
        /// 行业类型ID
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "行业类型ID必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "行业类型ID不正确")]
        public string IndustryId { get; set; }
    }
}
