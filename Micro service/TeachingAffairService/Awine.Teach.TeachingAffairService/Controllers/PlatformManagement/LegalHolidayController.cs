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
    /// 法定假日
    /// </summary>
    public class LegalHolidayController : ApiController
    {
        /// <summary>
        /// ILegalHolidayService
        /// </summary>
        private readonly ILegalHolidayService _legalHolidayService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="legalHolidayService"></param>
        public LegalHolidayController(ILegalHolidayService legalHolidayService)
        {
            _legalHolidayService = legalHolidayService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, int year = 0)
        {
            return Response(success: true, data: await _legalHolidayService.GetPageList(page, limit, year));
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(int year = 0)
        {
            return Response(success: true, data: await _legalHolidayService.GetAll(year));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]LegalHolidayAddViewModel model)
        {
            var result = await _legalHolidayService.Add(model);
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
        public async Task<IActionResult> Put([FromForm]LegalHolidayUpdateViewModel model)
        {
            var result = await _legalHolidayService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = await _legalHolidayService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}