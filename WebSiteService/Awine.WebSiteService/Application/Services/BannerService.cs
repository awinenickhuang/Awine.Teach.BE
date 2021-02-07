using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.WebSite.Applicaton.Interface;
using Awine.WebSite.Applicaton.ServiceResult;
using Awine.WebSite.Applicaton.ViewModels;
using Awine.WebSite.Domain;
using Awine.WebSite.Domain.Interface;
using Microsoft.Extensions.Logging;

namespace Awine.WebSite.Applicaton.Services
{
    /// <summary>
    /// 横幅图片
    /// </summary>
    public class BannerService : IBannerService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<BannerService> _logger;

        /// <summary>
        /// IBannerRepository
        /// </summary>
        private readonly IBannerRepository _bannerRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="bannerRepository"></param>
        public BannerService(
            IMapper mapper,
            ILogger<BannerService> logger,
            IBannerRepository bannerRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _bannerRepository = bannerRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<BannerViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _bannerRepository.GetPageList(page, limit);

            return _mapper.Map<IPagedList<Banner>, IPagedList<BannerViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BannerViewModel>> GetAll()
        {
            var entities = await _bannerRepository.GetAll();

            return _mapper.Map<IEnumerable<Banner>, IEnumerable<BannerViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(BannerAddViewModel model)
        {
            var banner = _mapper.Map<BannerAddViewModel, Banner>(model);

            if (await _bannerRepository.Add(banner) > 0)
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
            var res = await _bannerRepository.Delete(id);

            if (res > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
