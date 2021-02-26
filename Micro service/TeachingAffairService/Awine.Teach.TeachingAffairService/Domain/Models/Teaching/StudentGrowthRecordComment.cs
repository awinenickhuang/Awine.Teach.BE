using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 学生成长记录评论
    /// </summary>
    public class StudentGrowthRecordComment : Entity
    {
        /// <summary>
        /// 学生成长记录标识
        /// </summary>
        public string StudentGrowthRecordId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 创建者标识
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatorName { get; set; }

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
