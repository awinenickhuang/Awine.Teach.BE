using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.WebSiteService.Configurations
{
    /// <summary>
    /// 上传文件返回结果
    /// </summary>
    public class LayeditUploadResult
    {
        /// <summary>
        /// 其他为失败
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 消息提示
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public Data Data { get; set; }
    }

    /// <summary>
    /// 数据
    /// </summary>
    public class Data
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string Src { get; set; }
    }
}
