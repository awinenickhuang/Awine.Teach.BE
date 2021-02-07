using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 允许添加的分支机构个数
    /// </summary>
    public class TenantsUpdateNumberOfBranchesViewModel
    {
        /// <summary>
        /// 租户标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 允许添加的分支机构个数
        /// </summary>
        public int NumberOfBranches { get; set; }
    }
}
