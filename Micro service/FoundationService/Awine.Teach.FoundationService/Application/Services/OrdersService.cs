using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.Interfaces;
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
    /// 订单信息
    /// </summary>
    public class OrdersService : IOrdersService
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        private readonly IOrdersRepository _ordersRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<OrdersService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="ordersRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public OrdersService(IOrdersRepository ordersRepository,
            IMapper mapper,
            ILogger<OrdersService> logger)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="tradeCategories"></param>
        /// <param name="performanceOwnerId"></param>
        /// <param name="performanceTenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrdersViewModel>> GetAll(string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "")
        {
            var entities = await _ordersRepository.GetAll(tenantId, saaSVersionId, tradeCategories, performanceOwnerId, performanceTenantId);

            return _mapper.Map<IEnumerable<Orders>, IEnumerable<OrdersViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="tradeCategories"></param>
        /// <param name="performanceOwnerId"></param>
        /// <param name="performanceTenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<OrdersViewModel>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string saaSVersionId = "", int tradeCategories = 0, string performanceOwnerId = "", string performanceTenantId = "")
        {
            var entities = await _ordersRepository.GetPageList(page, limit, tenantId, saaSVersionId, tradeCategories, performanceOwnerId, performanceTenantId);

            return _mapper.Map<IPagedList<Orders>, IPagedList<OrdersViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrdersViewModel> GetModel(string id)
        {
            var entity = await _ordersRepository.GetModel(id);

            return _mapper.Map<Orders, OrdersViewModel>(entity);
        }
    }
}
