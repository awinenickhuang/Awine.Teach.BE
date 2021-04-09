using Awine.Framework.Core.Cryptography;
using Awine.Framework.Core.Models;
using Awine.IdpCenter.IRepositories;
using DotNetCore.CAP;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerHost.Quickstart.UI
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// 旨在提供用户界面用来与IdentityServer通信的服务，主要涉及用户交互。它可以从依赖项注入系统中获得，通常将作为构造函数参数注入到IdentityServer用户界面的MVC控制器中。
        /// </summary>
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly ICapPublisher _capBus;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(
            IUserRepository userRepository,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            ICapPublisher capBus,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _capBus = capBus;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // 根据backurl校验信息，并获取构建登录页面需要的信息，需要Idr4提供的交互接口 IIdentityServerInteractionService
            var vm = await BuildLoginViewModelAsync(returnUrl);

            // 仅扩展登录
            if (vm.IsExternalLoginOnly)
            {
                // 交互授权信息
                return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });
            }

            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            //校验授权相关信息
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            //点击取消 提供给第三方的登录取消的情况
            //PKCE(RFC7636)是授权码流程的扩展，以防止某些攻击，并能够安全地执行来自公共客户端的OAuth交换。
            if (button != "login")
            {
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    if (context.IsNativeClient())
                    {
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                // 验证用户名/密码
                var aspnetUser = await _userRepository.GetByAccount(model.Account);

                if (null == aspnetUser)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.InvalidCredentialsErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);

                    var v1 = await BuildLoginViewModelAsync(model);
                    return View(v1);
                }

                //验证用户的密码
                var verifyHashedPasswordResult = PasswordManager.VerifyHashedPassword(aspnetUser.PasswordHash, model.Password);
                if (!verifyHashedPasswordResult)
                {
                    //TO DO：记录登录失败次数，自动锁定账号
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.InvalidCredentialsErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);

                    var v3 = await BuildLoginViewModelAsync(model);
                    return View(v3);
                }

                if (!aspnetUser.IsActive)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.AccountHasBeenDisableErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, AccountOptions.AccountHasBeenDisableErrorMessage);

                    var v2 = await BuildLoginViewModelAsync(model);
                    return View(v2);
                }
                if (aspnetUser.LockoutEnabled && aspnetUser.LockoutEnd > DateTime.Now)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.AccountHasBeenLockedErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, $"{AccountOptions.AccountHasBeenLockedErrorMessage}，锁定结束时间：{aspnetUser.LockoutEnd.ToString("yyyy-MM-dd")}。");

                    var v2 = await BuildLoginViewModelAsync(model);
                    return View(v2);
                }
                if (null == aspnetUser.Tenant)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.AccountWithoutTenantErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, AccountOptions.AccountWithoutTenantErrorMessage);

                    var v2 = await BuildLoginViewModelAsync(model);
                    return View(v2);
                }
                if (aspnetUser.Tenant.Status != 1)
                {
                    string tenantStateMessage = "正常状态";
                    switch (aspnetUser.Tenant.Status)
                    {
                        case 2:
                            tenantStateMessage = "锁定（异常）";
                            break;
                        case 3:
                            tenantStateMessage = "锁定（过期）";
                            break;
                        default:
                            tenantStateMessage = "异常状态";
                            break;
                    }
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.AccountTenantStateAnomalyErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, $"{AccountOptions.AccountTenantStateAnomalyErrorMessage}，当前机构状态为：{tenantStateMessage}");

                    var v2 = await BuildLoginViewModelAsync(model);
                    return View(v2);
                }
                if (aspnetUser.Tenant.ClassiFication == 1 && DateTime.Now > aspnetUser.Tenant.VIPExpirationTime)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.AccountTenantStateAnomalyErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, $"机构的使用权益已过期，过期时间：{aspnetUser.Tenant.VIPExpirationTime.ToString("yyyy-MM-dd")}，请联系运营商或续费。");

                    var v2 = await BuildLoginViewModelAsync(model);
                    return View(v2);
                }

                await _events.RaiseAsync(new UserLoginSuccessEvent(aspnetUser.Account, aspnetUser.Id, aspnetUser.UserName, clientId: context?.Client.ClientId));

                // 只有在用户选择“记住我”时才显式设置过期。否则，我们依赖于cookie中间件中配置的过期。
                AuthenticationProperties props = null;
                if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                {
                    props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                    };
                };

                // 发出带有主题ID和用户名的认证cookie
                var isuser = new IdentityServerUser(aspnetUser.Id)
                {
                    DisplayName = aspnetUser.UserName
                };

                await HttpContext.SignInAsync(isuser, props);

                // 发出登录事件
                LogOnLog log = new LogOnLog
                {
                    UserName = aspnetUser.UserName,
                    Account = aspnetUser.Account,
                    TenantId = aspnetUser.Tenant.Id,
                    TenantName = aspnetUser.Tenant.Name,
                    LogonIPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                };

                _capBus.Publish("acc.cdzssy.userlogon", log);

                if (context != null)
                {
                    if (context.IsNativeClient())
                    {
                        // 客户端是本地的，所以这种变化在怎么来 返回响应是为了最终用户获得更好的UX。
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    // 我们可以信任模型。ReturnUrl因为GetAuthorizationContextAsync返回非空
                    return Redirect(model.ReturnUrl);
                }

                // 请求本地页面
                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    // 用户可能点击了一个恶意链接-应该被记录
                    throw new Exception("非法地址信息");
                }
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return View("LoggedOut", vm);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Private Methods

        /*****************************************/
        /* Private Methods */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            //存在外部授权方案 利用授权策略提供服务获取相关信息 authcontext?.IdP 相关的 scheme 的策略方案类型 如：cookies
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                //判断是不是idr4提供外部登录，根据backurl只能触发一个外部扩展登录
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // 这意味着短路的UI和只触发一个外部IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Account = context?.LoginHint,
                };

                //不是Idr4提供的
                if (!local)
                {
                    //显示扩展登录 构造相关模型
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            //不是Idp扩展登录，获取所有的授权策略信息 获取中间件授权中的信息 然后展示到登录界面上 供用户选择提供的第三方扩展登录
            var schemes = await _schemeProvider.GetAllSchemesAsync();

            //登录的策略 或者 DisplayName不是空的策略信息
            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;

            //判断客户端信息
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Account = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Account = model.Account;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }

        #endregion
    }
}
