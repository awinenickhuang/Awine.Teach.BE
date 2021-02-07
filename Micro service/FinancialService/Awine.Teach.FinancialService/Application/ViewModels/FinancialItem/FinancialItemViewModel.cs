using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FinancialService.Application.ViewModels
{
    /// <summary>
    /// 财务收支项目 -> 视图模型
    /// </summary>
    public class FinancialItemViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 启用状态 1-启用 2-停用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 建创时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
