using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.FoundationService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.FoundationService.Controllers
{
    /// <summary>
    /// 角色声明信息
    /// </summary>
    public class RolesClaimsController : ApiController
    {
        /// <summary>
        /// 角色声明信息
        /// </summary>
        private readonly IRolesClaimsService _rolesClaimsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rolesClaimsService"></param>
        public RolesClaimsController(IRolesClaimsService rolesClaimsService)
        {
            _rolesClaimsService = rolesClaimsService;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string roleId)
        {
            return Response(success: true, data: await _rolesClaimsService.GetAll(roleId));
        }
    }
}