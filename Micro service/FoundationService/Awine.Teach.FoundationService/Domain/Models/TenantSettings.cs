using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 机构设置
    /// </summary>
    public class TenantSettings
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 允许添加的分支机构个数
        /// </summary>
        public int NumberOfBranches { get; set; }

        /// <summary>
        /// 最大用户数量
        /// </summary>
        public int MaxNumberOfUser { get; set; }

        /// <summary>
        /// 最大课程数量
        /// </summary>
        public int MaxNumberOfCourse { get; set; }

        /// <summary>
        /// 最大班级数量
        /// </summary>
        public int MaxNumberOfClass { get; set; }

        /// <summary>
        /// 最大学生数量
        /// </summary>
        public int MaxNumberOfStudent { get; set; }

        /// <summary>
        /// 最大存储空间
        /// </summary>
        public long MaxStorageSpace { get; set; }

        /// <summary>
        /// 可用存储空间
        /// </summary>
        public long AvailableStorageSpace { get; set; }

        /// <summary>
        /// 已用存储空间
        /// </summary>
        public long UsedStorageSpace { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
