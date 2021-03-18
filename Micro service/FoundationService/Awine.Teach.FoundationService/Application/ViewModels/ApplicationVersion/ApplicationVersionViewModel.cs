using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 应用版本 -> 视图
    /// </summary>
    public class ApplicationVersionViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 版本名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本标识
        /// </summary>
        public string Identifying { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
