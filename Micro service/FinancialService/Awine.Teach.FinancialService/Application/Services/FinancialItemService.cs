using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.FinancialService.Application.Interfaces;
using Awine.Teach.FinancialService.Application.ServiceResult;
using Awine.Teach.FinancialService.Application.ViewModels;
using Awine.Teach.FinancialService.Domain;
using Awine.Teach.FinancialService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FinancialService.Application.Services
{
    /// <summary>
    /// 财务收支项目
    /// </summary>
    public class FinancialItemService : IFinancialItemService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<FinancialItemService> _logger;

        /// <summary>
        /// ICurrentUser
        /// </summary>
        private ICurrentUser _currentUser;

        /// <summary>
        /// IFinancialItemRepository
        /// </summary>
        private readonly IFinancialItemRepository _financialItemRepository;

        /// <summary>
        /// IDailySpendingRepository
        /// </summary>
        private readonly IDailySpendingRepository _dailySpendingRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="currentUser"></param>
        /// <param name="financialItemRepository"></param>
        /// <param name="dailySpendingRepository"></param>
        public FinancialItemService(
            IMapper mapper,
            ILogger<FinancialItemService> logger,
            ICurrentUser currentUser,
            IFinancialItemRepository financialItemRepository,
            IDailySpendingRepository dailySpendingRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _currentUser = currentUser;
            _financialItemRepository = financialItemRepository;
            _dailySpendingRepository = dailySpendingRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<IPagedList<FinancialItemViewModel>> GetPageList(int page = 1, int limit = 15, int status = 0)
        {
            var entities = await _financialItemRepository.GetPageList(page, limit, _currentUser.TenantId, status);

            return _mapper.Map<IPagedList<FinancialItem>, IPagedList<FinancialItemViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<FinancialItemViewModel>> GetAll(int status = 0)
        {
            var entities = await _financialItemRepository.GetAll(_currentUser.TenantId, status);

            return _mapper.Map<IEnumerable<FinancialItem>, IEnumerable<FinancialItemViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FinancialItemViewModel> GetModel(string id)
        {
            var entities = await _financialItemRepository.GetModel(id);

            return _mapper.Map<FinancialItem, FinancialItemViewModel>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(FinancialItemAddViewModel model)
        {
            var entity = _mapper.Map<FinancialItemAddViewModel, FinancialItem>(model);
            entity.TenantId = _currentUser.TenantId;
            if (null != await _financialItemRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "操作失败 -> 数据重复" };
            }
            if (await _financialItemRepository.Add(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(FinancialItemUpdateViewModel model)
        {
            var entity = _mapper.Map<FinancialItemUpdateViewModel, FinancialItem>(model);
            if (null != await _financialItemRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "操作失败 -> 数据重复" };
            }
            if (await _financialItemRepository.Update(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 更新 -> 启用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdateStatus(FinancialItemUpdateStatusViewModel model)
        {
            var item = new FinancialItem()
            {
                Id = model.Id,
                Status = model.Status
            };
            if (await _financialItemRepository.UpdateStatus(item) > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(string id)
        {
            var spends = await _dailySpendingRepository.GetAll(_currentUser.TenantId, id);

            if (spends.Count() > 0)
            {
                return new Result { Success = false, Message = "数据在使用中，不允许删除！" };
            }

            if (await _financialItemRepository.Delete(id) > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }

            return new Result { Success = false, Message = "操作失败" };
        }
    }
}
