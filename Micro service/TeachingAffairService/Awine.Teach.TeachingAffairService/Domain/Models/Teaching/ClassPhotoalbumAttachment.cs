using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 班级相册 -> 附件
    /// </summary>
    public class ClassPhotoalbumAttachment : Entity
    {
        /// <summary>
        /// 相册ID
        /// </summary>
        public string photoalbumId { get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        public string AttachmentUri { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public string IsDeleted { get; set; }
    }
}
