using AutoMapper;
using Awine.Framework.AspNetCore.Model;
using Awine.Framework.Core;
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
    /// 日常支出
    /// </summary>
    public class DailySpendingService : IDailySpendingService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<DailySpendingService> _logger;

        /// <summary>
        /// ICurrentUser
        /// </summary>
        private ICurrentUser _currentUser;

        /// <summary>
        /// IDailySpendingRepository
        /// </summary>
        private readonly IDailySpendingRepository _dailySpendingRepository;

        /// <summary>
        /// IFinancialItemRepository
        /// </summary>
        private readonly IFinancialItemRepository _financialItemRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="currentUser"></param>
        /// <param name="dailySpendingRepository"></param>
        /// <param name="financialItemRepository"></param>
        public DailySpendingService(
            IMapper mapper,
            ILogger<DailySpendingService> logger,
            ICurrentUser currentUser,
            IDailySpendingRepository dailySpendingRepository,
            IFinancialItemRepository financialItemRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _currentUser = currentUser;
            _dailySpendingRepository = dailySpendingRepository;
            _financialItemRepository = financialItemRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        public async Task<IPagedList<DailySpendingViewModel>> GetPageList(int page = 1, int limit = 15, string beginDate = "", string finishDate = "", string financialItemId = "")
        {
            var entities = await _dailySpendingRepository.GetPageList(page, limit, _currentUser.TenantId, beginDate, finishDate, financialItemId);

            return _mapper.Map<IPagedList<DailySpending>, IPagedList<DailySpendingViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DailySpendingViewModel>> GetAll(string financialItemId = "")
        {
            var entities = await _dailySpendingRepository.GetAll(_currentUser.TenantId, financialItemId);

            return _mapper.Map<IEnumerable<DailySpending>, IEnumerable<DailySpendingViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(DailySpendingAddViewModel model)
        {
            var item = await _financialItemRepository.GetModel(model.FinancialItemId);
            if (null == item)
            {
                return new Result { Success = false, Message = "未找到支出项目" };
            }

            var entity = _mapper.Map<DailySpendingAddViewModel, DailySpending>(model);

            entity.TenantId = _currentUser.TenantId;
            entity.FinancialItemName = item.Name;

            if (await _dailySpendingRepository.Add(entity) > 0)
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
        public async Task<Result> Update(DailySpendingUpdateViewModel model)
        {
            var entity = _mapper.Map<DailySpendingUpdateViewModel, DailySpending>(model);

            var item = await _financialItemRepository.GetModel(model.FinancialItemId);

            if (null == item)
            {
                return new Result { Success = false, Message = "未找到支出项目" };
            }

            entity.FinancialItemName = item.Name;

            if (await _dailySpendingRepository.Update(entity) > 0)
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
        public async Task<int> Delete(string id)
        {
            return await _dailySpendingRepository.Delete(id);
        }

        #region 数据统计

        /// <summary>
        /// 各项目常开销统计
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<PieChartViewModel> SpendingReport(string date)
        {
            PieChartViewModel chartData = new PieChartViewModel();

            var items = await _financialItemRepository.GetAll(tenantId: _currentUser.TenantId, status: 1);

            var spending = await _dailySpendingRepository.GetAll(tenantId: _currentUser.TenantId);

            DateTime time = Convert.ToDateTime(date);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            spending = spending.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            spending = spending.Where(x => x.CreateTime <= dateTo);

            foreach (var item in items)
            {
                var itemspend = spending.Where(x => x.FinancialItemId.Equals(item.Id));
                var sum = 0M;
                foreach (var s in itemspend)
                {
                    sum += s.Amount;
                }
                chartData.LegendData.Add(item.Name);
                chartData.SeriesDecimalData.Add(new PieChartSeriesDecimalData()
                {
                    Value = sum,
                    Name = item.Name
                });
            }

            return chartData;
        }

        #endregion
    }
}
