using Awine.IdpCenter.Entities;
using Awine.IdpCenter.IRepositories;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Awine.IdpCenter.Services
{
    /// <summary>
    /// 添加IProfileService用于连接到自定义用户配置文件存储的实现。DefaultProfileService类提供基于认证的cookie，其依赖作为用于令牌发行的唯一权利要求的源的默认实现。
    /// 装载用户身份单元资源
    /// 在用户登录的时候，在请求中添加用户的一些额外信息
    /// </summary>
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// IUserRepository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ProfileService> _logger;

        /// <summary>
        /// The constructor injects the required resources
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        public ProfileService(IUserRepository userRepository, ILogger<ProfileService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// 预期会为用户加载声明的API。它传递了的实例ProfileDataRequestContext。
        /// </summary>/// <param name="context">The context.</param>
        /// <returns></returns>
        /// <remarks>
        /// 只要有关用户的身份信息单元被请求（例如在令牌创建期间或通过用户信息终点），就会调用此方法，根据请求需要的Claim类型，来为我们装载信息。
        /// </remarks>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            _logger.LogDebug("Starting [ProfileService].[GetProfileDataAsync] Method.");

            //判断是否有请求Claim信息
            if (context.RequestedClaimTypes.Any())
            {
                var user = await _userRepository.GetBySubjectId(context.Subject.GetSubjectId());
                if (user != null)
                {
                    var claims = context.Subject.Claims.ToList();
                    claims.Add(new System.Security.Claims.Claim("account", user.Account));
                    //将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim
                    if (!string.IsNullOrEmpty(user.Id))
                    {
                        claims.Add(new Claim("id", user.Id));
                    }
                    if (!string.IsNullOrEmpty(user.UserName))
                    {
                        claims.Add(new Claim("username", user?.UserName));
                    }
                    if (null != user.Role)
                    {
                        claims.Add(new Claim("roleid", user.Role.Id));
                        claims.Add(new Claim("rolename", user.Role.Name));
                    }
                    if (null != user.Tenant)
                    {
                        claims.Add(new Claim("tenantid", user.Tenant.Id));
                        claims.Add(new Claim("tenantname", user.Tenant.Name));
                        claims.Add(new Claim("tenantclassification", user.Tenant.ClassiFication.ToString()));
                    }

                    // context.IssuedClaims = User.Claims，那么所有Claim都将被返回，而不会根据请求的Claim来进行筛选
                    context.IssuedClaims = claims;
                }
            }
            //return Task.CompletedTask;
        }

        /// <summary>
        /// 预期指示当前是否允许用户获得令牌的API。它传递了的实例IsActiveContext。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userRepository.GetBySubjectId(context.Subject.GetSubjectId()).Result;
            context.IsActive = (user != null) && user.IsActive;
            await Task.CompletedTask;
        }
    }
}
