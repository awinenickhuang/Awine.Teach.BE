namespace Microsoft.AspNetCore.Authentication.WeChat
{
    public static class WeChatDefaults
    {
        /// <summary>
        /// 
        /// </summary>
        public const string AuthenticationScheme = "WeChat";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string DisplayName = "微信登录";

        /// <summary>
        /// 第一步，获取授权临时票据（code）地址，适用于微信客户端外的网页登录
        /// </summary>
        public static readonly string AuthorizationEndpoint = "https://open.weixin.qq.com/connect/qrconnect";

        /// <summary>
        /// 第一步，获取授权临时票据（code）地址，适用于微信客户端内的网页登录（在微信内部访问登录）
        /// </summary>
        public static readonly string AuthorizationEndpoint2 = "https://open.weixin.qq.com/connect/oauth2/authorize";

        /// <summary>
        /// 第二步，用户允许授权后，通过返回的 code 换取 access_token 地址
        /// </summary>
        public static readonly string TokenEndpoint = "https://api.weixin.qq.com/sns/oauth2/access_token";

        /// <summary>
        /// 第三步，使用 access_token 获取用户个人信息地址
        /// </summary>
        public static readonly string UserInformationEndpoint = "https://api.weixin.qq.com/sns/userinfo";
    }
}