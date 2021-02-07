using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.DingTalk;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DingTalkExtensions
    {
        public static AuthenticationBuilder AddDingTalk(this AuthenticationBuilder builder)
            => builder.AddDingTalk(DingTalkDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddDingTalk(this AuthenticationBuilder builder, Action<DingTalkOptions> configureOptions)
            => builder.AddDingTalk(DingTalkDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddDingTalk(this AuthenticationBuilder builder, string authenticationScheme, Action<DingTalkOptions> configureOptions)
            => builder.AddDingTalk(authenticationScheme, DingTalkDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddDingTalk(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<DingTalkOptions> configureOptions)
            => builder.AddOAuth<DingTalkOptions, DingTalkHandler>(authenticationScheme, displayName, configureOptions);
    }
}
