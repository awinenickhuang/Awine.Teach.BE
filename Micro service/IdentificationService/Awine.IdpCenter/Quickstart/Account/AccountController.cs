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
        /// ּ���ṩ�û�����������IdentityServerͨ�ŵķ�����Ҫ�漰�û������������Դ�������ע��ϵͳ�л�ã�ͨ������Ϊ���캯������ע�뵽IdentityServer�û������MVC�������С�
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
            // ����backurlУ����Ϣ������ȡ������¼ҳ����Ҫ����Ϣ����ҪIdr4�ṩ�Ľ����ӿ� IIdentityServerInteractionService
            var vm = await BuildLoginViewModelAsync(returnUrl);

            // ����չ��¼
            if (vm.IsExternalLoginOnly)
            {
                // ������Ȩ��Ϣ
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
            //У����Ȩ�����Ϣ
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            //���ȡ�� �ṩ���������ĵ�¼ȡ�������
            //PKCE(RFC7636)����Ȩ�����̵���չ���Է�ֹĳЩ���������ܹ���ȫ��ִ�����Թ����ͻ��˵�OAuth������
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
                // ��֤�û���/����
                var aspnetUser = await _userRepository.GetByAccount(model.Account);

                if (null == aspnetUser)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.InvalidCredentialsErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);

                    var v1 = await BuildLoginViewModelAsync(model);
                    return View(v1);
                }

                //��֤�û�������
                var verifyHashedPasswordResult = PasswordManager.VerifyHashedPassword(aspnetUser.PasswordHash, model.Password);
                if (!verifyHashedPasswordResult)
                {
                    //TO DO����¼��¼ʧ�ܴ������Զ������˺�
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
                    ModelState.AddModelError(string.Empty, $"{AccountOptions.AccountHasBeenLockedErrorMessage}����������ʱ�䣺{aspnetUser.LockoutEnd.ToString("yyyy-MM-dd")}��");

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
                    string tenantStateMessage = "����״̬";
                    switch (aspnetUser.Tenant.Status)
                    {
                        case 2:
                            tenantStateMessage = "�������쳣��";
                            break;
                        case 3:
                            tenantStateMessage = "���������ڣ�";
                            break;
                        default:
                            tenantStateMessage = "�쳣״̬";
                            break;
                    }
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.AccountTenantStateAnomalyErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, $"{AccountOptions.AccountTenantStateAnomalyErrorMessage}����ǰ����״̬Ϊ��{tenantStateMessage}");

                    var v2 = await BuildLoginViewModelAsync(model);
                    return View(v2);
                }
                if (aspnetUser.Tenant.ClassiFication == 1 && DateTime.Now > aspnetUser.Tenant.VIPExpirationTime)
                {
                    await _events.RaiseAsync(new UserLoginFailureEvent(model.Account, AccountOptions.AccountTenantStateAnomalyErrorMessage, clientId: context?.Client.ClientId));
                    ModelState.AddModelError(string.Empty, $"������ʹ��Ȩ���ѹ��ڣ�����ʱ�䣺{aspnetUser.Tenant.VIPExpirationTime.ToString("yyyy-MM-dd")}������ϵ��Ӫ�̻����ѡ�");

                    var v2 = await BuildLoginViewModelAsync(model);
                    return View(v2);
                }

                await _events.RaiseAsync(new UserLoginSuccessEvent(aspnetUser.Account, aspnetUser.Id, aspnetUser.UserName, clientId: context?.Client.ClientId));

                // ֻ�����û�ѡ�񡰼�ס�ҡ�ʱ����ʽ���ù��ڡ���������������cookie�м�������õĹ��ڡ�
                AuthenticationProperties props = null;
                if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                {
                    props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                    };
                };

                // ������������ID���û�������֤cookie
                var isuser = new IdentityServerUser(aspnetUser.Id)
                {
                    DisplayName = aspnetUser.UserName
                };

                await HttpContext.SignInAsync(isuser, props);

                // ������¼�¼�
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
                        // �ͻ����Ǳ��صģ��������ֱ仯����ô�� ������Ӧ��Ϊ�������û���ø��õ�UX��
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    // ���ǿ�������ģ�͡�ReturnUrl��ΪGetAuthorizationContextAsync���طǿ�
                    return Redirect(model.ReturnUrl);
                }

                // ���󱾵�ҳ��
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
                    // �û����ܵ����һ����������-Ӧ�ñ���¼
                    throw new Exception("�Ƿ���ַ��Ϣ");
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

            //�����ⲿ��Ȩ���� ������Ȩ�����ṩ�����ȡ�����Ϣ authcontext?.IdP ��ص� scheme �Ĳ��Է������� �磺cookies
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                //�ж��ǲ���idr4�ṩ�ⲿ��¼������backurlֻ�ܴ���һ���ⲿ��չ��¼
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // ����ζ�Ŷ�·��UI��ֻ����һ���ⲿIdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Account = context?.LoginHint,
                };

                //����Idr4�ṩ��
                if (!local)
                {
                    //��ʾ��չ��¼ �������ģ��
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            //����Idp��չ��¼����ȡ���е���Ȩ������Ϣ ��ȡ�м����Ȩ�е���Ϣ Ȼ��չʾ����¼������ ���û�ѡ���ṩ�ĵ�������չ��¼
            var schemes = await _schemeProvider.GetAllSchemesAsync();

            //��¼�Ĳ��� ���� DisplayName���ǿյĲ�����Ϣ
            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;

            //�жϿͻ�����Ϣ
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
