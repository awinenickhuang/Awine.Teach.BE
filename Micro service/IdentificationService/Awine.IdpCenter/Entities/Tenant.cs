﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.IdpCenter.Entities
{
    /// <summary>
    /// Tenant info for the convenience of query, data redundancy is done appropriately
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
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
        /// 租户类型 1-机构 2-代理商 3-运营商
        /// </summary>
        public int ClassiFication { get; set; }

        /// <summary>
        /// 版本标识
        /// </summary>
        public string SaaSVersionId { get; set; }

        /// <summary>
        /// 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市ID
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区ID
        /// </summary>
        public string DistrictId { get; set; }

        /// <summary>
        /// 区名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 地址
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
        /// 创建人标识
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建人租户标识
        /// </summary>
        public string CreatorTenantId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
