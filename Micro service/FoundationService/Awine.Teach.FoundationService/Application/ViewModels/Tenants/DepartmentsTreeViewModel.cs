﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 部部树型数据模型 -> 适用于 Layui Tree 数据结构
    /// </summary>
    public class DepartmentsTreeViewModel
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
        public string Title { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 节点是否初始展开，默认 false
        /// </summary>
        public bool Spread { get; set; } = true;

        /// <summary>
        /// 叶子节点
        /// </summary>
        public IList<DepartmentsTreeViewModel> Children { get; set; }
    }
}
