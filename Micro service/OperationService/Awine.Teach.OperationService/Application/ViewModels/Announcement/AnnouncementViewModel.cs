using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.OperationService.Application.ViewModels
{
    /// <summary>
    /// 平台公告 -> 视图模型
    /// </summary>
    public class AnnouncementViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 建创时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
