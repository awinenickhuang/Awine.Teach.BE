using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 咨询记录
    /// </summary>
    public class ConsultRecord : Entity
    {
        /// <summary>
        /// 咨询人
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 学生年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 咨询课程标识
        /// </summary>
        public string CounselingCourseId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 咨询课程
        /// </summary>
        public string CounselingCourseName { get; set; } = "";

        /// <summary>
        /// 客户基本情况
        /// </summary>
        public string BasicSituation { get; set; }

        /// <summary>
        /// 跟进状态 1-待跟进 2-跟进中 3-已邀约 4-已试听 5-已到访 6-已成交
        /// </summary>
        public int TrackingState { get; set; } = 1;

        /// <summary>
        /// 负责员工标识
        /// </summary>
        public string TrackingStafferId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 负责员工姓名
        /// </summary>
        public string TrackingStafferName { get; set; } = "待指派";

        /// <summary>
        /// 营销渠道ID
        /// </summary>
        public string MarketingChannelId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 营销渠道名称
        /// </summary>
        public string MarketingChannelName { get; set; } = "";

        /// <summary>
        /// 成交意向星级
        /// </summary>
        public int ClinchIntentionStar { get; set; }

        /// <summary>
        /// 操作人标识
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
