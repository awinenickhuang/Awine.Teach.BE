using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 机构入驻 -> 注册 -> 模型
    /// </summary>
    public class TenantsEnterViewModel
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
        /// 联系电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 省标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "省标识必填")]
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "市标识必填")]
        public string CityId { get; set; }

        /// <summary>
        /// 区标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "区标识必填")]
        public string DistrictId { get; set; }

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
    }
}
