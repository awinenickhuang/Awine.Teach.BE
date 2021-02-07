using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 学生成长记录
    /// </summary>
    public class StudentGrowthRecord : Entity
    {
        /// <summary>
        /// 学生标识
        /// </summary>
        public string StudentId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 主题话题
        /// </summary>
        public string Topics { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ViewCount { get; set; } = 0;

        /// <summary>
        /// 租户标识 - 当发布者为用户时为关联机构（非必选）
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 创建者标识
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
