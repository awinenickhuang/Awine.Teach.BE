using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 机构设置
    /// </summary>
    public class TenantSettingsViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

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
        public int MaxStorageSpace { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
