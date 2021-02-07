using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Awine.IdpCenter.IRepositories;
using Awine.Framework.Core.Cryptography;
using Awine.IdpCenter.Tools;

namespace Awine.IdpCenter.Services
{
    /// <summary>
    /// 资源拥有者验证器（Resource Owner Validator）
    /// </summary>
    /// <remarks>
    /// 如果要使用OAuth 2.0 密码模式（Resource Owner Password Credentials Grant），则需要实现并注册IResourceOwnerPasswordValidator接口.
    /// </remarks>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// IUserRepository
        /// </summary>
        public IUserRepository _userRepository { get; private set; }

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ResourceOwnerPasswordValidator> _logger;

        /// <summary>
        /// The constructor injects the required resources
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        public ResourceOwnerPasswordValidator(IUserRepository userRepository, ILogger<ResourceOwnerPasswordValidator> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// ValidateAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            _logger.LogDebug("Starting [ResourceOwnerPasswordValidator] Method.");

            var user = await this._userRepository.GetByAccount(context.UserName);

            if (null == user)
            {
                _logger.LogDebug("[ResourceOwnerPasswordValidator] message: user no found in database,validate failed.");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "密码模式验证失败 -> 无效凭据");
            }

            if (user.LockoutEnabled && user.LockoutEnd > DateTime.Now)
            {
                _logger.LogDebug("[ResourceOwnerPasswordValidator] message: user account locked,validate failed.");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "密码模式验证失败 -> 账号被锁定");
            }

            if (null == user.Tenant)
            {
                _logger.LogDebug("[ResourceOwnerPasswordValidator] message: user account without tenant,validate failed.");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "密码模式验证失败 -> 账号无机构信息");
            }

            if (user.Tenant.Status != 1)
            {
                _logger.LogDebug("[ResourceOwnerPasswordValidator] message: user account tenant state anomaly,validate failed.");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "密码模式验证失败 -> 机构状态异常");
            }

            if (user.Tenant.ClassiFication != 1 && DateTime.Now > user.Tenant.VIPExpirationTime)
            {
                _logger.LogDebug("[ResourceOwnerPasswordValidator] message: user account tenant VIP expired,validate failed.");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, $"密码模式验证失败 -> 机构的使用权益已过期，过期时间：{user.Tenant.VIPExpirationTime.ToString("yyyy-MM-dd")}");
            }

            var verifyHashedPasswordResult = PasswordManager.VerifyHashedPassword(user.PasswordHash, context.Password);
            if (!verifyHashedPasswordResult)
            {
                _logger.LogDebug("[ResourceOwnerPasswordValidator] message: the password is invalid,validate failed.");
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "密码模式验证失败 -> 密码错误");
            }

            var claims = ClaimsUtility.GetClaims(user);
            context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password, claims);
        }
    }
}
