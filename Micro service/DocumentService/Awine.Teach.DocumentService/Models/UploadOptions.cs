using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.DocumentService.Models
{
    /// <summary>
    /// 文件上传设置
    /// </summary>
    public class UploadOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public long MaxLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> SupportedExtensions { get; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public bool IsOverrideEnabled { get; set; }
    }

    /// <summary>
    /// Cos上传设置
    /// </summary>
    public class CosUploadOptions : UploadOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string CosStorageUri { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FileUploadOptions : UploadOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string FileStoragePath { get; set; }
    }
}
