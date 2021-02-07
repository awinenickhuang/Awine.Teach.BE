namespace IdentityServerHost.Quickstart.UI
{
    public class ExternalProvider
    {
        /// <summary>
        /// 显示第三方扩展登录的名称 比如 QQ  微信 等
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 扩展授权的策略  类似 QQ  需要一个策略 以及一个显示的名称
        /// </summary>
        public string AuthenticationScheme { get; set; }
    }
}