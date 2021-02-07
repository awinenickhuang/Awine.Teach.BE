using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.OperationService.Application.Interfaces;
using Awine.Teach.OperationService.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.OperationService.Controllers
{
    /// <summary>
    /// 反馈信息
    /// </summary>
    public class FeedbackController : ApiController
    {
        /// <summary>
        /// IFeedbackService
        /// </summary>
        private readonly IFeedbackService _feedbackService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="feedbackService"></param>
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15)
        {
            return Response(success: true, data: await _feedbackService.GetPageList(page, limit));
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("single")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _feedbackService.GetModel(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]FeedbackAddViewModel model)
        {
            var result = await _feedbackService.Add(model);
            if (result > 0)
            {
                return Response(success: true, message: "操作成功");
            }
            return Response(success: false, message: "操作失败");
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
            var result = await _feedbackService.Delete(id);
            if (result > 0)
            {
                return Response(success: true, message: "操作成功");
            }
            return Response(success: false, message: "操作失败");
        }
    }
}