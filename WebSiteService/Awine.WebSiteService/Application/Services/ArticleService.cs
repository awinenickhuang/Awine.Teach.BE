using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.WebSite.Applicaton.Interface;
using Awine.WebSite.Applicaton.ServiceResult;
using Awine.WebSite.Applicaton.ViewModels;
using Awine.WebSite.Domain;
using Awine.WebSite.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Applicaton.Services
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class ArticleService : IArticleService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ArticleService> _logger;

        /// <summary>
        /// IArticleRepository
        /// </summary>
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="articleRepository"></param>
        public ArticleService(
            IMapper mapper,
            ILogger<ArticleService> logger,
            IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="forumId"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="contentSource"></param>
        /// <returns></returns>
        public async Task<IPagedList<ArticleViewModel>> GetPageList(int page = 1, int limit = 15, string forumId = "", string title = "", string author = "", string contentSource = "")
        {
            var entities = await _articleRepository.GetPageList(page, limit, forumId, title, author, contentSource);

            return _mapper.Map<IPagedList<Article>, IPagedList<ArticleViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ArticleViewModel>> GetAll(string forumId = "", int number = 0)
        {
            var entities = await _articleRepository.GetAll(forumId, number);

            return _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ArticleViewModel> GetModel(string id)
        {
            var entity = await _articleRepository.GetModel(id);

            return _mapper.Map<Article, ArticleViewModel>(entity);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        public async Task<ArticleViewModel> GetModelForumAccording(string forumId)
        {
            var entity = await _articleRepository.GetModelForumAccording(forumId);

            return _mapper.Map<Article, ArticleViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ArticleAddViewModel model)
        {
            var articale = _mapper.Map<ArticleAddViewModel, Article>(model);

            if (await _articleRepository.Add(articale) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(ArticleUpdateViewModel model)
        {
            var articale = _mapper.Map<ArticleUpdateViewModel, Article>(model);

            if (await _articleRepository.Update(articale) > 0)
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
            var res = await _articleRepository.Delete(id);

            if (res > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
