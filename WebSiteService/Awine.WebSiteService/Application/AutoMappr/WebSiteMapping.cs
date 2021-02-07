using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.WebSite.Applicaton.ViewModels;
using Awine.WebSite.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.WebSite.Applicaton.AutoMappr
{
    /// <summary>
    /// WebSiteMapping
    /// </summary>
    public class WebSiteMapping : Profile
    {
        /// <summary>
        /// 映射关系配置
        /// </summary>
        public WebSiteMapping()
        {
            //管理员
            CreateMap<Manager, ManagerViewModel>().ReverseMap();
            CreateMap<ManagerAddViewModel, Manager>();
            CreateMap<ManagerUpdateViewModel, Manager>();
            CreateMap<ManagerUpdatePasswordViewModel, Manager>();
            CreateMap<IPagedList<Manager>, IPagedList<ManagerViewModel>>().ConvertUsing<PagedListConverter<Manager, ManagerViewModel>>();

            //横幅图片
            CreateMap<Banner, BannerViewModel>().ReverseMap();
            CreateMap<BannerAddViewModel, Banner>();
            CreateMap<IPagedList<Banner>, IPagedList<BannerViewModel>>().ConvertUsing<PagedListConverter<Banner, BannerViewModel>>();

            //版块管理
            CreateMap<Forum, ForumViewModel>().ReverseMap();
            CreateMap<ForumAddViewModel, Forum>();
            CreateMap<ForumUpdateViewModel, Forum>();
            CreateMap<Forum, ForumTreeViewModel>();
            CreateMap<IPagedList<Forum>, IPagedList<ForumViewModel>>().ConvertUsing<PagedListConverter<Forum, ForumViewModel>>();

            //文章管理
            CreateMap<Article, ArticleViewModel>().ReverseMap();
            CreateMap<ArticleAddViewModel, Article>();
            CreateMap<ArticleUpdateViewModel, Article>();
            CreateMap<IPagedList<Article>, IPagedList<ArticleViewModel>>().ConvertUsing<PagedListConverter<Article, ArticleViewModel>>();

            //网站设置
            CreateMap<Settings, SettingsViewModel>().ReverseMap();
            CreateMap<SettingsAddViewModel, Settings>();
            CreateMap<SettingsUpdateViewModel, Settings>();
            CreateMap<IPagedList<Settings>, IPagedList<SettingsViewModel>>().ConvertUsing<PagedListConverter<Settings, SettingsViewModel>>();
        }
    }
}
