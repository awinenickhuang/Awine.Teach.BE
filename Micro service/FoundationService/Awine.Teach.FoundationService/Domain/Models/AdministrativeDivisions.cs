using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 行政区域划分 -> 公共数据
    /// </summary>
    public class AdministrativeDivisions : Entity
    {
        /// <summary>
        /// 编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级编码
        /// </summary>
        public int ParentCode { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
    }
}
