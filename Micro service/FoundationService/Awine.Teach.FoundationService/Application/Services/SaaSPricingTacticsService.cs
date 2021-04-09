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
    /// SaaS定价策略
    /// </summary>
    public class SaaSPricingTacticsService : ISaaSPricingTacticsService
    {
        /// <summary>
        /// SaaS定价策略
        /// </summary>
        private readonly ISaaSPricingTacticsRepository _saaSPricingTacticsRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<SaaSPricingTacticsService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="saaSPricingTacticsRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public SaaSPricingTacticsService(ISaaSPricingTacticsRepository saaSPricingTacticsRepository,
            IMapper mapper,
            ILogger<SaaSPricingTacticsService> logger)
        {
            _saaSPricingTacticsRepository = saaSPricingTacticsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SaaSPricingTacticsViewModel>> GetAll(string saaSVersionId)
        {
            var entities = await _saaSPricingTacticsRepository.GetAll(saaSVersionId);

            return _mapper.Map<IEnumerable<SaaSPricingTactics>, IEnumerable<SaaSPricingTacticsViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<IPagedList<SaaSPricingTacticsViewModel>> GetPageList(int page = 1, int limit = 15, string saaSVersionId = "")
        {
            var entities = await _saaSPricingTacticsRepository.GetPageList(page, limit, saaSVersionId);

            return _mapper.Map<IPagedList<SaaSPricingTactics>, IPagedList<SaaSPricingTacticsViewModel>>(entities);
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SaaSPricingTacticsViewModel> GetModel(string id)
        {
            var entity = await _saaSPricingTacticsRepository.GetModel(id);

            return _mapper.Map<SaaSPricingTactics, SaaSPricingTacticsViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(SaaSPricingTacticsAddViewModel model)
        {
            var entity = _mapper.Map<SaaSPricingTacticsAddViewModel, SaaSPricingTactics>(model);

            var existing = await _saaSPricingTacticsRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }


            var result = await _saaSPricingTacticsRepository.Add(entity);

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
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(SaaSPricingTacticsUpdateViewModel model)
        {
            var entity = _mapper.Map<SaaSPricingTacticsUpdateViewModel, SaaSPricingTactics>(model);

            var existing = await _saaSPricingTacticsRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _saaSPricingTacticsRepository.Update(entity);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
