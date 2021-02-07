using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Awine.IdpCenter.Entities
{
    /// <summary>
    /// 企业用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// new Claim(JwtClaimTypes.Name, "Alice Smith")
        /// new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
        /// </summary>
        //public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>();

        /// <summary>
        /// 登录失败次数
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
        public string NormalizedUserName { get; set; }

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

        /// <summary>
        /// 角色信息
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// 租户信息
        /// </summary>
        public Tenant Tenant { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
