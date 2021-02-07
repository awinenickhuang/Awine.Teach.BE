using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Core.Models
{
    /// <summary>
    /// 用户登录日志
    /// </summary>
    public class LogOnLog
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserName { get; set; }

        public string Account { get; set; }

        public string LogonIPAddress { get; set; }

        public string TenantName { get; set; }

        public string TenantId { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
