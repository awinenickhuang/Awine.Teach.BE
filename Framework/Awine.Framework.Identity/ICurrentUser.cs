using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Awine.Framework.Identity
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public interface ICurrentUser
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// UserId
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// RoleId
        /// </summary>
        string RoleId { get; }

        /// <summary>
        /// TenantId
        /// </summary>
        string TenantId { get; }

        /// <summary>
        /// TenantName
        /// </summary>
        string TenantName { get; }

        /// <summary>
        /// IsAuthenticated
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();

        /// <summary>
        /// 获取全部自定义 Claims 集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
