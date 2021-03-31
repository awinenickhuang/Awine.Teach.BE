using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.Entities
{
    /// <summary>
    /// 短信发送记录
    /// </summary>
    public class SmsRecord
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [Column("client_id")]
        public int ClientId { get; set; }

        /// <summary>
        /// 发送类型
        /// </summary>
        [Column("send_type")]
        public string SendType { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [Column("send_time")]
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 发送结果
        /// </summary>
        [Column("send_result")]
        public bool SendResult { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        [Column("ip_address")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [Column("receiver")]
        public string Receiver { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }
    }
}
