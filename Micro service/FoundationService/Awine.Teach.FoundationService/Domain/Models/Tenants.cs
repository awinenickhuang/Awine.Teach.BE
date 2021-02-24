using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 租户信息
    /// </summary>
    public class Tenants : Entity
    {
        /// <summary>
        /// 父级租户标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 租户类型 1-免费 2-试用 3-付费（VIP）4-代理商 5-运营单位
        /// </summary>
        public int ClassiFication { get; set; } = 1;

        /// <summary>
        /// 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 省标识
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市标识
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区标识
        /// </summary>
        public string DistrictId { get; set; }

        /// <summary>
        /// 区名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// VIP过期时间
        /// </summary>
        public DateTime VIPExpirationTime { get; set; }

        /// <summary>
        /// 行业类型标识
        /// </summary>
        public string IndustryId { get; set; }

        /// <summary>
        /// 行业类型名称
        /// </summary>
        public string IndustryName { get; set; }

        /// <summary>
        /// 允许添加的分支机构个数
        /// </summary>
        public int NumberOfBranches { get; set; } = 0;
    }
}
