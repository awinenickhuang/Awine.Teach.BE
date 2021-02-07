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
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 行业
    /// </summary>
    public class IndustryCategoryService : IIndustryCategoryService
    {
        /// <summary>
        /// 行业
        /// </summary>
        private readonly IIndustryCategoryRepository _industryCategoryRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<IndustryCategoryService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="industryCategoryRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public IndustryCategoryService(IIndustryCategoryRepository industryCategoryRepository, IMapper mapper, ILogger<IndustryCategoryService> logger)
        {
            _industryCategoryRepository = industryCategoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IPagedList<IndustryCategoryViewModel>> GetPageList(int pageIndex = 1, int pageSize = 15)
        {
            var entities = await _industryCategoryRepository.GetPageList(pageIndex, pageSize);
            return _mapper.Map<IPagedList<IndustryCategory>, IPagedList<IndustryCategoryViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IndustryCategoryViewModel>> GetAll()
        {
            var entities = await _industryCategoryRepository.GetAll();
            return _mapper.Map<IEnumerable<IndustryCategory>, IEnumerable<IndustryCategoryViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(IndustryCategoryAddViewModel model)
        {
            var entity = _mapper.Map<IndustryCategoryAddViewModel, IndustryCategory>(model);

            if (null != await _industryCategoryRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _industryCategoryRepository.Add(entity) > 0)
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
        public async Task<Result> Update(IndustryCategoryViewModel model)
        {
            var entity = _mapper.Map<IndustryCategoryViewModel, IndustryCategory>(model);

            if (null != await _industryCategoryRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _industryCategoryRepository.Update(entity) > 0)
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
            var result = await _industryCategoryRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
