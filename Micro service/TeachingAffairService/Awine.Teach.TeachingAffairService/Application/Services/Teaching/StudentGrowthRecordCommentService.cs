using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 学生成长记录评论
    /// </summary>
    public class StudentGrowthRecordCommentService : IStudentGrowthRecordCommentService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<CourseService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 学生成长记录 -> 评论
        /// </summary>
        private readonly IStudentGrowthRecordCommentRepository _studentGrowthRecordCommentRepository;

        /// <summary>
        /// 学生成长记录
        /// </summary>
        private readonly IStudentGrowthRecordRepository _studentGrowthRecordRepository;

        /// <summary>
        /// 构造 - 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="studentGrowthRecordCommentRepository"></param>
        /// <param name="studentGrowthRecordRepository"></param>
        public StudentGrowthRecordCommentService(IMapper mapper,
            ILogger<CourseService> logger,
            ICurrentUser user,
            IStudentGrowthRecordCommentRepository studentGrowthRecordCommentRepository,
            IStudentGrowthRecordRepository studentGrowthRecordRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _studentGrowthRecordCommentRepository = studentGrowthRecordCommentRepository;
            _studentGrowthRecordRepository = studentGrowthRecordRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentGrowthRecordId"></param>
        /// <returns></returns>
        public async Task<IPagedList<StudentGrowthRecordCommentViewModel>> GetPageList(int page = 1, int limit = 15, string studentGrowthRecordId = "")
        {
            var entities = await _studentGrowthRecordCommentRepository.GetPageList(page, limit, studentGrowthRecordId);

            return _mapper.Map<IPagedList<StudentGrowthRecordComment>, IPagedList<StudentGrowthRecordCommentViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentGrowthRecordCommentViewModel> GetModel(string id)
        {
            var entity = await _studentGrowthRecordCommentRepository.GetModel(id);

            return _mapper.Map<StudentGrowthRecordComment, StudentGrowthRecordCommentViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(StudentGrowthRecordCommentAddViewModel model)
        {
            var studentGrowthRecord = await _studentGrowthRecordRepository.GetModel(model.StudentGrowthRecordId);
            if (null == studentGrowthRecord)
            {
                return new Result { Success = false, Message = "未找到学生成长记录！" };
            }
            var entity = _mapper.Map<StudentGrowthRecordCommentAddViewModel, StudentGrowthRecordComment>(model);
            entity.TenantId = studentGrowthRecord.TenantId;
            entity.CreatorId = _user.UserId;
            entity.CreatorName = _user.Name;

            if (await _studentGrowthRecordCommentRepository.Add(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
