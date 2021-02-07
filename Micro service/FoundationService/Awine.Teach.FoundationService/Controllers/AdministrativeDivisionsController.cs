using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.FoundationService.Controllers
{
    /// <summary>
    /// 行政区域划分
    /// </summary>
    public class AdministrativeDivisionsController : ApiController
    {
        /// <summary>
        /// 行政区域划分
        /// </summary>
        private readonly IAdministrativeDivisionsService _administrativeDivisionsService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="administrativeDivisionsService"></param>
        public AdministrativeDivisionsController(IAdministrativeDivisionsService administrativeDivisionsService)
        {
            _administrativeDivisionsService = administrativeDivisionsService;
        }

        /// <summary>
        /// 取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> LoadAll()
        {
            return Response(success: true, data: await _administrativeDivisionsService.GetAll());
        }

        /// <summary>
        /// 获取某一区域的下级数据
        /// </summary>
        /// <param name="parentCode">父级ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("subordinate")]
        public async Task<IActionResult> LoadSubordinateRegionalism(int parentCode)
        {
            return Response(success: true, data: await _administrativeDivisionsService.GetSubordinateRegionalism(parentCode));
        }
    }
}