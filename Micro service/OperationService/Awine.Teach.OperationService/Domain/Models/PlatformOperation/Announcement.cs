using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.OperationService.Domain
{
    /// <summary>
    /// 平台公告
    /// </summary>
    public class Announcement : Entity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息类型 1-公告（全部可见） 2-私信（租户可见）
        /// </summary>
        public int AnnouncementType { get; set; } = 1;

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; } = Guid.Empty.ToString();
    }
}
