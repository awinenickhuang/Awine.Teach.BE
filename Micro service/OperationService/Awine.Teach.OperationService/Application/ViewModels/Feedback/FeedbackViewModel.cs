using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.OperationService.Application.ViewModels
{
    /// <summary>
    /// 反馈信息 -> 视图模型
    /// </summary>
    public class FeedbackViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 反馈信息
        /// </summary>
        public string FeedbackMessage { get; set; }

        /// <summary>
        /// 回复信息
        /// </summary>
        public string ReplyMessage { get; set; }

        /// <summary>
        /// 是否公开显示
        /// </summary>
        public bool PublicDisplay { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
