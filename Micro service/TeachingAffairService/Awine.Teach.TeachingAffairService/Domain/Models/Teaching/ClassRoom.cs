using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 教室管理
    /// </summary>
    public class ClassRoom : Entity
    {
        /// <summary>
        /// 教室名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 教室备注
        /// </summary>
        public string NoteContent { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public string DisplayOrder { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
