using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 应用版本 -> 添加视图
    /// </summary>
    public class ApplicationVersionAddViewModel
    {
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
    }
}
