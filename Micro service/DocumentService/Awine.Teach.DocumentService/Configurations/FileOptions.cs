using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.DocumentService.Configurations
{
    /// <summary>
    /// 文件上传配置
    /// </summary>
    public class AwineFileOptions
    {
        /// <summary>
        /// 允许的文件类型
        /// </summary>
        public string FileTypes { get; set; }

        /// <summary>
        /// 最大文件大小
        /// </summary>
        public int MaxSize { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string FileBaseUrl { get; set; }
    }
}
