﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.ExtensionGrants.SMS
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidationComponent
    {
        /// <summary>
        /// 
        /// </summary>
        Task<bool> CheckOverLimit(string phone);
        /// <summary>
        /// 
        /// </summary>
        Task<ActionObjectResult<bool>> ValidSmsAsync(string phone, string code);

        /// <summary>
        /// 
        /// </summary>
        Task ClearSession(string phone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int GetSmsType(string type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <param name="phone"></param>
        /// <param name="ip"></param>
        /// <param name="isSuccess"></param>
        /// <param name="code"></param>
        /// <param name="expiresMinute"></param>
        /// <param name="smsMsgId"></param>
        /// <param name="smsMsg"></param>
        /// <returns></returns>
        Task SaveLog(int appId, string content, int type, string phone, string ip,
            bool isSuccess, string code, int expiresMinute, string smsMsgId, string smsMsg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expiresMinute"></param>
        /// <returns></returns>
        SmsContent GetSmsContent(int type, int expiresMinute = 5);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="phone"></param>
        /// <param name="type"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task<ActionObjectResult<bool>> SendAsync(int appId, string phone, int type, string ip);

        /// <summary>
        /// 发送内容信息
        /// </summary>
        Task<ActionObjectResult<bool>> SendAsync(string phone, string code, int appId, string content, int type, string ip, int expiresMinute = 5);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<string> CreateTicketAsync(string phone);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        Task<string> GetTicketPhoneAsync(string ticket);

    }
}
