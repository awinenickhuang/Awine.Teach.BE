using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Awine.Teach.FoundationService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Domain.Models;
using System.Linq;
using Awine.Framework.Core.Cryptography;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 租户信息管理
    /// </summary>
    public class TenantsService : ITenantsService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<TenantsService> _logger;

        /// <summary>
        /// 当前用户
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 租户
        /// </summary>
        private readonly ITenantsRepository _tenantsRepository;

        /// <summary>
        /// 行业
        /// </summary>
        private readonly IIndustryCategoryRepository _industryCategoryRepository;

        /// <summary>
        /// 区域
        /// </summary>
        private readonly IAdministrativeDivisionsRepository _administrativeDivisionsRepository;

        /// <summary>
        /// SaaSSaaS版本
        /// </summary>
        private readonly ISaaSVersionRepository _applicationVersionRepository;

        /// <summary>
        /// SaaSSaaS版本定价策略
        /// </summary>
        private readonly ISaaSPricingTacticsRepository _applicationPricingTacticsRepository;

        /// <summary>
        /// SaaSSaaS版本包含的系统模块信息
        /// </summary>
        private readonly ISaaSVersionOwnedModuleRepository _applicationVersionOwnedModuleRepository;

        /// <summary>
        /// 租户默认参数配置信息
        /// </summary>
        private readonly ITenantDefaultSettingsRepository _tenantDefaultSettingsRepository;

        /// <summary>
        /// 租户参数配置信息
        /// </summary>
        private readonly ITenantSettingsRepository _tenantSettingsRepository;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly IUsersRepository _usersRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="tenantsRepository"></param>
        /// <param name="industryCategoryRepository"></param>
        /// <param name="administrativeDivisionsRepository"></param>
        /// <param name="applicationVersionRepository"></param>
        /// <param name="applicationPricingTacticsRepository"></param>
        /// <param name="applicationVersionOwnedModuleRepository"></param>
        /// <param name="tenantDefaultSettingsRepository"></param>
        /// <param name="tenantSettingsRepository"></param>
        /// <param name="usersRepository"></param>
        public TenantsService(
            IMapper mapper,
            ILogger<TenantsService> logger,
            ICurrentUser user,
            ITenantsRepository tenantsRepository,
            IIndustryCategoryRepository industryCategoryRepository,
            IAdministrativeDivisionsRepository administrativeDivisionsRepository,
            ISaaSVersionRepository applicationVersionRepository,
            ISaaSPricingTacticsRepository applicationPricingTacticsRepository,
            ISaaSVersionOwnedModuleRepository applicationVersionOwnedModuleRepository,
            ITenantDefaultSettingsRepository tenantDefaultSettingsRepository,
            ITenantSettingsRepository tenantSettingsRepository,
            IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _tenantsRepository = tenantsRepository;
            _industryCategoryRepository = industryCategoryRepository;
            _administrativeDivisionsRepository = administrativeDivisionsRepository;
            _applicationVersionRepository = applicationVersionRepository;
            _applicationPricingTacticsRepository = applicationPricingTacticsRepository;
            _applicationVersionOwnedModuleRepository = applicationVersionOwnedModuleRepository;
            _tenantDefaultSettingsRepository = tenantDefaultSettingsRepository;
            _tenantSettingsRepository = tenantSettingsRepository;
            _usersRepository = usersRepository;
        }

        /// <summary>
        /// 全部数据
        /// </summary>
        /// <param name="classiFication"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="status"></param>
        /// <param name="industryId"></param>
        /// <param name="creatorId"></param>
        /// <param name="creatorTenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TenantsViewModel>> GetAll(int classiFication = 0, string saaSVersionId = "", int status = 0, string industryId = "", string creatorId = "", string creatorTenantId = "")
        {
            var tenantId = string.Empty;

            if (string.IsNullOrEmpty(creatorTenantId))
            {
                if (_user.TenantClassiFication == 3)
                {
                    creatorTenantId = "";                  //运营商查询所有
                }
                else if (_user.TenantClassiFication == 2)
                {
                    creatorTenantId = _user.TenantId;      //代理商只能查询自己的机构
                }
                else                                       //机构只能查询自己所在机构
                {
                    creatorTenantId = "";
                    tenantId = _user.TenantId;
                }
            }

            var entities = await _tenantsRepository.GetAll(tenantId, classiFication, saaSVersionId, status, industryId, creatorId, creatorTenantId);

            return _mapper.Map<IEnumerable<Tenants>, IEnumerable<TenantsViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="classiFication"></param>
        /// <param name="saaSVersionId"></param>
        /// <param name="status"></param>
        /// <param name="industryId"></param>
        /// <param name="creatorId"></param>
        /// <param name="creatorTenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<TenantsViewModel>> GetPageList(int page = 1, int limit = 15, int classiFication = 0, string saaSVersionId = "", int status = 0, string industryId = "", string creatorId = "", string creatorTenantId = "")
        {
            var tenantId = string.Empty;

            if (string.IsNullOrEmpty(creatorTenantId))
            {
                if (_user.TenantClassiFication == 3)
                {
                    creatorTenantId = "";                  //运营商查询所有
                }
                else if (_user.TenantClassiFication == 2)
                {
                    creatorTenantId = _user.TenantId;      //代理商只能查询自己的机构
                }
                else                                       //机构只能查询自己所在机构
                {
                    creatorTenantId = "";
                    tenantId = _user.TenantId;
                }
            }

            var entities = await _tenantsRepository.GetPageList(page, limit, tenantId, classiFication, saaSVersionId, status, industryId, creatorId, creatorTenantId);

            return _mapper.Map<IPagedList<Tenants>, IPagedList<TenantsViewModel>>(entities);
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TenantsViewModel> GetModel(string id)
        {
            var entity = await _tenantsRepository.GetModel(id);
            return _mapper.Map<Tenants, TenantsViewModel>(entity);
        }

        /// <summary>
        /// 租户开通
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(TenantsAddViewModel model)
        {
            if (model.ClassiFication > 1)
            {
                if (_user.TenantClassiFication != 3)
                {
                    return new Result { Success = false, Message = "账号未设置开通代理商权限！" };
                }
            }
            var existUser = await _usersRepository.GetModel(account: model.ContactsPhone, phoneNumber: model.ContactsPhone);

            if (null != existUser)
            {
                return new Result { Success = false, Message = "手机号码已被占用！" };
            }

            var existTenant = await _tenantsRepository.GetModel(new Tenants() { Name = model.Name });

            if (null != existTenant)
            {
                return new Result { Success = false, Message = "机构名称已被占用！" };
            }

            var industry = await _industryCategoryRepository.GetModel(model.IndustryId);

            if (null == industry)
            {
                return new Result { Success = false, Message = "未找到行业信息！" };
            }

            //购买版本
            var version = await _applicationVersionRepository.GetModel(model.SaaSVersionId);
            if (null == version)
            {
                return new Result { Success = false, Message = "未找到版本信息！" };
            }
            //购买版本 -> 版本定价策略
            var pricingTactics = await _applicationPricingTacticsRepository.GetModel(model.PricingTacticsId);
            if (null == pricingTactics)
            {
                return new Result { Success = false, Message = "未找到购买版本定价策略信息！" };
            }

            //版本对应的参数
            var tenantDefaultSettings = await _tenantDefaultSettingsRepository.GetModelForSaaSVersion(version.Id);
            if (null == tenantDefaultSettings)
            {
                return new Result { Success = false, Message = "未找到购买版本的设置信息！" };
            }

            //租户信息
            var tenant = _mapper.Map<TenantsAddViewModel, Tenants>(model);

            tenant.IndustryName = industry.Name;
            tenant.ClassiFication = 1;
            tenant.SaaSVersionName = version.Name;
            tenant.Status = 1;
            tenant.VIPExpirationTime = DateTime.Now.AddYears(pricingTactics.NumberOfYears);//过期时间为当前时间加购买年数
            tenant.CreatorId = _user.UserId;
            tenant.Creator = _user.UserName;
            tenant.CreatorTenantId = _user.TenantId;
            tenant.CreatorTenantName = _user.TenantName;

            var province = await _administrativeDivisionsRepository.GetModelByCode(tenant.ProvinceId);
            if (null == province)
            {
                return new Result { Success = false, Message = "未找到省信息！" };
            }
            tenant.ProvinceName = province.Name;

            var city = await _administrativeDivisionsRepository.GetModelByCode(tenant.CityId);
            if (null == city)
            {
                return new Result { Success = false, Message = "未找到市信息！" };
            }
            tenant.CityName = city.Name;

            var district = await _administrativeDivisionsRepository.GetModelByCode(tenant.DistrictId);
            if (null == district)
            {
                return new Result { Success = false, Message = "未找到区信息！" };
            }
            tenant.DistrictName = district.Name;

            //租户管理员角色
            var role = new Roles()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "管理员",
                NormalizedName = "TenantAdmin",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Identifying = 3,
                TenantId = tenant.Id,
                IsDeleted = false,
                CreateTime = DateTime.Now
            };

            //赋予租户管理员角色操作权限（模块权限）
            IList<Domain.Models.RolesOwnedModules> rolesOwnedModules = new List<Domain.Models.RolesOwnedModules>();
            var versionModules = await _applicationVersionOwnedModuleRepository.GetSaaSVersionOwnedModules(version.Id);
            if (versionModules.Count() <= 0)
            {
                return new Result { Success = false, Message = "开通失败，未找到版本设置信息！" };
            }
            foreach (var m in versionModules)
            {
                rolesOwnedModules.Add(new Domain.Models.RolesOwnedModules()
                {
                    Id = Guid.NewGuid().ToString(),
                    TenantId = tenant.Id,
                    ModuleId = m.ModuleId,
                    RoleId = role.Id
                });
            }

            //创建机构信息
            Departments departments = new Departments()
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = Guid.Empty.ToString(),
                Name = tenant.Name,
                Describe = tenant.Name,
                DisplayOrder = 1,
                TenantId = tenant.Id,
                IsDeleted = false,
                CreateTime = tenant.CreateTime
            };

            //创建租户账号
            Users user = new Users()
            {
                AccessFailedCount = 0,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Email = "",
                EmailConfirmed = false,
                LockoutEnabled = true,
                LockoutEnd = DateTime.Now,
                NormalizedEmail = "",
                NormalizedUserName = tenant.Contacts,
                UserName = tenant.Contacts,
                Account = tenant.ContactsPhone,
                PasswordHash = PasswordManager.HashPassword(model.PasswordHash),
                PhoneNumber = tenant.ContactsPhone,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
                Gender = 0,
                IsActive = true,
                TenantId = tenant.Id,
                RoleId = role.Id,
                DepartmentId = departments.Id
            };

            //机构设置
            var tenantSettings = new TenantSettings()
            {
                MaxNumberOfBranch = tenantDefaultSettings.MaxNumberOfBranch,
                MaxNumberOfDepartments = tenantDefaultSettings.MaxNumberOfDepartments,
                MaxNumberOfRoles = tenantDefaultSettings.MaxNumberOfRoles,
                MaxNumberOfUser = tenantDefaultSettings.MaxNumberOfUser,
                MaxNumberOfCourse = tenantDefaultSettings.MaxNumberOfCourse,
                MaxNumberOfClass = tenantDefaultSettings.MaxNumberOfClass,
                MaxNumberOfClassRoom = tenantDefaultSettings.MaxNumberOfClassRoom,
                MaxNumberOfStudent = tenantDefaultSettings.MaxNumberOfStudent,
                MaxStorageSpace = tenantDefaultSettings.MaxStorageSpace,
                AvailableStorageSpace = tenantDefaultSettings.MaxStorageSpace,
                UsedStorageSpace = 0,
                TenantId = tenant.Id
            };

            //创建订单
            Orders order = new Orders()
            {
                TenantId = tenant.Id,
                TenantName = tenant.Name,
                NumberOfYears = pricingTactics.NumberOfYears,
                PayTheAmount = pricingTactics.ChargeRates,
                PerformanceOwnerId = _user.UserId,
                PerformanceOwner = _user.UserName,
                PerformanceTenantId = _user.TenantId,
                PerformanceTenant = _user.TenantName,
                TradeCategories = 1,
                SaaSVersionId = version.Id,
                SaaSVersionName = version.Name
            };

            if (!string.IsNullOrEmpty(model.PerformanceOwnerId))
            {
                var performanceOwner = await _usersRepository.GetModel(model.PerformanceOwnerId);
                if (null == performanceOwner)
                {
                    return new Result { Success = false, Message = "未找到业务归属人！" };
                }
                order.PerformanceOwnerId = performanceOwner.Id;
                order.PerformanceOwner = performanceOwner.UserName;
            }

            if (await _tenantsRepository.Add(tenant, departments, user, role, rolesOwnedModules, tenantSettings, order))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新 -> 基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(TenantsUpdateViewModel model)
        {
            try
            {
                var entity = _mapper.Map<TenantsUpdateViewModel, Tenants>(model);

                if (await _tenantsRepository.Update(entity) > 0)
                {
                    return new Result { Success = true, Message = "操作成功！" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"发生异常{ex.ToString()}");
                return new Result { Success = false, Message = $"操作失败！{ex.ToString()}" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新 -> 租户状态 1-正常 2-锁定（异常）3-锁定（过期）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdateStatus(TenantsUpdateStatusViewModel model)
        {
            var exist = await _tenantsRepository.GetModel(model.Id);

            if (null == exist)
            {
                return new Result { Success = false, Message = "未找到机构信息！" };
            }

            var entity = new Tenants()
            {
                Id = model.Id,
                Status = model.Status
            };

            var result = await _tenantsRepository.UpdateStatus(entity);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
