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
    /// 试听记录
    /// </summary>
    public class TrialClassController : ApiController
    {
        /// <summary>
        /// ITrialClassService
        /// </summary>
        private readonly ITrialClassService _trialClassService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trialClassService"></param>
        public TrialClassController(ITrialClassService trialClassService)
        {
            _trialClassService = trialClassService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="listeningState"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(int listeningState = 0, string courseScheduleId = "")
        {
            return Response(success: true, data: await _trialClassService.GetAll(listeningState, courseScheduleId));
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="listeningState"></param>
        /// <param name="teacherId"></param>
        /// <param name="courseId"></param>
        /// <param name="courseScheduleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string name = "", string phoneNumber = "", int listeningState = 0, string teacherId = "", string courseId = "", string courseScheduleId = "")
        {
            return Response(success: true, data: await _trialClassService.GetPageList(page, limit, name, phoneNumber, listeningState, teacherId, courseId, courseScheduleId));
        }

        /// <summary>
        /// 跟班试听
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("listenfollowclass")]
        public async Task<IActionResult> ListenFollowClass([FromForm] TrialClassListenViewModel model)
        {
            var result = await _trialClassService.ListenFollowClass(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 一对一试听
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("onetoonelisten")]
        public async Task<IActionResult> OneToOneListen([FromForm] TrialClassListenCourseViewModel model)
        {
            var result = await _trialClassService.OneToOneListen(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 更新 -> 到课状态 -> 单个
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatelisteningstate")]
        public async Task<IActionResult> UpdateListeningState([FromForm] TrialClassUpdateListeningStateViewModel model)
        {
            var result = await _trialClassService.UpdateListeningState(model);
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
        public async Task<IActionResult> Delete([FromForm] string id)
        {
            var result = await _trialClassService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        #region 统计分析

        /// <summary>
        ///  试听课程情况 -> 当月每天的试所有课程试听情况
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("trialclassreportchart")]
        public async Task<IActionResult> TrialClassReportChart(string designatedMonth)
        {
            var result = await _trialClassService.TrialClassReportChart(designatedMonth);
            return Response(success: true, data: result);
        }

        #endregion
    }
}