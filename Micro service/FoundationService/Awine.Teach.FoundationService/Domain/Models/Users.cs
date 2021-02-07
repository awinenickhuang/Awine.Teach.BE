using Awine.Framework.Core.DomainInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Domain.Models
{
    /// <summary>
    /// 系统账号
    /// </summary>
    public class Users : Entity
    {
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 记录用户尝试登陆却登陆失败的次数
        /// </summary>
        public int AccessFailedCount { get; set; } = 0;

        /// <summary>
        /// 同步标记-每当用户记录被更改时必须要更改此列的值
        /// </summary>
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱是否验证
        /// </summary>
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// 指示这个用户可不可以被锁定
        /// </summary>
        public bool LockoutEnabled { get; set; } = true;

        /// <summary>
        /// 指定锁定的到期日期，null 或者一个过去的时间，代表这个用户没有被锁定
        /// </summary>
        public DateTime LockoutEnd { get; set; } = DateTime.Now;

        /// <summary>
        /// 规范化后的电子邮件
        /// </summary>
        public string NormalizedEmail { get; set; } = string.Empty;

        /// <summary>
        /// 标准化的用户名
        /// </summary>
        public string NormalizedUserName { get; set; } = string.Empty;

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机号是否验证
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; } = false;

        /// <summary>
        /// 安全标识
        /// </summary>
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 指示当前用户是否开启了双因子验证
        /// </summary>
        public bool TwoFactorEnabled { get; set; } = true;

        /// <summary>
        /// 性别 1-男 2-女
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 部门标识
        /// </summary>
        public string DepartmentId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        #region  Association Objects

        /// <summary>
        /// 租户信息
        /// </summary>
        public Tenants PlatformTenant { get; set; }

        /// <summary>
        /// 角色信息
        /// </summary>
        public Roles AspnetRole { get; set; }

        /// <summary>
        /// 部门信息
        /// </summary>
        public Departments Department { get; set; }

        #endregion
    }
}
