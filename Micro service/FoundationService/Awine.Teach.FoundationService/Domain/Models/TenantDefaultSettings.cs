using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 机构信息设置 不同的SaaS版本对应不同的配置
    /// </summary>
    public class TenantDefaultSettings
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 最大分支机构数量
        /// </summary>
        public int MaxNumberOfBranch { get; set; }

        /// <summary>
        /// 最大部门数量
        /// </summary>
        public int MaxNumberOfDepartments { get; set; }

        /// <summary>
        /// 最大角色数量
        /// </summary>
        public int MaxNumberOfRoles { get; set; }

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
        /// 最大教室数量
        /// </summary>
        public int MaxNumberOfClassRoom { get; set; }

        /// <summary>
        /// 最大学生数量
        /// </summary>
        public int MaxNumberOfStudent { get; set; }

        /// <summary>
        /// 最大存储空间
        /// </summary>
        public long MaxStorageSpace { get; set; }

        /// <summary>
        /// SaaS版本标识
        /// </summary>
        public string SaaSVersionId { get; set; }

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
