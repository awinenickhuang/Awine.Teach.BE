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
    /// 班级管理
    /// </summary>
    public class ClassesController : ApiController
    {
        /// <summary>
        /// IClassesService
        /// </summary>
        private readonly IClassesService _classesService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="classesService"></param>
        public ClassesController(IClassesService classesService)
        {
            _classesService = classesService;
        }

        /// <summary>
        /// 所有数据 -> 个般用于下拉框
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "")
        {
            return Response(success: true, data: await _classesService.GetAll(courseId, recruitStatus, typeOfClass, beginDate, finishDate));
        }

        /// <summary>
        /// 分页列表 -> 一般用于表格
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string name = "", string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "")
        {
            return Response(success: true, data: await _classesService.GetPageList(page, limit, name, courseId, recruitStatus, typeOfClass, beginDate, finishDate));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]ClassesAddViewModel model)
        {
            var result = await _classesService.Add(model);
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
        public async Task<IActionResult> Put([FromForm]ClassesUpdateViewModel model)
        {
            var result = await _classesService.Update(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 更新班级招生状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updaterecruitstatus")]
        public async Task<IActionResult> UpdateRecruitStatus([FromForm]ClassesUpdateRecruitStatusViewModel model)
        {
            var result = await _classesService.UpdateRecruitStatus(model);
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
            var result = await _classesService.Delete(id);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }
    }
}