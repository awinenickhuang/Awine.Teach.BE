using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 班级相册
    /// </summary>
    public class ClassPhotoalbum : Entity
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 相册名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverPhoto { get; set; }

        /// <summary>
        /// 相册描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 可见范围 1-仅机构可见 2-机构及学生可见 3-完全公开
        /// </summary>
        public int VisibleRange { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
