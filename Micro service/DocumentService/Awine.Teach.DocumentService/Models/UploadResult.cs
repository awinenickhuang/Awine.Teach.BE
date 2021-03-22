using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.DocumentService.Models
{
    /// <summary>
    /// 上传结果
    /// </summary>
    public class UploadResult
    {
        /// <summary>
        /// 文件存储路径
        /// </summary>
        public string UploadedUri { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小 单位:MB
        /// </summary>
        public int FileSize { get; set; }
    }
}
