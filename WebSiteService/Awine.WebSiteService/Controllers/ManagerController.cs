using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Awine.WebSite.Applicaton.Interface;
using Awine.WebSite.Applicaton.ViewModels;
using Awine.WebSiteService.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Awine.WebSiteService.Controllers
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class ManagerController : ApiController
    {
        /// <summary>
        /// JwtSetting
        /// </summary>
        private readonly JwtSetting _jwtSetting;

        /// <summary>
        /// IManagerService
        /// </summary>
        private readonly IManagerService _managerService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jwtSetting"></param>
        /// <param name="managerService"></param>
        public ManagerController(IOptions<JwtSetting> option, IManagerService managerService)
        {
            _jwtSetting = option.Value;
            _managerService = managerService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        [Authorize]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15)
        {
            return Response(success: true, data: await _managerService.GetPageList(page, limit));
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("details")]
        [Authorize]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _managerService.GetModel(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [Authorize]
        public async Task<IActionResult> Add([FromForm]ManagerAddViewModel model)
        {
            var result = await _managerService.Add(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm]ManagerUpdateViewModel model)
        {
            var result = await _managerService.Update(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        [Authorize]
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _managerService.Delete(id);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatepassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromForm]ManagerUpdatePasswordViewModel model)
        {
            var result = await _managerService.UpdatePassword(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm]ManagerLoginViewModel model)
        {
            var result = await _managerService.GetModel(model);

            if (!result.Success)
            {
                return Response(success: false, message: result.Message);
            }

            ManagerViewModel manager = result.Data;

            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", manager.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim("name", manager.Name),
            };

            //创建令牌
            var token = new JwtSecurityToken(
              issuer: _jwtSetting.Issuer,
              audience: _jwtSetting.Audience,
              signingCredentials: _jwtSetting.Credentials,
              claims: claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddSeconds(_jwtSetting.ExpireSeconds)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Response(success: true, message: "登录成功", data: jwtToken);
        }
    }
}