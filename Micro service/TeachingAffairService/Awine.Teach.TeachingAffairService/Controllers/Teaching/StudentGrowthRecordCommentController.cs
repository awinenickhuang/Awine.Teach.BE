using Awine.Teach.TeachingAffairService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Controllers.Teaching
{
    /// <summary>
    /// 学生成长记录评论
    /// </summary>
    public class StudentGrowthRecordCommentController : ApiController
    {
        /// <summary>
        /// IStudentGrowthRecordCommentService
        /// </summary>
        private readonly IStudentGrowthRecordCommentService _studentGrowthRecordCommentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="studentGrowthRecordCommentService"></param>
        public StudentGrowthRecordCommentController(IStudentGrowthRecordCommentService studentGrowthRecordCommentService)
        {
            _studentGrowthRecordCommentService = studentGrowthRecordCommentService;
        }
    }
}
