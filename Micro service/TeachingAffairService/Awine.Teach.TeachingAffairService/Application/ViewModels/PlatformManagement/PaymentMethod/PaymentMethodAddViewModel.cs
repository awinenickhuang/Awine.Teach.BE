using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 收款方式 -> 添加 -> 视图模型
    /// </summary>
    public class PaymentMethodAddViewModel
    {
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
    }
}
