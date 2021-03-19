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
            CreateMap<TenantsEnterViewModel, Tenants>();
            CreateMap<TenantsUpdateViewModel, Tenants>();
            CreateMap<IPagedList<Tenants>, IPagedList<TenantsViewModel>>().ConvertUsing<PagedListConverter<Tenants, TenantsViewModel>>();

            //部门信息
            CreateMap<DepartmentsAddViewModel, Departments>();
            CreateMap<DepartmentsUpdateViewModel, Departments>();
            CreateMap<DepartmentsViewModel, Departments>();
            CreateMap<Departments, DepartmentsViewModel>()
                .ForMember(dest => dest.TenantName, options => options.MapFrom(src => src.Tenant.Name));
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

            //应用版本
            CreateMap<ApplicationVersion, ApplicationVersionViewModel>().ReverseMap();
            CreateMap<ApplicationVersionAddViewModel, ApplicationVersion>();
            CreateMap<ApplicationVersionUpdateViewModel, ApplicationVersion>();
            CreateMap<IPagedList<ApplicationVersion>, IPagedList<ApplicationVersionViewModel>>().ConvertUsing<PagedListConverter<ApplicationVersion, ApplicationVersionViewModel>>();

            //应用版本包括的模块信息
            CreateMap<ApplicationVersionOwnedModule, ApplicationVersionOwnedModuleViewModel>();
        }
    }
}
