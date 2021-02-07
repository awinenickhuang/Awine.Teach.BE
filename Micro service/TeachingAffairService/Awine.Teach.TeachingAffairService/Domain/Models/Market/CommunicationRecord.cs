using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 咨询记录 -> 跟进记录
    /// </summary>
    public class CommunicationRecord
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 咨询记录标识
        /// </summary>
        public string ConsultRecordId { get; set; }

        /// <summary>
        /// 沟通内容
        /// </summary>
        public string CommunicationContent { get; set; }

        /// <summary>
        /// 负责员工标识
        /// </summary>
        public string TrackingStafferId { get; set; }

        /// <summary>
        /// 负责员工姓名
        /// </summary>
        public string TrackingStafferName { get; set; }

        /// <summary>
        /// 成交意向星级
        /// </summary>
        public int ClinchIntentionStar { get; set; }

        /// <summary>
        /// 沟通方式
        /// </summary>
        public string CommunicateWay { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
