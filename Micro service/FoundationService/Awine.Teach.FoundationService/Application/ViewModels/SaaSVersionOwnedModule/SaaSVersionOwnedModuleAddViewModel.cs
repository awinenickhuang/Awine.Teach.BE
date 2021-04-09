using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// SaaS版本包括的系统模块
    /// </summary>
    public class SaaSVersionOwnedModuleAddViewModel
    {
        /// <summary>
        /// SaaS版本标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "SaaS版本标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "角色标识不正确")]
        public string SaaSVersionId { get; set; }

        /// <summary>
        /// 模块|菜单集合
        /// </summary>
        public IList<SaaSVersionOwnedModules> SaaSVersionOwnedModules { get; set; } = new List<SaaSVersionOwnedModules>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class SaaSVersionOwnedModules
    {
        /// <summary>
        /// 模块|菜单标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "菜单标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "菜单标识不正确")]
        public string ModuleId { get; set; }
    }
}
