using System;

namespace IdentityServerHost.Quickstart.UI
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;

        public static bool AllowRememberLogin = false;

        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt = true;

        /// <summary>
        /// 退出时自动跳回应用
        /// </summary>
        public static bool AutomaticRedirectAfterSignOut = true;

        /// <summary>
        /// 无效的账号
        /// </summary>
        public static string AccountDoesnotExistErrorMessage = "无效的账号";

        /// <summary>
        /// 无效的账号或密码
        /// </summary>
        public static string InvalidCredentialsErrorMessage = "无效的账号或密码";

        /// <summary>
        /// 账号被锁定
        /// </summary>
        public static string AccountHasBeenLockedErrorMessage = "账号被锁定";

        /// <summary>
        /// 账号被禁用
        /// </summary>
        public static string AccountHasBeenDisableErrorMessage = "账号被禁用";

        /// <summary>
        /// 账号无机构信息
        /// </summary>
        public static string AccountWithoutTenantErrorMessage = "账号无机构信息";

        /// <summary>
        /// 账号被禁用
        /// </summary>
        public static string AccountTenantStateAnomalyErrorMessage = "机构状态异常";
    }
}
