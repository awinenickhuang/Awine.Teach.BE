using System;

namespace Microsoft.AspNetCore.Authentication.DingTalk
{
    /// <summary>
    /// Default values for Google authentication
    /// </summary>
    public static class DingTalkDefaults
    {
        public const string AuthenticationScheme = "DingTalk";

        public static readonly string DisplayName = "¶¤¶¤µÇÂ¼";

        public static readonly string AuthorizationEndpoint = "https://oapi.dingtalk.com/connect/qrconnect";

        public static readonly string TokenEndpoint = "https://oapi.dingtalk.com/sns/gettoken";

        public static readonly string UserInformationEndpoint = "https://oapi.dingtalk.com/sns/getuserinfo_bycode";
    }
}
