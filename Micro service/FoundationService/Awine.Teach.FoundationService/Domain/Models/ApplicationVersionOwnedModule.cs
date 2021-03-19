using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 应用版本对应的系统模块
    /// </summary>
    public class ApplicationVersionOwnedModule
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 应用版本ID
        /// </summary>
        public string AppVersionId { get; set; }

        /// <summary>
        /// 模块|菜单标识
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
