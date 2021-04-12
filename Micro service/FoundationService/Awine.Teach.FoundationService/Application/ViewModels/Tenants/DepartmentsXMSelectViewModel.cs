using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 适用于 Layui XMSelect 数据结构
    /// </summary>
    public class DepartmentsXMSelectViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父级部门
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否选中 -> XMSelect
        /// </summary>
        public bool Selected { get; set; } = false;

        /// <summary>
        /// 叶子节点
        /// </summary>
        public IList<DepartmentsXMSelectViewModel> Children { get; set; }
    }
}
