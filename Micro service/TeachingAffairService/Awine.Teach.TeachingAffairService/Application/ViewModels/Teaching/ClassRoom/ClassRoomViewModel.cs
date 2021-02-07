using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 教室管理 -> 视图模型
    /// </summary>
    public class ClassRoomViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 教室名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 教室备注
        /// </summary>
        public string NoteContent { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public string DisplayOrder { get; set; }

        /// <summary>
        /// 建造时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
