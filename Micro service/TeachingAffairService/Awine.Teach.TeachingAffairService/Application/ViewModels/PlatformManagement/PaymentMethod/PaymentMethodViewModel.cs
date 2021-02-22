using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 收款方式 -> 视图模型
    /// </summary>
    public class PaymentMethodViewModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 收款方式
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string NoteInformation { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
