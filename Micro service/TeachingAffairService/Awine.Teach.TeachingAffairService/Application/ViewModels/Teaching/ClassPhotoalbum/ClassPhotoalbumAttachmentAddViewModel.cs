using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 班级相册 -> 相片添加视图模型
    /// </summary>
    public class ClassPhotoalbumAttachmentAddViewModel
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
    }
}
