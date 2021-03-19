using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 应用版本对应的系统模块
    /// </summary>
    public class ApplicationVersionOwnedModuleAddViewModel
    {
        /// <summary>
        /// 应用版本标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "应用版本标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "角色标识不正确")]
        public string AppVersionId { get; set; }

        /// <summary>
        /// 模块|菜单集合
        /// </summary>
        public IList<AppVersionOwnedModules> AppVersionOwnedModules { get; set; } = new List<AppVersionOwnedModules>();
    }

    /// <summary>
    /// 
    /// </summary>
    public class AppVersionOwnedModules
    {
        /// <summary>
        /// 模块|菜单标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "菜单标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "菜单标识不正确")]
        public string ModuleId { get; set; }
    }
}
