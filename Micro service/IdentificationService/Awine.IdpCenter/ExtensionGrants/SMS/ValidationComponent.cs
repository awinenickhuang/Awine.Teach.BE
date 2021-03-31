using Awine.Framework.Core.SMSVerificationCode;
using Awine.IdpCenter.Entities;
using Awine.IdpCenter.IRepositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.ExtensionGrants.SMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationComponent //: IValidationComponent
    {
        private readonly SmsOptions _smsServerOptions;

        //private readonly IPassportClient _passportClient;

        //private readonly IUnitOfWork _unitOfWork;

        private readonly ISmsRecordRepository _smsRecordRepository;

        //private readonly IBackgroundRunService _backgroundRunService;

        //private readonly AppSettings _appSettings;

        /*
        /// <summary>
        /// 
        /// </summary>
        public ValidationComponent(
            IOptions<SmsOptions> smsOptions,
            IPassportClient passportClient,
            ISmsRecordRepository smsRecordRepository,
            IUnitOfWork unitOfWork,
            IBackgroundRunService backgroundRunService,
            AppSettings appSettings)
        {
            _passportClient = passportClient;
            _smsRecordRepository = smsRecordRepository;
            _unitOfWork = unitOfWork;
            _backgroundRunService = backgroundRunService;
            _appSettings = appSettings;
            _smsServerOptions = smsOptions.Value;
        }

        private static string GetSmsKey(string phone)
        {
            return $"sms_{phone}";
        }

        private static string GetTicketKey(string ticket)
        {
            return $"ticket:{ticket}";
        }

        /// <summary>
        /// 记录短信发送日志
        /// </summary>
        public async Task SaveLog(int appId, string content, ValidationType type, string phone, string ip,
            bool isSuccess, string code, int expiresMinute, string smsMsgId, string smsMsg)
        {
            var smsRecordEntity = new SmsRecord
            {
                SendTime = DateTime.Now,
                ClientId = appId,
                Content = content,
                SendType = type.ToString(),
                Receiver = phone,
                IpAddress = ip,
                SendResult = isSuccess,
                Remark = isSuccess ? $"发送成功，msg_id：{smsMsgId}" : $"发送失败，错误描述：{smsMsg}，msg_id：{smsMsgId}"
            };
            await _smsRecordRepository.InsertAsync(smsRecordEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<ActionObjectResult<bool>> ValidSmsAsync(string phone, string code)
        {
            var smsCode = await _smsRecordRepository.GetAsync(phone);

            if (smsCode == null)
            {
                return ActionObject.Ok(false, -1, "未发送验证码或验证码已超时");
            }

            if (smsCode.ValidateCount >= 3)  //0 1  2
            {
                return ActionObject.Ok(false, -1, "输入错误的次数超过3次，请重新获取验证码");
            }

            if (code.Equals(smsCode.Code))
            {
                //让验证码过期
                return ActionObject.Ok(true);
            }

            var timeSpan = smsCode.SendTime.AddMinutes(_smsServerOptions.ExpiresMinute) - DateTime.Now;

            if (timeSpan.TotalSeconds <= 0)
            {
                return ActionObject.Ok(false, -1, "未发送验证码或验证码已超时");
            }

            smsCache.ValidateCount += 1;   //更新验证次数，不延长时间

            await _redisCache.AddAsync(uniqueKey, smsCache, timeSpan);

            return ActionObject.Ok(false, -1, "输入验证码不正确");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ValidationType GetSmsType(string type)
        {
            if (!Enum.TryParse(type, true, out ValidationType sendType))
            {
                sendType = ValidationType.UnKnow;
            }
            return sendType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expiresMinute"></param>
        /// <returns></returns>
        public SmsContent GetSmsContent(ValidationType type, int expiresMinute = 5)
        {
            var code = new Random(Guid.NewGuid().GetHashCode()).Next(100000, 999999);

            if (_appSettings.DeveloperMode)
                code = 666666;

            string content;

            switch (type)
            {
                case ValidationType.Register:
                    content = $"您正在使用手机号注册福禄通行证账号，验证码：{code}，{expiresMinute}分钟内有效。如非本人操作，请忽略本短信。";
                    break;
                case ValidationType.ChangePhoneNo:
                    content = $"您正在修改手机号，验证码：{code}，{expiresMinute}分钟内有效。请勿将验证码泄露给他人使用。";
                    break;
                case ValidationType.ResetPassword:
                    content = $"您正在使用手机号进行修改密码操作，验证码：{code}，{expiresMinute}分钟内有效。验证码提供给让人可能导致账号被盗，请勿泄露，谨防被骗。";
                    break;
                case ValidationType.Login:
                    content = $"您正在使用短信验证码登录功能，验证码：{code}，{expiresMinute}分钟内有效。请勿将验证码泄露给他人使用。";
                    break;
                case ValidationType.Validate:
                    expiresMinute = 15;
                    content = $"您正在使用手机验证操作，验证码：{code}，{expiresMinute}分钟内有效。如非本人操作，请忽略本短信。";
                    break;
                default:
                    content = $"验证码：{code}，{expiresMinute}分钟内有效。请勿将验证码泄露给他人使用。";
                    break;
            }

            return new SmsContent
            {
                Code = code.ToString(),
                Content = content,
                ExpiresMinute = expiresMinute,
                ValidationType = type
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="phone"></param>
        /// <param name="type"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<ActionObjectResult<bool>> SendAsync(int appId, string phone, ValidationType type, string ip)
        {
            var smsModel = GetSmsContent(type);

            return await SendAsync(phone, smsModel.Code, appId, smsModel.Content, smsModel.ValidationType, ip, smsModel.ExpiresMinute);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<ActionObjectResult<bool>> SendAsync(string phone, string smsCode, int appId, string content, ValidationType type, string ip, int expiresMinute = 5)
        {
            var uniqueKey = GetSmsKey(phone);

            if (!await CheckOverLimit(phone))
                return ActionObject.Ok(false, -1, "请求频率过高，请稍后再试");

            var smsLimit = new SmsCache { Code = smsCode, StartTime = DateTime.Now, ValidateCount = 0, ExpiresMinute = expiresMinute };

            await _redisCache.AddAsync(uniqueKey, smsLimit, new TimeSpan(0, 0, expiresMinute * 60));

            if (!_appSettings.DeveloperMode)
            {
                var (code, msg, msgId) = await _passportClient.SendSms(phone, content, _smsServerOptions.SignatureCode);

                _backgroundRunService.Transfer<IValidationComponent>(x =>
                    x.SaveLog(appId, content, type, phone, ip, code == "0", smsCode, expiresMinute, msgId, msg));
            }

            return ActionObject.Ok(true);
        }*/
    }
}
