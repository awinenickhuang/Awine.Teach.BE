using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Domain
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student : Entity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别 1-男 2-女
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 学生年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string NoteInformation { get; set; }

        /// <summary>
        /// 学习进度 1-已报名（未分班） 2-已报名（已分班）3-停课 4-退费 5-毕业
        /// </summary>
        public int LearningProcess { get; set; } = 1;

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
