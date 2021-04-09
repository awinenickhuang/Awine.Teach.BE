using AutoMapper;
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
using Awine.Framework.Core.Collections;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 系统模块
    /// </summary>
    public class ModulesService : IModulesService
    {
        /// <summary>
        /// IModulesRepository
        /// </summary>
        public readonly IModulesRepository _modulesRepository;

        /// <summary>
        /// IButtonsRepository
        /// </summary>
        public readonly IButtonsRepository _buttonsRepository;

        /// <summary>
        /// 角色权限
        /// </summary>
        private readonly IRolesOwnedModulesRepository _rolesOwnedModulesRepository;

        /// <summary>
        /// SaaS版本信息
        /// </summary>
        private readonly ISaaSVersionRepository _applicationVersionRepository;

        /// <summary>
        /// SaaS版本包括的模块信息
        /// </summary>
        private readonly ISaaSVersionOwnedModuleRepository _applicationVersionOwnedModuleRepository;

        /// <summary>
        /// 角色信息
        /// </summary>
        private readonly IRolesRepository _rolesRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ModulesService> _logger;

        /// <summary>
        /// 当前用户
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="modulesRepository"></param>
        /// <param name="buttonsRepository"></param>
        /// <param name="rolesOwnedModulesRepository"></param>
        /// <param name="applicationVersionRepository"></param>
        /// <param name="applicationVersionOwnedModuleRepository"></param>
        /// <param name="rolesRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        public ModulesService(IModulesRepository modulesRepository,
            IButtonsRepository buttonsRepository,
            IRolesOwnedModulesRepository rolesOwnedModulesRepository,
            ISaaSVersionRepository applicationVersionRepository,
            ISaaSVersionOwnedModuleRepository applicationVersionOwnedModuleRepository,
            IRolesRepository rolesRepository,
            IMapper mapper, ILogger<ModulesService> logger, ICurrentUser user)
        {
            _modulesRepository = modulesRepository;
            _buttonsRepository = buttonsRepository;
            _rolesOwnedModulesRepository = rolesOwnedModulesRepository;
            _applicationVersionRepository = applicationVersionRepository;
            _applicationVersionOwnedModuleRepository = applicationVersionOwnedModuleRepository;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _logger = logger;
            _user = user;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ModulesViewModel>> GetAll()
        {
            var entities = await _modulesRepository.GetAll();
            return _mapper.Map<IEnumerable<Modules>, IEnumerable<ModulesViewModel>>(entities);
        }

        /// <summary>
        /// 带选中状态的列表 -> 设置角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModulesWithCheckedStatusViewModel>> GetAllWithChedkedStatus(string roleId)
        {
            //如果找不到当前登录用户角色信息，什么都不返回
            var currentRole = await _rolesRepository.GetModel(_user.RoleId);
            if (null == currentRole)
            {
                return new List<ModulesWithCheckedStatusViewModel>();
            }

            //系统中所有模块信息
            var entities = await _modulesRepository.GetAll();

            if (currentRole.Identifying != 1)
            {
                //当前登录用户角色拥有的模块信息
                var currentRoleOwnedModules = await _rolesOwnedModulesRepository.GetRoleOwnedModules(_user.RoleId);
                IList<Modules> currentRoleOwnedModuleList = new List<Modules>();
                foreach (var m in entities)
                {
                    if (currentRoleOwnedModules.Where(x => x.ModuleId.Equals(m.Id)).Count() > 0)
                    {
                        currentRoleOwnedModuleList.Add(m);
                    }
                }

                //对象转换
                var modulesWithCheckedStatusViewModels = _mapper.Map<IEnumerable<Modules>, IEnumerable<ModulesWithCheckedStatusViewModel>>(currentRoleOwnedModuleList);

                //待设置角色拥有的模块信息
                var roleOwnedModules = await _rolesOwnedModulesRepository.GetRoleOwnedModules(roleId);
                foreach (var item in modulesWithCheckedStatusViewModels)
                {
                    if (roleOwnedModules.Where(x => x.ModuleId.Equals(item.Id)).Count() > 0)
                    {
                        item.Checked = true;
                    }
                }

                return modulesWithCheckedStatusViewModels;
            }
            else//平台超管角色
            {
                //对象转换
                var modulesWithCheckedStatusViewModels = _mapper.Map<IEnumerable<Modules>, IEnumerable<ModulesWithCheckedStatusViewModel>>(entities);

                //待设置角色拥有的模块信息
                var roleOwnedModules = await _rolesOwnedModulesRepository.GetRoleOwnedModules(roleId);

                //当前登录用户拥有的模块信息
                foreach (var item in modulesWithCheckedStatusViewModels)
                {
                    if (roleOwnedModules.Where(x => x.ModuleId.Equals(item.Id)).Count() > 0)
                    {
                        item.Checked = true;
                    }
                }
                return modulesWithCheckedStatusViewModels;
            }
        }

        /// <summary>
        /// 带选中状态的列表 -> 设置SaaS版本包括的模块
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModulesWithCheckedStatusViewModel>> GetAllWithChedkedStatusForAppVersion(string saaSVersionId)
        {
            //如果找不到SaaS版本信息，什么都不返回
            var appversion = await _applicationVersionRepository.GetModel(saaSVersionId);
            if (null == appversion)
            {
                return new List<ModulesWithCheckedStatusViewModel>();
            }

            //系统中所有模块信息
            var entities = await _modulesRepository.GetAll();

            //对象转换
            var modulesWithCheckedStatusViewModels = _mapper.Map<IEnumerable<Modules>, IEnumerable<ModulesWithCheckedStatusViewModel>>(entities);

            //当前SaaS版本包括的模块信息
            var appVersionOwnedModules = await _applicationVersionOwnedModuleRepository.GetAppVersionOwnedModules(saaSVersionId);
            foreach (var item in modulesWithCheckedStatusViewModels)
            {
                if (appVersionOwnedModules.Where(x => x.ModuleId.Equals(item.Id)).Count() > 0)
                {
                    item.Checked = true;
                }
            }

            return modulesWithCheckedStatusViewModels;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<ModulesViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _modulesRepository.GetPageList(page, limit);
            return _mapper.Map<IPagedList<Modules>, IPagedList<ModulesViewModel>>(entities);
        }

        /// <summary>
        /// 获取一个模块的子模块列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModulesViewModel>> GetAllChilds(string parentId)
        {
            var entities = await _modulesRepository.GetAllChilds(parentId);
            return _mapper.Map<IEnumerable<Modules>, IEnumerable<ModulesViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ModulesAddViewModel model)
        {
            var entity = _mapper.Map<ModulesAddViewModel, Modules>(model);

            var existing = await _modulesRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            if (string.IsNullOrEmpty(entity.ParentId))
            {
                entity.ParentId = Guid.Empty.ToString();
            }

            var result = await _modulesRepository.Add(entity);

            if (result > 0)
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
            var childs = await _modulesRepository.GetAllChilds(id);

            if (childs.ToList().Count() > 0)
            {
                return new Result { Success = false, Message = "该模块存在子模块,不允许删除!" };
            }

            var buttons = await _buttonsRepository.GetModuleButtons(id);
            if (buttons.ToList().Count() > 0)
            {
                return new Result { Success = false, Message = "模块下配置了按钮,不允许删除!" };
            }

            var aspnetroles_owned_modules = await _rolesOwnedModulesRepository.GetModels(id);
            if (aspnetroles_owned_modules.Count() > 0)
            {
                return new Result { Success = false, Message = "模块被权限使用，不允许删除!" };
            }

            var result = await _modulesRepository.Delete(id);

            if (result > 0)
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
        public async Task<Result> Update(ModulesUpdateViewModel model)
        {
            var entity = _mapper.Map<ModulesUpdateViewModel, Modules>(model);

            var existing = await _modulesRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            if (string.IsNullOrEmpty(entity.ParentId))
            {
                entity.ParentId = Guid.Empty.ToString();
            }

            var result = await _modulesRepository.Update(entity);

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
        public async Task<ModulesViewModel> GetModel(string id)
        {
            var entity = await _modulesRepository.GetModel(id);
            return _mapper.Map<Modules, ModulesViewModel>(entity);
        }

        /// <summary>
        /// 模块管理 -> 树型列表
        /// </summary>
        /// <param name="moduleParentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModulesTreeViewModel>> GetTreeList(string moduleParentId = "")
        {
            var entities = await _modulesRepository.GetAll();
            var result = _mapper.Map<IEnumerable<Modules>, IEnumerable<ModulesTreeViewModel>>(entities);
            List<ModulesTreeViewModel> rootNodes = new List<ModulesTreeViewModel>();

            foreach (ModulesTreeViewModel n in result)
            {
                if (!string.IsNullOrEmpty(moduleParentId))
                {
                    if (n.Id.Equals(moduleParentId))
                    {
                        n.Selected = true;
                    }
                }
                if (n.ParentId.Equals(Guid.Empty.ToString()))
                {
                    rootNodes.Add(n);
                    BuildChildNodes(n, result, moduleParentId);
                }
            }

            return rootNodes;
        }

        /// <summary>
        /// 获取当前用户角色拥有的模块信息 -> 生成系统菜单 TO DO : Add to cache
        /// </summary>
        /// <returns></returns>
        public async Task<Result<IEnumerable<ModulesTreeViewModel>>> GetRoleOwnedModules()
        {
            if (string.IsNullOrEmpty(_user.TenantId))
            {
                return new Result<IEnumerable<ModulesTreeViewModel>> { Success = false, Message = "系统无法正确识别用户租户信息！" };
            }

            if (string.IsNullOrEmpty(_user.RoleId))
            {
                return new Result<IEnumerable<ModulesTreeViewModel>> { Success = false, Message = "系统无法正常识别用户角色信息！" };
            }

            var existRole = await _rolesRepository.GetModel(_user.RoleId);

            if (null == existRole)
            {
                return new Result<IEnumerable<ModulesTreeViewModel>> { Success = false, Message = "角色信息非法！" };
            }

            //系统中所有的模块
            var systemAllModules = await _modulesRepository.GetAll();

            //当前角色拥有的模块
            var roleOwnedModules = await _rolesOwnedModulesRepository.GetRoleOwnedModules(_user.RoleId);

            var systemAllViewModules = _mapper.Map<IEnumerable<Modules>, IEnumerable<ModulesTreeViewModel>>(systemAllModules);

            List<ModulesTreeViewModel> rootNodes = new List<ModulesTreeViewModel>();

            foreach (ModulesTreeViewModel n in systemAllViewModules)
            {
                if (n.ParentId.Equals(Guid.Empty.ToString()) && n.IsEnable && roleOwnedModules.Any(x => x.ModuleId == n.Id))
                {
                    rootNodes.Add(n);
                    BuildPermissionChildNodes(n, systemAllViewModules, roleOwnedModules);
                }
            }

            return new Result<IEnumerable<ModulesTreeViewModel>> { Success = true, Message = "请求成功！", Data = rootNodes };
        }

        #region 树型结构TreeSelect列表

        /// <summary>
        /// 获取父节点下所有的子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<ModulesTreeViewModel> GetChildNodes(ModulesTreeViewModel parentNode, IEnumerable<ModulesTreeViewModel> nodes)
        {
            List<ModulesTreeViewModel> childNodes = new List<ModulesTreeViewModel>();
            foreach (ModulesTreeViewModel n in nodes)
            {
                if (parentNode.Id.Equals(n.ParentId))
                {
                    childNodes.Add(n);
                }
            }
            return childNodes;
        }

        /// <summary>
        /// 递归子节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <param name="moduleParentId"></param>
        private void BuildChildNodes(ModulesTreeViewModel node, IEnumerable<ModulesTreeViewModel> nodes, string moduleParentId = "")
        {
            List<ModulesTreeViewModel> children = GetChildNodes(node, nodes);
            foreach (ModulesTreeViewModel child in children)
            {
                if (!string.IsNullOrEmpty(moduleParentId))
                {
                    if (child.Id.Equals(moduleParentId))
                    {
                        child.Selected = true;
                    }
                }
                BuildChildNodes(child, nodes);
            }
            node.Children = children;
        }
        #endregion

        #region 带权限|树型结构TreeSelect列表

        /// <summary>
        /// 获取父节点下所有的子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodes"></param>
        /// <param name="rolesOwnedModules"></param>
        /// <returns></returns>
        private List<ModulesTreeViewModel> GetPermissionChildNodes(ModulesTreeViewModel parentNode, IEnumerable<ModulesTreeViewModel> nodes, IEnumerable<Domain.Models.RolesOwnedModules> rolesOwnedModules)
        {
            List<ModulesTreeViewModel> childNodes = new List<ModulesTreeViewModel>();
            foreach (ModulesTreeViewModel n in nodes)
            {
                if (parentNode.Id.Equals(n.ParentId) && n.IsEnable && rolesOwnedModules.Where(x => x.ModuleId.Equals(n.Id)).Count() > 0)
                {
                    childNodes.Add(n);
                }
            }
            return childNodes;
        }

        /// <summary>
        /// 递归子节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <param name="rolesOwnedModules"></param>
        private void BuildPermissionChildNodes(ModulesTreeViewModel node, IEnumerable<ModulesTreeViewModel> nodes, IEnumerable<Domain.Models.RolesOwnedModules> rolesOwnedModules)
        {
            List<ModulesTreeViewModel> children = GetPermissionChildNodes(node, nodes, rolesOwnedModules);
            foreach (ModulesTreeViewModel child in children)
            {
                BuildPermissionChildNodes(child, nodes, rolesOwnedModules);
            }
            node.Children = children;
        }

        #endregion
    }
}
