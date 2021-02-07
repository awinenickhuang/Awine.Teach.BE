using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.OperationService.Application.Interfaces;
using Awine.Teach.OperationService.Application.ViewModels;
using Awine.Teach.OperationService.Domain;
using Awine.Teach.OperationService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Application.Services
{
    /// <summary>
    /// 反馈信息
    /// </summary>
    public class FeedbackService : IFeedbackService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// IUser
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<FeedbackService> _logger;

        /// <summary>
        /// IFeedbackRepository
        /// </summary>
        private readonly IFeedbackRepository _feedbackRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="feedbackRepository"></param>
        public FeedbackService(
            IMapper mapper,
            ICurrentUser user,
            ILogger<FeedbackService> logger,
            IFeedbackRepository feedbackRepository
            )
        {
            _mapper = mapper;
            _user = user;
            _logger = logger;
            _feedbackRepository = feedbackRepository;
        }


        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<FeedbackViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _feedbackRepository.GetPageList(page, limit);

            return _mapper.Map<IPagedList<Feedback>, IPagedList<FeedbackViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FeedbackViewModel>> GetAll()
        {
            var entities = await _feedbackRepository.GetAll();

            return _mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FeedbackViewModel> GetModel(string id)
        {
            var entitiy = await _feedbackRepository.GetModel(id);

            return _mapper.Map<Feedback, FeedbackViewModel>(entitiy);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(FeedbackAddViewModel model)
        {
            var entity = _mapper.Map<FeedbackAddViewModel, Feedback>(model);
            entity.TenantId = _user.TenantId;
            entity.UserId = _user.UserId;
            return await _feedbackRepository.Add(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(string id)
        {
            return await _feedbackRepository.Delete(id);
        }
    }
}
