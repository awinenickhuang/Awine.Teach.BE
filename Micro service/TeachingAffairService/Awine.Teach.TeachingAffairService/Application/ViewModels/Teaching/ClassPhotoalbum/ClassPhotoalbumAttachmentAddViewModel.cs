﻿using System;
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
        public string PhotoalbumId { get; set; }

        /// <summary>
        /// 图片文件名称
        /// </summary>
        public string AttachmentFileName { get; set; }

        /// <summary>
        /// 图片完整地址
        /// </summary>
        public string AttachmentFullUri { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 本次上传的图片大小
        /// </summary>
        public int AttachmentSize { get; set; }
    }
}
