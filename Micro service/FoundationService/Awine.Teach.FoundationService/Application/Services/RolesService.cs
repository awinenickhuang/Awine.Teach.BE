using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Awine.Teach.FoundationService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Domain.Models;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RolesService : IRolesService
    {
        /// <summary>
        /// 角色管理
        /// </summary>
        private readonly IRolesRepository _rolesRepository;

        /// <summary>
        /// 角色权限
        /// </summary>
        private readonly IRolesOwnedModulesRepository _rolesOwnedModulesRepository;

        /// <summary>
        /// 角色声明
        /// </summary>
        private readonly IRolesClaimsRepository _rolesClaimsRepository;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly IUsersRepository _usersRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<RolesService> _logger;

        /// <summary>
        /// 当前用户
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="rolesRepository"></param>
        /// <param name="rolesOwnedModulesRepository"></param>
        /// <param name="rolesClaimsRepository"></param>
        /// <param name="usersRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        public RolesService(IRolesRepository rolesRepository,
            IRolesOwnedModulesRepository rolesOwnedModulesRepository,
            IRolesClaimsRepository rolesClaimsRepository,
            IUsersRepository usersRepository,
            IMapper mapper, ILogger<RolesService> logger, ICurrentUser user)
        {
            _rolesRepository = rolesRepository;
            _rolesOwnedModulesRepository = rolesOwnedModulesRepository;
            _rolesClaimsRepository = rolesClaimsRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
            _logger = logger;
            _user = user;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<RolesViewModel>> GetPageList(int pageIndex = 1, int pageSize = 15, string tenantId = "")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                tenantId = _user.TenantId;
            }
            var entities = await _rolesRepository.GetPageList(pageIndex, pageSize, tenantId);
            return _mapper.Map<IPagedList<Roles>, IPagedList<RolesViewModel>>(entities);
        }

        /// <summary>
        /// 取所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RolesViewModel>> GetAll(string tenantId = "")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                tenantId = _user.TenantId;
            }
            var entities = await _rolesRepository.GetAll(tenantId);
            return _mapper.Map<IEnumerable<Roles>, IEnumerable<RolesViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(RolesAddViewModel model)
        {
            var entity = _mapper.Map<RolesAddViewModel, Roles>(model);
            entity.NormalizedName = entity.Name.ToLower();
            entity.ConcurrencyStamp = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(entity.TenantId))
            {
                entity.TenantId = _user.TenantId;
            }

            if (null != await _rolesRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _rolesRepository.Add(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(string id)
        {
            var existing_role = await _rolesRepository.GetModel(id);
            if (null == existing_role)
            {
                return new Result { Success = true, Message = "找不到角色信息！" };
            }
            if (existing_role.Identifying == 1)
            {
                return new Result { Success = true, Message = "平台管理员角色不允许删除！" };
            }

            //对于管理员类型的角色，只有平台超管可以删除
            if (existing_role.Identifying == 2 || existing_role.Identifying == 3)
            {
                var currentRole = await _rolesRepository.GetModel(_user.RoleId);
                if (null == currentRole)
                {
                    return new Result { Success = true, Message = "无法识你的身份，操作失败！" };
                }
                if (currentRole.Identifying != 1)
                {
                    return new Result { Success = true, Message = "你无权删除管理员角色，操作失败！" };
                }
            }

            var roleOwnedModules = await _rolesOwnedModulesRepository.GetRoleOwnedModules(id);
            if (roleOwnedModules.Count() > 0)
            {
                return new Result { Success = true, Message = "角色设置了模块，不允许删除！" };
            }

            var roleClaims = await _rolesClaimsRepository.GetAll(id);
            if (roleClaims.Count() > 0)
            {
                return new Result { Success = true, Message = "角色设置了权限，不允许删除！" };
            }

            var users = await _usersRepository.GetAllInRole(id);
            if (users.Count() > 0)
            {
                return new Result { Success = true, Message = "角色被用户使用，不允许删除！" };
            }

            var result = await _rolesRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RolesViewModel> GetModel(string id)
        {
            var entity = await _rolesRepository.GetModel(id);
            return _mapper.Map<Roles, RolesViewModel>(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(RolesUpdateViewModel model)
        {

            var entity = _mapper.Map<RolesUpdateViewModel, Roles>(model);

            if (null != await _rolesRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _rolesRepository.Update(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 保存 -> 角色拥有的模块信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> SaveRoleOwnedModules(RolesOwnedModulesAddViewModel model)
        {
            IList<Domain.Models.RolesOwnedModules> rolesOwnedModules = new List<Domain.Models.RolesOwnedModules>();
            IList<RolesClaims> aspnetroleClaims = new List<RolesClaims>();

            if (string.IsNullOrEmpty(model.RoleId))
            {
                return new Result { Success = false, Message = "操作失败：角色标识不能为空！" };
            }

            var existRole = await _rolesRepository.GetModel(model.RoleId);
            if (null == existRole)
            {
                return new Result { Success = false, Message = "操作失败：角色不存在！" };
            }

            foreach (var item in model.RolesOwnedModules)
            {
                var module = new Domain.Models.RolesOwnedModules()
                {
                    RoleId = model.RoleId,
                    ModuleId = item.ModuleId,
                    TenantId = existRole.TenantId
                };
                rolesOwnedModules.Add(module);
            }

            foreach (var item in model.RoleClaims)
            {
                var module = new RolesClaims()
                {
                    RoleId = model.RoleId,
                    ClaimType = item.ClaimValue,
                    ClaimValue = item.ClaimValue
                };
                aspnetroleClaims.Add(module);
            }

            var result = await _rolesOwnedModulesRepository.SaveRoleOwnedModules(model.RoleId, rolesOwnedModules, aspnetroleClaims);

            if (result)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 查询 -> 角色拥有的模块信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IList<RolesOwnedModulesViewModel>> GetRoleOwnedModules(string roleId)
        {
            IList<RolesOwnedModulesViewModel> result = new List<RolesOwnedModulesViewModel>();

            var rolesOwnedModules = await _rolesOwnedModulesRepository.GetRoleOwnedModules(roleId);

            foreach (var item in rolesOwnedModules)
            {
                var rolesOwnedModule = new RolesOwnedModulesViewModel()
                {
                    Id = item.Id,
                    RoleId = item.RoleId,
                    ModuleId = item.ModuleId
                };
                result.Add(rolesOwnedModule);
            }

            return result;
        }
    }
}
