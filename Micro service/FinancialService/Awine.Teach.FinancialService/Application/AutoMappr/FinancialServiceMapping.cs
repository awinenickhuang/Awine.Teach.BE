using System;
using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.FinancialService.Application.ViewModels;
using Awine.Teach.FinancialService.Domain;

namespace Awine.Teach.FinancialService.Application.AutoMappr
{
    /// <summary>
    /// 财务服务 -> 实体与视图模型映射配置
    /// </summary>
    public class FinancialServiceMapping : Profile
    {
        /// <summary>
        /// 映射关系配置
        /// </summary>
        public FinancialServiceMapping()
        {
            //财务收支项目
            CreateMap<FinancialItem, FinancialItemViewModel>().ReverseMap();
            CreateMap<FinancialItemAddViewModel, FinancialItem>();
            CreateMap<FinancialItemUpdateViewModel, FinancialItem>();
            CreateMap<IPagedList<FinancialItem>, IPagedList<FinancialItemViewModel>>().ConvertUsing<PagedListConverter<FinancialItem, FinancialItemViewModel>>();

            //日常支出
            CreateMap<DailySpending, DailySpendingViewModel>().ReverseMap();
            CreateMap<DailySpendingAddViewModel, DailySpending>();
            CreateMap<DailySpendingUpdateViewModel, DailySpending>();
            CreateMap<IPagedList<DailySpending>, IPagedList<DailySpendingViewModel>>().ConvertUsing<PagedListConverter<DailySpending, DailySpendingViewModel>>();
        }
    }
}
