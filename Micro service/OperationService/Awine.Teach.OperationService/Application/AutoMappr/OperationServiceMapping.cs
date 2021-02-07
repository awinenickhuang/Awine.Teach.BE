using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.OperationService.Application.ViewModels;
using Awine.Teach.OperationService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.OperationService.Application.AutoMappr
{
    /// <summary>
    /// 平台运营服务 -> 实体与视图模型映射配置
    /// </summary>
    public class OperationServiceMapping : Profile
    {
        /// <summary>
        /// 映射关系配置
        /// </summary>
        public OperationServiceMapping()
        {
            //平台公告
            CreateMap<Announcement, AnnouncementViewModel>().ReverseMap();
            CreateMap<AnnouncementAddViewModel, Announcement>();
            //CreateMap<AnnouncementUpdateViewModel, Announcement>();
            CreateMap<IPagedList<Announcement>, IPagedList<AnnouncementViewModel>>().ConvertUsing<PagedListConverter<Announcement, AnnouncementViewModel>>();

            //行业资讯
            CreateMap<News, NewsViewModel>().ReverseMap();
            CreateMap<NewsAddViewModel, News>();
            //CreateMap<NewsUpdateViewModel, News>();
            CreateMap<IPagedList<News>, IPagedList<NewsViewModel>>().ConvertUsing<PagedListConverter<News, NewsViewModel>>();

            //反馈信息
            CreateMap<Feedback, FeedbackViewModel>().ReverseMap();
            CreateMap<FeedbackAddViewModel, Feedback>();
            CreateMap<IPagedList<Feedback>, IPagedList<FeedbackViewModel>>().ConvertUsing<PagedListConverter<Feedback, FeedbackViewModel>>();

            //租户登录信息
            CreateMap<TenantLogging, TenantLoggingViewModel>().ReverseMap();
            CreateMap<IPagedList<TenantLogging>, IPagedList<TenantLoggingViewModel>>().ConvertUsing<PagedListConverter<TenantLogging, TenantLoggingViewModel>>();
        }
    }
}
