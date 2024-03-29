﻿using System;
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
    /// 咨询记录
    /// </summary>
    public class ConsultRecordController : ApiController
    {
        /// <summary>
        /// IConsultRecordService
        /// </summary>
        private readonly IConsultRecordService _consultRecordService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="consultRecordService"></param>
        public ConsultRecordController(IConsultRecordService consultRecordService)
        {
            _consultRecordService = consultRecordService;
        }

        /// <summary>
        /// 机构首页 -> 生源情况统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("statistical")]
        public async Task<IActionResult> ConsulRecordStatistical()
        {
            return Response(success: true, data: await _consultRecordService.ConsulRecordStatistical());
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="counselingCourseId"></param>
        /// <param name="marketingChannelId"></param>
        /// <param name="trackingState"></param>
        /// <param name="trackingStafferId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string name = "", string phoneNumber = "", string startTime = "", string endTime = "", string counselingCourseId = "", string marketingChannelId = "", int trackingState = 0, string trackingStafferId = "")
        {
            return Response(success: true, data: await _consultRecordService.GetPageList(page, limit, name, phoneNumber, startTime, endTime, counselingCourseId, marketingChannelId, trackingState, trackingStafferId));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] ConsultRecordAddViewModel model)
        {
            var result = await _consultRecordService.Add(model);

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
        public async Task<IActionResult> Put([FromForm] ConsultRecordUpdateViewModel model)
        {
            var result = await _consultRecordService.Update(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 跟进任务指派
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("trackingassigned")]
        public async Task<IActionResult> TrackingAssigned([FromForm] ConsultRecordTrackingAssignedViewModel model)
        {
            var result = await _consultRecordService.TrackingAssigned(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 生源情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("monthchartreport")]
        public async Task<IActionResult> ConsultRecordMonthChartReport(string designatedMonth)
        {
            return Response(success: true, data: await _consultRecordService.ConsultRecordMonthChartReport(designatedMonth));
        }

        /// <summary>
        /// 生源情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("coursemonthchartreport")]
        public async Task<IActionResult> ConsultRecordCourseMonthChartReport(string designatedMonth)
        {
            return Response(success: true, data: await _consultRecordService.ConsultRecordCourseMonthChartReport(designatedMonth));
        }

        /// <summary>
        /// 营销转化情况 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("studenttransformationtreport")]
        public async Task<IActionResult> StudentTransformationtReport(string designatedMonth)
        {
            return Response(success: true, data: await _consultRecordService.StudentTransformationtReport(designatedMonth));
        }

        /// <summary>
        /// 各营销渠道生源来源情况 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns>各招生渠道指定月份的新增生源数量</returns>
        [HttpGet]
        [Route("studentsourcechannelreport")]
        public async Task<IActionResult> StudentSourceChannelReport(string designatedMonth)
        {
            return Response(success: true, data: await _consultRecordService.StudentSourceChannelReport(designatedMonth));
        }
    }
}