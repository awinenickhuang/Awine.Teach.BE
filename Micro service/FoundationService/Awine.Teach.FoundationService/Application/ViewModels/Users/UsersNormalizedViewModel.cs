using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 系统账号 -> 所有属性 -> 视图模型
    /// </summary>
    public class UsersNormalizedViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 记录用户尝试登陆却登陆失败的次数
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 同步标记-每当用户记录被更改时必须要更改此列的值
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 邮箱是否验证
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 指示这个用户可不可以被锁定
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 指定锁定的到期日期，null 或者一个过去的时间，代表这个用户没有被锁定
        /// </summary>
        public DateTime LockoutEnd { get; set; }

        /// <summary>
        /// 规范化后的电子邮件
        /// </summary>
        public string NormalizedEmail { get; set; }

        /// <summary>
        /// 标准化的用户名
        /// </summary>
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

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
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 安全标识
        /// </summary>
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 指示当前用户是否开启了双因子验证
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// 性别 1-男 2-女
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 部门标识
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
