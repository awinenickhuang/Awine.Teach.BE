using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using Awine.Teach.FoundationService.Domain.Interface;
using Awine.Teach.FoundationService.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 应用版本
    /// </summary>
    public class ApplicationVersionService : IApplicationVersionService
    {
        /// <summary>
        /// 应用版本
        /// </summary>
        private readonly IApplicationVersionRepository _applicationVersionRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ApplicationVersionService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="applicationVersionRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ApplicationVersionService(IApplicationVersionRepository applicationVersionRepository, IMapper mapper, ILogger<ApplicationVersionService> logger)
        {
            _applicationVersionRepository = applicationVersionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        public async Task<IPagedList<ApplicationVersionViewModel>> GetPageList(int pageIndex = 1, int pageSize = 15, string name = "", string identifying = "")
        {
            var entities = await _applicationVersionRepository.GetPageList(pageIndex, pageSize, name, identifying);

            return _mapper.Map<IPagedList<ApplicationVersion>, IPagedList<ApplicationVersionViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApplicationVersionViewModel>> GetAll(string name = "", string identifying = "")
        {
            var entities = await _applicationVersionRepository.GetAll();

            return _mapper.Map<IEnumerable<ApplicationVersion>, IEnumerable<ApplicationVersionViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ApplicationVersionAddViewModel model)
        {
            var entity = _mapper.Map<ApplicationVersionAddViewModel, ApplicationVersion>(model);

            var existing = await _applicationVersionRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _applicationVersionRepository.Add(entity);

            if (result > 0)
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
        public async Task<Result> Update(ApplicationVersionUpdateViewModel model)
        {
            var entity = _mapper.Map<ApplicationVersionUpdateViewModel, ApplicationVersion>(model);

            var existing = await _applicationVersionRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _applicationVersionRepository.Update(entity);

            if (result > 0)
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
            var result = await _applicationVersionRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApplicationVersionViewModel> GetModel(string id)
        {
            var entity = await _applicationVersionRepository.GetModel(id);

            return _mapper.Map<ApplicationVersion, ApplicationVersionViewModel>(entity);
        }
    }
}
