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
    /// 咨询记录 -> 跟进记录
    /// </summary>
    public class CommunicationRecordService : ICommunicationRecordService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<CommunicationRecordService> _logger;

        /// <summary>
        /// IUser
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// ICommunicationRecordRepository
        /// </summary>
        private readonly ICommunicationRecordRepository _communicationRecordRepository;

        /// <summary>
        /// IConsultRecordRepository
        /// </summary>
        private readonly IConsultRecordRepository _consultRecordRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="communicationRecordRepository"></param>
        /// <param name="consultRecordRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        public CommunicationRecordService(
            IMapper mapper,
            ILogger<CommunicationRecordService> logger,
            ICurrentUser user,
            ICommunicationRecordRepository communicationRecordRepository,
            IConsultRecordRepository consultRecordRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _communicationRecordRepository = communicationRecordRepository;
            _consultRecordRepository = consultRecordRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="consultRecordId"></param>
        /// <returns></returns>
        public async Task<IPagedList<CommunicationRecordViewModel>> GetPageList(int page = 1, int limit = 15, string consultRecordId = "")
        {
            if (string.IsNullOrEmpty(consultRecordId))
            {
                consultRecordId = Guid.Empty.ToString();
            }

            var entities = await _communicationRecordRepository.GetPageList(page, limit, consultRecordId);

            return _mapper.Map<IPagedList<CommunicationRecord>, IPagedList<CommunicationRecordViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(CommunicationRecordAddViewModel model)
        {
            var consult = await _consultRecordRepository.GetModel(model.ConsultRecordId);

            if (null == consult)
            {
                return new Result { Success = false, Message = "操作失败：未找到咨询记录！" };
            }

            /*
             * TO DO
             * 1-本人才可以填写跟进记录
             * 2-未分配跟进人的不可以填写跟进记录
             * */

            var entity = _mapper.Map<CommunicationRecordAddViewModel, CommunicationRecord>(model);

            entity.TrackingStafferId = _user.UserId;
            entity.TrackingStafferName = _user.UserName;

            if (await _communicationRecordRepository.Add(entity, model.TrackingState))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
