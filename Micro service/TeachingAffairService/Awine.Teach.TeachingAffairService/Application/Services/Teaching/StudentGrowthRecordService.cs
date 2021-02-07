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
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 学生成长记录
    /// </summary>
    public class StudentGrowthRecordService : IStudentGrowthRecordService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<StudentGrowthRecordService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 正式学生
        /// </summary>
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// 学生成长记录
        /// </summary>
        private readonly IStudentGrowthRecordRepository _studentGrowthRecordRepository;

        /// <summary>
        /// 构造函数 - 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="studentRepository"></param>
        /// <param name="studentGrowthRecordRepository"></param>
        public StudentGrowthRecordService(
            IMapper mapper,
            ILogger<StudentGrowthRecordService> logger,
            ICurrentUser user,
            IStudentRepository studentRepository,
            IStudentGrowthRecordRepository studentGrowthRecordRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _studentRepository = studentRepository;
            _studentGrowthRecordRepository = studentGrowthRecordRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task<IPagedList<StudentGrowthRecordViewModel>> GetPageList(int page = 1, int limit = 15, string studentId = "")
        {
            var entities = await _studentGrowthRecordRepository.GetPageList(page, limit, _user.TenantId, studentId);
            return _mapper.Map<IPagedList<StudentGrowthRecord>, IPagedList<StudentGrowthRecordViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentGrowthRecordViewModel> GetModel(string id)
        {
            var entity = await _studentGrowthRecordRepository.GetModel(id);
            return _mapper.Map<StudentGrowthRecord, StudentGrowthRecordViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(StudentGrowthRecordAddViewModel model)
        {
            var entity = _mapper.Map<StudentGrowthRecordAddViewModel, StudentGrowthRecord>(model);
            entity.TenantId = _user.TenantId;
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatorId = _user.UserId;
            entity.CreatorName = _user.Name;

            var student = await _studentRepository.GetModel(model.StudentId);
            if (null == student)
            {
                return new Result { Success = false, Message = "未找到学生信息！" };
            }

            entity.StudentName = student.Name;

            if (!student.TenantId.Equals(_user.TenantId))
            {
                return new Result { Success = false, Message = "未经允许的数据权限！" };
            }

            var exist = await _studentGrowthRecordRepository.GetModel(entity);
            if (null != exist)
            {
                return new Result { Success = false, Message = "标题（主题）重复！" };
            }

            if (await _studentGrowthRecordRepository.Add(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(StudentGrowthRecordUpdateViewModel model)
        {
            var entity = _mapper.Map<StudentGrowthRecordUpdateViewModel, StudentGrowthRecord>(model);
            entity.CreatorId = _user.UserId;
            entity.CreatorName = _user.Name;
            var exist = await _studentGrowthRecordRepository.GetModel(entity);
            if (null != exist)
            {
                return new Result { Success = false, Message = "主题重复！" };
            }

            if (await _studentGrowthRecordRepository.Update(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(string id)
        {
            if (await _studentGrowthRecordRepository.Delete(id))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
