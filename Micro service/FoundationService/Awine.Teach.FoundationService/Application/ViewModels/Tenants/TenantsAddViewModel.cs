using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 租户 -> 添加 -> 视图模型
    /// </summary>
    public class TenantsAddViewModel
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "租户名称必填")]
        [StringLength(32, ErrorMessage = "租户名称长度为2-32个字符", MinimumLength = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Required(ErrorMessage = "联系人必填"), MaxLength(6, ErrorMessage = "联系人姓名太长啦"), MinLength(2, ErrorMessage = "联系人姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "联系人姓名必须是中文")]
        public string Contacts { get; set; }

        /// <summary>
        /// 管理员密码
        /// </summary>
        [Required(ErrorMessage = "管理员密码必填")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "联系电话必填")]
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 租户类型 1-免费 2-试用 3-付费（VIP）8-代理商 9-平台运营
        /// </summary>
        [Required(ErrorMessage = "租户类型必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "租户类型只能输入正整数")]
        public int ClassiFication { get; set; }

        /// <summary>
        /// 版本标识
        /// </summary>
        [Required(ErrorMessage = "版本标识必填")]
        public string SaaSVersionId { get; set; }

        /// <summary>
        /// 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        [Required(ErrorMessage = "租户状态必填")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "租户状态只能输入整数")]
        public int Status { get; set; }

        /// <summary>
        /// 省标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "省标识必填")]
        public string ProvinceId { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "省名称必填")]
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "市名称必填")]
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
        [Required(AllowEmptyStrings = false, ErrorMessage = "区名称必填")]
        [StringLength(32, ErrorMessage = "{0} 区名称的长度为 {2} 到 {32}个字符", MinimumLength = 2)]
        public string DistrictName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "地址必填")]
        [StringLength(128, ErrorMessage = "地址长度为2-128个字符", MinimumLength = 2)]
        public string Address { get; set; }

        /// <summary>
        /// 行业类型标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "行业类型标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "行业类型标识不正确")]
        public string IndustryId { get; set; }

        /// <summary>
        /// 版本定价策略标识
        /// </summary>
        [Required(ErrorMessage = "版本定价策略标识必填")]
        public string PricingTacticsId { get; set; }

        /// <summary>
        /// 业绩归属人ID
        /// </summary>
        public string PerformanceOwnerId { get; set; }
    }
}
