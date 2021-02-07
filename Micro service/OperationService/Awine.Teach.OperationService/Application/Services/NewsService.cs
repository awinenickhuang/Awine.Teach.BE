using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.OperationService.Application.Interfaces;
using Awine.Teach.OperationService.Application.ServiceResult;
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
    /// 行业资讯
    /// </summary>
    public class NewsService : INewsService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<NewsService> _logger;

        /// <summary>
        /// INewsRepository
        /// </summary>
        private readonly INewsRepository _newsRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="newsRepository"></param>
        public NewsService(
            IMapper mapper,
            ILogger<NewsService> logger,
            INewsRepository newsRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _newsRepository = newsRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<NewsViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _newsRepository.GetPageList(page, limit);

            return _mapper.Map<IPagedList<News>, IPagedList<NewsViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<NewsViewModel> GetModel(string id)
        {
            var entitiy = await _newsRepository.GetModel(id);

            return _mapper.Map<News, NewsViewModel>(entitiy);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(NewsAddViewModel model)
        {
            var entity = _mapper.Map<NewsAddViewModel, News>(model);

            if (await _newsRepository.Add(entity) > 0)
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
            if (await _newsRepository.Delete(id) > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }

            return new Result { Success = false, Message = "操作失败" };
        }
    }
}
