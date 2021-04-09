using AutoMapper;
using Awine.Framework.AspNetCore.Model;
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
            entity.CreatorName = _user.UserName;

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

        #region 数据统计

        /// <summary>
        /// 生源情况统计 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 指定月份的生源数量
        /// </remarks>
        public async Task<BasicLineChartViewModel> ConsultRecordMonthChartReport(string designatedMonth)
        {
            var consultRecords = await _consultRecordRepository.GetAll(tenantId: _user.TenantId);

            DateTime time = Convert.ToDateTime(designatedMonth);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            consultRecords = consultRecords.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            consultRecords = consultRecords.Where(x => x.CreateTime <= dateTo);

            //当前月天数
            int days = TimeCalculate.DaysInMonth(time);

            BasicLineChartViewModel basicLineChartViewModel = new BasicLineChartViewModel();

            for (int day = 1; day <= days; day++)
            {
                basicLineChartViewModel.XAxisData.Add($"{time.Year}-{time.Month}-{day}");
                basicLineChartViewModel.SeriesData.Add(consultRecords.Where(x => x.CreateTime.Year == time.Year
                && x.CreateTime.Month == time.Month && x.CreateTime.Day == day).Count());
            }

            return basicLineChartViewModel;
        }

        /// <summary>
        /// 课程咨询情况 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 各课程咨询数量
        /// </remarks>
        public async Task<PieChartViewModel> ConsultRecordCourseMonthChartReport(string designatedMonth)
        {
            PieChartViewModel pieChartResult = new PieChartViewModel();

            //查询咨询记录并按时间进行过滤
            var consultRecordEnumerable = await _consultRecordRepository.GetAll(tenantId: _user.TenantId);

            DateTime time = Convert.ToDateTime(designatedMonth);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            consultRecordEnumerable = consultRecordEnumerable.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            consultRecordEnumerable = consultRecordEnumerable.Where(x => x.CreateTime <= dateTo);

            //查询所有状态为启用的课程
            var courses = await _courseRepository.GetAll(tenantId: _user.TenantId, enabledStatus: 1);
            List<PieChartSeriesData> pieChartSeries = new List<PieChartSeriesData>();

            foreach (var course in courses)
            {
                pieChartResult.LegendData.Add(course.Name);

                PieChartSeriesData pieChartSeriesData = new PieChartSeriesData()
                {
                    Value = consultRecordEnumerable.Where(x => x.CounselingCourseId.Equals(course.Id)).Count(),
                    Name = course.Name
                };

                pieChartSeries.Add(pieChartSeriesData);
            }

            pieChartResult.SeriesData = pieChartSeries;

            return pieChartResult;
        }

        /// <summary>
        /// 营销转化情况 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 各课程咨询数量
        /// </remarks>
        public async Task<HorizontalBarChartViewModel> StudentTransformationtReport(string designatedMonth)
        {
            HorizontalBarChartViewModel horizontalBarChartResult = new HorizontalBarChartViewModel();

            //查询咨询记录并按时间进行过滤
            var consultRecordEnumerable = await _consultRecordRepository.GetAll(tenantId: _user.TenantId);

            DateTime time = Convert.ToDateTime(designatedMonth);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            consultRecordEnumerable = consultRecordEnumerable.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            consultRecordEnumerable = consultRecordEnumerable.Where(x => x.CreateTime <= dateTo);

            //跟进状态 1-待跟进 2-跟进中 3-已邀约 4-已试听 5-已到访 6-已成交
            horizontalBarChartResult.HorizontalBarChartYAxis.Add(new HorizontalBarChartYAxis { TrackingState = 6, Name = "已成交" });
            horizontalBarChartResult.HorizontalBarChartYAxis.Add(new HorizontalBarChartYAxis { TrackingState = 5, Name = "已到访" });
            horizontalBarChartResult.HorizontalBarChartYAxis.Add(new HorizontalBarChartYAxis { TrackingState = 4, Name = "已试听" });
            horizontalBarChartResult.HorizontalBarChartYAxis.Add(new HorizontalBarChartYAxis { TrackingState = 3, Name = "已邀约" });
            horizontalBarChartResult.HorizontalBarChartYAxis.Add(new HorizontalBarChartYAxis { TrackingState = 2, Name = "跟进中" });
            horizontalBarChartResult.HorizontalBarChartYAxis.Add(new HorizontalBarChartYAxis { TrackingState = 1, Name = "待跟进" });
            horizontalBarChartResult.HorizontalBarChartYAxis.Add(new HorizontalBarChartYAxis { TrackingState = 0, Name = "总生源数" });

            List<HorizontalBarChartSeries> series = new List<HorizontalBarChartSeries>();

            foreach (var trackingState in horizontalBarChartResult.HorizontalBarChartYAxis)
            {
                if (trackingState.TrackingState > 0)
                {
                    HorizontalBarChartSeries seriesData = new HorizontalBarChartSeries()
                    {
                        Count = consultRecordEnumerable.Where(x => x.TrackingState == trackingState.TrackingState).Count()
                    };
                    series.Add(seriesData);
                }
                else
                {
                    HorizontalBarChartSeries seriesData = new HorizontalBarChartSeries()
                    {
                        Count = consultRecordEnumerable.Count()
                    };
                    series.Add(seriesData);
                }
            }

            horizontalBarChartResult.HorizontalBarChartSeries = series;

            return horizontalBarChartResult;
        }

        /// <summary>
        /// 各营销渠道生源来源情况 -> 绘图 -> 统计指定月份
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        /// <remarks>
        /// 各招生渠道指定月份的新增生源数量
        /// </remarks>
        public async Task<StackedLineChartViewModel> StudentSourceChannelReport(string designatedMonth)
        {
            StackedLineChartViewModel chartResult = new StackedLineChartViewModel();

            DateTime time = Convert.ToDateTime(designatedMonth);

            //查询生源渠道
            var channels = await _marketingChannelRepository.GetAll(tenantId: _user.TenantId);
            foreach (var channel in channels)
            {
                //生源渠道数据
                chartResult.Legend.Data.Add(channel.Name);
                //生成Series
                chartResult.Series.Add(new StackedLineChartSeries()
                {
                    Id = channel.Id,
                    Name = channel.Name
                });
            }

            //查询咨询记录并按时间进行过滤
            var consultRecords = await _consultRecordRepository.GetAll(tenantId: _user.TenantId);

            var dateFrom = TimeCalculate.FirstDayOfMonth(time);
            consultRecords = consultRecords.Where(x => x.CreateTime >= dateFrom);

            var dateTo = TimeCalculate.LastDayOfMonth(time);
            consultRecords = consultRecords.Where(x => x.CreateTime <= dateTo);

            //生成 XAxis 坐标点
            int days = TimeCalculate.DaysInMonth(time);
            //初始X坐标点数据
            List<string> xAxisData = new List<string>();
            for (int day = 1; day <= days; day++)
            {
                chartResult.XAxis.Data.Add($"{time.Year}-{time.Month}-{day}");
                //每次生成 XAxis 时，都需要向 Series 的 每个 Data 写入当前坐标点（时间）的统计数据
                foreach (var ser in chartResult.Series)
                {
                    //填充 图表 数据
                    ser.Data.Add(consultRecords.Where(x => x.MarketingChannelId.Equals(ser.Id) && x.CreateTime.Year == time.Year && x.CreateTime.Month == time.Month && x.CreateTime.Day == day).Count());
                }
            }
            return chartResult;
        }

        #endregion
    }
}