using System;
using System.Threading.Tasks;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Awine.Teach.TeachingAffairService.Controllers
{
    /// <summary>
    /// 学生选课信息
    /// </summary>
    public class StudentCourseItemController : ApiController
    {
        /// <summary>
        /// IStudentCourseItemService
        /// </summary>
        private readonly IStudentCourseItemService _studentCourseItemService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="studentCourseItemService"></param>
        public StudentCourseItemController(IStudentCourseItemService studentCourseItemService)
        {
            _studentCourseItemService = studentCourseItemService;
        }

        /// <summary>
        /// 所有数据 -> 不分页列表
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAll(string studentId, string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0)
        {
            return Response(success: true, data: await _studentCourseItemService.GetAll(studentId, courseId, classesId, studentOrderId, learningProcess));
        }

        /// <summary>
        /// 所有数据 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("pagelist")]
        public async Task<IActionResult> GetPageList(int page = 1, int limit = 15, string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0)
        {
            return Response(success: true, data: await _studentCourseItemService.GetPageList(page, limit, studentId, courseId, classesId, studentOrderId, learningProcess));
        }

        /// <summary>
        /// 把学生添加进班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addstudentsintoclass")]
        public async Task<IActionResult> AddStudentsIntoClass([FromForm]StudentCourseItemUpdateViewModel model)
        {
            var result = await _studentCourseItemService.AddStudentsIntoClass(model);
            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }
            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 把学生从班级中移除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("removestudentfromclass")]
        public async Task<IActionResult> RemoveStudentFromClass([FromForm]StudentCourseItemUpdateViewModel model)
        {
            var result = await _studentCourseItemService.RemoveStudentFromClass(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }

        /// <summary>
        /// 更新报读课程的学习进度
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatelearningprocess")]
        public async Task<IActionResult> UpdateLearningProcess([FromForm]UpdateLearningProcessViewModel model)
        {
            var result = await _studentCourseItemService.UpdateLearningProcess(model);

            if (result.Success)
            {
                return Response(success: true, message: result.Message);
            }

            return Response(success: false, message: result.Message);
        }
    }
}