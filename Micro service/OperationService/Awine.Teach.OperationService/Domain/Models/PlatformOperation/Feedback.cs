using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.OperationService.Domain
{
    /// <summary>
    /// 反馈信息
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 反馈信息
        /// </summary>
        public string FeedbackMessage { get; set; }

        /// <summary>
        /// 回复信息
        /// </summary>
        public string ReplyMessage { get; set; } = "";

        /// <summary>
        /// 是否公开显示
        /// </summary>
        public bool PublicDisplay { get; set; } = false;

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
