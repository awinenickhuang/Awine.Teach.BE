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
    /// 平台公告
    /// </summary>
    public class AnnouncementService : IAnnouncementService
    {
        /// <summary>
        /// IAnnouncementRepository
        /// </summary>
        private readonly IAnnouncementRepository _announcementRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<AnnouncementService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="announcementRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public AnnouncementService(IAnnouncementRepository announcementRepository,
            IMapper mapper,
            ILogger<AnnouncementService> logger)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<AnnouncementViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _announcementRepository.GetPageList(page, limit);

            return _mapper.Map<IPagedList<Announcement>, IPagedList<AnnouncementViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AnnouncementViewModel> GetModel(string id)
        {
            var entitiy = await _announcementRepository.GetModel(id);

            return _mapper.Map<Announcement, AnnouncementViewModel>(entitiy);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(AnnouncementAddViewModel model)
        {
            var entity = _mapper.Map<AnnouncementAddViewModel, Announcement>(model);
            if (await _announcementRepository.Add(entity) > 0)
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
            if (await _announcementRepository.Delete(id) > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }
    }
}
