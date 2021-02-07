using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 咨询记录 -> 跟进记录 -> 视图模型
    /// </summary>
    public class CommunicationRecordViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; }

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
        public DateTime CreateTime { get; set; }
    }
}
