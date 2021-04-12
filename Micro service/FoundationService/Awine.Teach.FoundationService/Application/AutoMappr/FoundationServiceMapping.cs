using System;
using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ViewModels;
using Awine.Teach.FoundationService.Domain.Models;

namespace Awine.Teach.FoundationService.Application.AutoMappr
{
    /// <summary>
    /// 基础服务 -> 实体与视图模型映射配置
    /// </summary>
    public class FoundationServiceMapping : Profile
    {
        /// <summary>
        /// 映射关系配置
        /// </summary>
        public FoundationServiceMapping()
        {
            //行政区域
            CreateMap<AdministrativeDivisions, AdministrativeDivisionsViewModel>().ReverseMap();

            //行业数据
            CreateMap<IndustryCategory, IndustryCategoryViewModel>().ReverseMap();
            CreateMap<IndustryCategoryAddViewModel, IndustryCategory>();
            CreateMap<IPagedList<IndustryCategory>, IPagedList<IndustryCategoryViewModel>>().ConvertUsing<PagedListConverter<IndustryCategory, IndustryCategoryViewModel>>();

            //租户信息
            CreateMap<Tenants, TenantsViewModel>();
            CreateMap<TenantsAddViewModel, Tenants>();
            CreateMap<TenantsUpdateViewModel, Tenants>();
            CreateMap<IPagedList<Tenants>, IPagedList<TenantsViewModel>>().ConvertUsing<PagedListConverter<Tenants, TenantsViewModel>>();

            //租户设置 - 系统默认
            CreateMap<TenantDefaultSettings, TenantDefaultSettingsViewModel>().ReverseMap();
            CreateMap<TenantDefaultSettingsAddViewModel, TenantDefaultSettings>();
            CreateMap<TenantDefaultSettingsUpdateViewModel, TenantDefaultSettings>();
            CreateMap<IPagedList<TenantDefaultSettings>, IPagedList<TenantDefaultSettingsViewModel>>().ConvertUsing<PagedListConverter<TenantDefaultSettings, TenantDefaultSettingsViewModel>>();

            //租户设置 - 单个租户
            CreateMap<TenantSettings, TenantSettingsViewModel>();
            CreateMap<TenantSettingsAddViewModel, TenantSettings>();
            CreateMap<TenantSettingsUpdateViewModel, TenantSettings>();
            CreateMap<IPagedList<TenantSettings>, IPagedList<TenantSettingsViewModel>>().ConvertUsing<PagedListConverter<TenantSettings, TenantSettingsViewModel>>();

            //部门信息
            CreateMap<DepartmentsAddViewModel, Departments>();
            CreateMap<DepartmentsUpdateViewModel, Departments>();
            CreateMap<DepartmentsViewModel, Departments>();
            CreateMap<Departments, DepartmentsViewModel>();
            CreateMap<Departments, DepartmentsTreeViewModel>().ForMember(dest => dest.Title, options => options.MapFrom(src => src.Name));
            CreateMap<Departments, DepartmentsXMSelectViewModel>();
            CreateMap<IPagedList<Departments>, IPagedList<DepartmentsViewModel>>().ConvertUsing<PagedListConverter<Departments, DepartmentsViewModel>>();

            //模块信息
            CreateMap<ModulesAddViewModel, Modules>();
            CreateMap<ModulesUpdateViewModel, Modules>();
            CreateMap<Modules, ModulesViewModel>();
            CreateMap<Modules, ModulesTreeViewModel>();
            CreateMap<Modules, ModulesWithCheckedStatusViewModel>();
            CreateMap<IPagedList<Modules>, IPagedList<ModulesViewModel>>().ConvertUsing<PagedListConverter<Modules, ModulesViewModel>>();

            //按钮信息
            CreateMap<ButtonsAddViewModel, Buttons>();
            CreateMap<Buttons, ButtonsViewModel>().ReverseMap();
            CreateMap<IPagedList<Buttons>, IPagedList<ButtonsViewModel>>().ConvertUsing<PagedListConverter<Buttons, ButtonsViewModel>>();

            //角色信息
            CreateMap<RolesAddViewModel, Roles>();
            CreateMap<RolesUpdateViewModel, Roles>();
            CreateMap<Roles, RolesViewModel>().ForMember(dest => dest.TenantName, options => options.MapFrom(src => src.PlatformTenant.Name));
            CreateMap<IPagedList<Roles>, IPagedList<RolesViewModel>>().ConvertUsing<PagedListConverter<Roles, RolesViewModel>>();

            //角色声明信息
            CreateMap<RolesClaims, RolesClaimsViewModel>();

            //用户信息
            CreateMap<UsersAddViewModel, Users>();
            CreateMap<UsersViewModel, Users>().ReverseMap();
            CreateMap<UsersUpdateViewModel, Users>();
            CreateMap<Users, UsersViewModel>()
                .ForMember(dest => dest.TenantName, options => options.MapFrom(src => src.PlatformTenant.Name))
                .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.AspnetRoleName, options => options.MapFrom(src => src.AspnetRole.Name));
            CreateMap<IPagedList<Users>, IPagedList<UsersViewModel>>().ConvertUsing<PagedListConverter<Users, UsersViewModel>>();

            //SaaS版本
            CreateMap<SaaSVersion, SaaSVersionViewModel>().ReverseMap();
            CreateMap<SaaSVersionAddViewModel, SaaSVersion>();
            CreateMap<SaaSVersionUpdateViewModel, SaaSVersion>();
            CreateMap<IPagedList<SaaSVersion>, IPagedList<SaaSVersionViewModel>>().ConvertUsing<PagedListConverter<SaaSVersion, SaaSVersionViewModel>>();

            //SaaS版本定价策略
            CreateMap<SaaSPricingTactics, SaaSPricingTacticsViewModel>().ReverseMap();
            CreateMap<SaaSPricingTacticsAddViewModel, SaaSPricingTactics>();
            CreateMap<SaaSPricingTacticsUpdateViewModel, SaaSPricingTactics>();
            CreateMap<IPagedList<SaaSPricingTactics>, IPagedList<SaaSPricingTacticsViewModel>>().ConvertUsing<PagedListConverter<SaaSPricingTactics, SaaSPricingTacticsViewModel>>();

            //SaaS版本包括的模块信息
            CreateMap<SaaSVersionOwnedModule, SaaSVersionOwnedModuleViewModel>();

            //订单信息
            CreateMap<Orders, OrdersViewModel>();
            CreateMap<IPagedList<Orders>, IPagedList<OrdersViewModel>>().ConvertUsing<PagedListConverter<Orders, OrdersViewModel>>();
        }
    }
}
