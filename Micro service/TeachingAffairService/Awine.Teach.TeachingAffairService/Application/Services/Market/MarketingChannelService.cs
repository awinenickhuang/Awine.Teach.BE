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
    /// 营销渠道
    /// </summary>
    public class MarketingChannelService : IMarketingChannelService
    {
        /// <summary>
        /// IMarketingChannelRepository
        /// </summary>
        private readonly IMarketingChannelRepository _marketingChannelRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<MarketingChannelService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="marketingChannelRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        public MarketingChannelService(IMarketingChannelRepository marketingChannelRepository,
            IMapper mapper,
            ILogger<MarketingChannelService> logger,
            ICurrentUser user)
        {
            _marketingChannelRepository = marketingChannelRepository;
            _mapper = mapper;
            _logger = logger;
            _user = user;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<MarketingChannelViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _marketingChannelRepository.GetPageList(page, limit, _user.TenantId);

            return _mapper.Map<IPagedList<MarketingChannel>, IPagedList<MarketingChannelViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MarketingChannelViewModel>> GetAll()
        {
            var entities = await _marketingChannelRepository.GetAll(_user.TenantId);

            return _mapper.Map<IEnumerable<MarketingChannel>, IEnumerable<MarketingChannelViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MarketingChannelViewModel> GetModel(string id)
        {
            var entity = await _marketingChannelRepository.GetModel(id);

            return _mapper.Map<MarketingChannel, MarketingChannelViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(MarketingChannelAddViewModel model)
        {
            var entity = _mapper.Map<MarketingChannelAddViewModel, MarketingChannel>(model);
            entity.TenantId = _user.TenantId;

            if (null != await _marketingChannelRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _marketingChannelRepository.Add(entity) > 0)
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
        public async Task<Result> Update(MarketingChannelUpdateViewModel model)
        {
            var entity = _mapper.Map<MarketingChannelUpdateViewModel, MarketingChannel>(model);

            if (null != await _marketingChannelRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _marketingChannelRepository.Update(entity) > 0)
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
            //var users = await _aspnetUserRepository.GetAllInDepartment(id);
            //if (users.Count() > 0)
            //{
            //    return new Result { Success = false, Message = "操作失败：！" };
            //}

            var result = await _marketingChannelRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
