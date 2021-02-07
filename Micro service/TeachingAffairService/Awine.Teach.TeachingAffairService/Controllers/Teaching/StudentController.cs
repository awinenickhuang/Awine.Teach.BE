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
    /// 学生管理
    /// </summary>
    public class StudentController : ApiController
    {
        /// <summary>
        /// IStudentService
        /// </summary>
        private readonly IStudentService _studentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="studentService"></param>
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string name = "", int gender = 0, string phoneNumber = "", int learningProcess = 0)
        {
            return Response(success: true, data: await _studentService.GetPageList(page, limit, name, gender, phoneNumber, learningProcess));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm]StudentAddViewModel model)
        {
            var result = await _studentService.Add(model);

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
        public async Task<IActionResult> Update([FromForm]StudentUpdateViewModel model)
        {
            var result = await _studentService.Update(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 取一个学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("single")]
        public async Task<IActionResult> GetModel(string id)
        {
            return Response(success: true, data: await _studentService.GetModel(id));
        }

        /// <summary>
        /// 学生报名 -> 新生报名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Registration([FromForm]StudentRegistrationViewModel model)
        {
            var result = await _studentService.Registration(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 学生报名 -> 学生扩科
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("increaselearningcourses")]
        public async Task<IActionResult> IncreaseLearningCourses([FromForm]StudentIncreaseLearningCoursesViewModel model)
        {
            var result = await _studentService.IncreaseLearningCourses(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 学生报名 -> 缴费续费
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("continuetopaytuition")]
        public async Task<IActionResult> ContinueTopaytuition([FromForm]StudentSupplementViewModel model)
        {
            var result = await _studentService.ContinueTopaytuition(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }
    }
}