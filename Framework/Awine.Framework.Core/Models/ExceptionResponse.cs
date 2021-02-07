using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Core.Models
{
    /// <summary>
    /// 格式化全局异常返回信息 -> 保持和正常请求返回格式统一
    /// </summary>
    public class ExceptionResponse
    {
        public int StatusCode { get; set; } = 500;

        public bool Success { get; set; } = false;

        public string Message { get; set; } = "请求中出现错误^~^正在努力修复中...";

        public object DeveloperMessage { get; set; }
    }
}
