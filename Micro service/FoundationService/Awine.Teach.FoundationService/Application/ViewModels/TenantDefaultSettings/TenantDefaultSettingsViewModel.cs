﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 机构信息设置 -> 不同的应用版本对应不同的配置
    /// </summary>
    public class TenantDefaultSettingsViewModel
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
        public int MaxStorageSpace { get; set; }

        /// <summary>
        /// 应用版本标识
        /// </summary>
        public string AppVersionId { get; set; }

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
