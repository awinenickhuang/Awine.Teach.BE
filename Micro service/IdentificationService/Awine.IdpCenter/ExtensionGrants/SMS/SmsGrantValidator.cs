using Awine.IdpCenter.ExtensionGrants.SMS;
using Awine.IdpCenter.IRepositories;
using Awine.IdpCenter.Regular;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Awine.IdpCenter.ExtensionGrants
{
    /// <summary>
    /// 扩展手机短信验证码登录
    /// </summary>
    public class SmsGrantValidator : IExtensionGrantValidator
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly IValidationComponent _validationComponent;

        private readonly IUserRepository _userRepository;

        public string GrantType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="userRepository"></param>
        public SmsGrantValidator(IHttpContextAccessor contextAccessor, IValidationComponent validationComponent, IUserRepository userRepository)
        {
            _contextAccessor = contextAccessor;

            _validationComponent = validationComponent;

            _userRepository = userRepository;

            GrantType = CustomGrantType.Sms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var phone = context.Request.Raw.Get("phone");

            var code = context.Request.Raw.Get("code");

            if (string.IsNullOrEmpty(phone) || Regex.IsMatch(phone, RegExp.PhoneNumber) == false)
            {

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "phone is not valid");

                return;

            }

            if (string.IsNullOrEmpty(code))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "code is not valid");

                return;
            }

            try
            {

                var validSms = await _validationComponent.ValidSmsAsync(phone, code);

                if (!validSms.Data)
                {

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, validSms.Message);

                    return;
                }

                var userEntity = await _userRepository.GetUserByPhoneAsync(phone);

                if (userEntity == null)
                {

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "用户不存在或未注册");

                    return;

                }

                if (userEntity.IsActive == false)
                {

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "您的账号已被禁止登录");

                    return;

                }

                //Query GetLoginClaims
            }
            catch (Exception ex)
            {

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, ex.Message);
            }
        }
    }
}
