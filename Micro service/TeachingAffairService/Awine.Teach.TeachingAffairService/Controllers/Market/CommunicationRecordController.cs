using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.TeachingAffairService.Controllers
{
    /// <summary>
    /// 咨询记录 -> 跟进记录
    /// </summary>
    public class CommunicationRecordController : ApiController
    {
        /// <summary>
        /// ICommunicationRecordService
        /// </summary>
        private readonly ICommunicationRecordService _communicationRecordService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="communicationRecordService"></param>
        public CommunicationRecordController(ICommunicationRecordService communicationRecordService)
        {
            _communicationRecordService = communicationRecordService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="consultRecordId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string consultRecordId = "")
        {
            return Response(success: true, data: await _communicationRecordService.GetPageList(page, limit, consultRecordId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]CommunicationRecordAddViewModel model)
        {
            var result = await _communicationRecordService.Add(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}