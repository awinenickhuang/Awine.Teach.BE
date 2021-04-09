using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 机构信息设置 -> 添加
    /// </summary>
    public class TenantDefaultSettingsAddViewModel
    {
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
    }
}
