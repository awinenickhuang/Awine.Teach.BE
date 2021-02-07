using AutoMapper;
using Awine.Framework.Core;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Awine.Framework.Core.LinqDynamicQueryable;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 咨询记录
    /// </summary>
    public class ConsultRecordService : IConsultRecordService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ConsultRecordService> _logger;

        /// <summary>
        /// IUser
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// IConsultRecordRepository
        /// </summary>
        private readonly IConsultRecordRepository _consultRecordRepository;

        /// <summary>
        /// IMarketingChannelRepository
        /// </summary>
        private readonly IMarketingChannelRepository _marketingChannelRepository;

        /// <summary>
        /// ICourseRepository
        /// </summary>
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="consultRecordRepository"></param>
        /// <param name="marketingChannelRepository"></param>
        /// <param name="courseRepository"></param>
        public ConsultRecordService(
            IMapper mapper,
            ILogger<ConsultRecordService> logger,
            ICurrentUser user,
            IConsultRecordRepository consultRecordRepository,
            IMarketingChannelRepository marketingChannelRepository,
            ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _consultRecordRepository = consultRecordRepository;
            _marketingChannelRepository = marketingChannelRepository;
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// 机构首页 -> 生源情况统计
        /// </summary>
        /// <returns></returns>
        public async Task<ConsultRecordStatisticalViewModel> ConsulRecordStatistical()
        {
            IEnumerable<ConsultRecord> consultRecords = await _consultRecordRepository.GetAll(_user.TenantId);
            return new ConsultRecordStatisticalViewModel
            {
                TotalAmount = consultRecords.Count(),
                TofollowupAmount = consultRecords.Where(x => x.TrackingState == 1).Count(),
                InthefollowupAmount = consultRecords.Where(x => x.TrackingState == 2).Count(),
                TradedAmount = consultRecords.Where(x => x.TrackingState == 6).Count(),
            };
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="counselingCourseId"></param>
        /// <param name="marketingChannelId"></param>
        /// <param name="trackingState"></param>
        /// <param name="trackingStafferId"></param>
        /// <returns></returns>
        public async Task<IPagedList<ConsultRecordViewModel>> GetPageList(int page = 1, int limit = 15, string name = "", string phoneNumber = "", string startTime = "", string endTime = "", string counselingCourseId = "", string marketingChannelId = "", int trackingState = 0, string trackingStafferId = "")
        {
            var entities = await _consultRecordRepository.GetPageList(page, limit, _user.TenantId, name, phoneNumber, startTime, endTime, counselingCourseId, marketingChannelId, trackingState, trackingStafferId);

            return _mapper.Map<IPagedList<ConsultRecord>, IPagedList<ConsultRecordViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ConsultRecordViewModel> GetModel(string id)
        {
            var entity = await _consultRecordRepository.GetModel(id);

            return _mapper.Map<ConsultRecord, ConsultRecordViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ConsultRecordAddViewModel model)
        {
            var entity = _mapper.Map<ConsultRecordAddViewModel, ConsultRecord>(model);
            entity.TenantId = _user.TenantId;

            var marketingChannel = await _marketingChannelRepository.GetModel(model.MarketingChannelId);
            if (null == marketingChannel)
            {
                return new Result { Success = false, Message = "未找到指定的营销渠道！" };
            }
            entity.MarketingChannelName = marketingChannel.Name;

            var reply = await _courseRepository.GetModel(model.CounselingCourseId);
            if (null == reply)
            {
                return new Result { Success = false, Message = "未找到指定的课程信息！" };
            }
            entity.CounselingCourseName = reply.Name;

            if (null != await _consultRecordRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            entity.CreatorId = _user.UserId;
            entity.CreatorName = _user.Name;

            if (await _consultRecordRepository.Add(entity) > 0)
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
        public async Task<Result> Update(ConsultRecordUpdateViewModel model)
        {
            var entity = _mapper.Map<ConsultRecordUpdateViewModel, ConsultRecord>(model);
            entity.TenantId = _user.TenantId;

            var marketingChannel = await _marketingChannelRepository.GetModel(model.MarketingChannelId);
            if (null == marketingChannel)
            {
                return new Result { Success = false, Message = "未找到指定的营销渠道！" };
            }
            entity.MarketingChannelName = marketingChannel.Name;
            var reply = await _courseRepository.GetModel(model.CounselingCourseId);
            if (null == reply)
            {
                return new Result { Success = false, Message = "未找到指定的课程信息！" };
            }
            entity.CounselingCourseName = reply.Name;
            if (null != await _consultRecordRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _consultRecordRepository.Update(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 跟进任务指派
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> TrackingAssigned(ConsultRecordTrackingAssignedViewModel model)
        {
            var record = await _consultRecordRepository.GetModel(model.ConsultRecordId);
            if (null == record)
            {
                return new Result { Success = false, Message = "未找到指定的咨询记录！" };
            }

            var consultRecord = new ConsultRecord()
            {
                Id = model.ConsultRecordId,
                TrackingStafferId = model.TrackingStafferId,
                TrackingStafferName = model.TrackingStafferName
            };

            if (await _consultRecordRepository.TrackingAssigned(consultRecord) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 生源情况统计 -> 绘图 -> 只统计当前月
        /// </summary>
        /// <param name="statisticalMmethod"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ConsultRecordChartViewModel>> ConsultRecordChartReport(int statisticalMmethod)
        {
            CurveChartViewModel curveChartViewModel = new CurveChartViewModel();
            var consultRecordEnumerable = await _consultRecordRepository.GetAll(tenantId: _user.TenantId);
            var consultRecordQuery = _mapper.Map<IEnumerable<ConsultRecord>, IEnumerable<ConsultRecordChartViewModel>>(consultRecordEnumerable);

            var dateFrom = TimeCalculate.GetTheFirstDayoftheCurrentMonth();
            consultRecordQuery = consultRecordQuery.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.GetTheLastDayoftheCurrentMonth();
            consultRecordQuery = consultRecordQuery.Where(x => x.CreateTime <= dateTo);

            // 根据天进行分组统计
            var groupBy = DynamicQueryable.GroupBy<ConsultRecordChartViewModel, dynamic>(consultRecordQuery.AsQueryable(), x => new { x.CreateTime.Day });

            consultRecordQuery = groupBy.Select(x => new ConsultRecordChartViewModel
            {
                CreateTime = x.FirstOrDefault().CreateTime,
                Quantity = x.Count(),
            }).ToObservableCollection();

            return consultRecordQuery;
        }
    }
}