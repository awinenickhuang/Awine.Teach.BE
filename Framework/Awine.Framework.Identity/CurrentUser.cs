using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Awine.Framework.Identity
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string UserName => GetUserName();

        private string GetUserName()
        {
            //return _accessor.HttpContext.User.Identity.Name ??
            //       _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
        }

        public string UserId => GetUserId();

        private string GetUserId()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        }

        public string RoleId => GetRoleId();

        private string GetRoleId()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "roleid")?.Value;
        }

        public string TenantId => GetTenantId();

        private string GetTenantId()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "tenantid")?.Value;
        }

        public string TenantName => GetTenantName();

        private string GetTenantName()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "tenantname")?.Value;
        }

        public int TenantClassiFication => GetTenantClassiFication();

        private int GetTenantClassiFication()
        {
            return Convert.ToInt32(_accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "tenantclassification")?.Value);
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}
