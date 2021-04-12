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
using Awine.Teach.FoundationService.Domain.Models;
using Awine.Teach.FoundationService.Application.ServiceResult;
using System.Linq;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 部门
    /// </summary>
    public class DepartmentsService : IDepartmentsService
    {
        /// <summary>
        /// 部门
        /// </summary>
        private readonly IDepartmentsRepository _departmentsRepository;

        /// <summary>
        /// 用户
        /// </summary>
        private readonly IUsersRepository _usersRepository;

        /// <summary>
        /// 租户参数配置
        /// </summary>
        private readonly ITenantSettingsRepository _tenantSettingsRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<DepartmentsService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="departmentsRepository"></param>
        /// <param name="usersRepository"></param>
        /// <param name="tenantSettingsRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        public DepartmentsService(IDepartmentsRepository departmentsRepository,
            IUsersRepository usersRepository,
            ITenantSettingsRepository tenantSettingsRepository,
            IMapper mapper, ILogger<DepartmentsService> logger, ICurrentUser user)
        {
            _departmentsRepository = departmentsRepository;
            _usersRepository = usersRepository;
            _tenantSettingsRepository = tenantSettingsRepository;
            _mapper = mapper;
            _logger = logger;
            _user = user;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IPagedList<DepartmentsViewModel>> GetPageList(int page = 1, int limit = 15, string tenantId = "")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                tenantId = _user.TenantId;
            }
            var entities = await _departmentsRepository.GetPageList(page, limit, tenantId);
            return _mapper.Map<IPagedList<Departments>, IPagedList<DepartmentsViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DepartmentsViewModel>> GetAll(string tenantId = "")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                tenantId = _user.TenantId;
            }
            var entities = await _departmentsRepository.GetAll(tenantId);
            return _mapper.Map<IEnumerable<Departments>, IEnumerable<DepartmentsViewModel>>(entities);
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DepartmentsViewModel> GetModel(string id)
        {
            var entity = await _departmentsRepository.GetModel(id);
            return _mapper.Map<Departments, DepartmentsViewModel>(entity);
        }

        /// <summary>
        /// 树型列表 -> 返回 Layui XMSelect 数据结构
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DepartmentsXMSelectViewModel>> GetXMSelectTree(string parentId = "")
        {
            var entities = await _departmentsRepository.GetAll(_user.TenantId);
            var result = _mapper.Map<IEnumerable<Departments>, IEnumerable<DepartmentsXMSelectViewModel>>(entities);
            List<DepartmentsXMSelectViewModel> rootNodes = new List<DepartmentsXMSelectViewModel>();

            foreach (DepartmentsXMSelectViewModel n in result)
            {
                if (!string.IsNullOrEmpty(parentId))
                {
                    if (n.Id.Equals(parentId))
                    {
                        n.Selected = true;
                    }
                }
                if (n.ParentId.Equals(Guid.Empty.ToString()))
                {
                    rootNodes.Add(n);
                    BuildXMSelectChildNodes(n, result, parentId);
                }
            }

            return rootNodes;
        }

        /// <summary>
        /// 树型列表 -> 返回 Layui Tree 数据结构
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DepartmentsTreeViewModel>> GetTree()
        {
            var entities = await _departmentsRepository.GetAll(_user.TenantId);
            var result = _mapper.Map<IEnumerable<Departments>, IEnumerable<DepartmentsTreeViewModel>>(entities);
            List<DepartmentsTreeViewModel> rootNodes = new List<DepartmentsTreeViewModel>();

            foreach (DepartmentsTreeViewModel n in result)
            {
                if (n.ParentId.Equals(Guid.Empty.ToString()))
                {
                    rootNodes.Add(n);
                    BuildTreeChildNodes(n, result);
                }
            }

            return rootNodes;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(DepartmentsAddViewModel model)
        {
            try
            {
                var parent = await _departmentsRepository.GetModel(model.ParentId);
                if (parent == null)
                {
                    return new Result { Success = false, Message = "未到到指定的父级部门信息！" };
                }

                //当前登录租户部门数据
                var departments = await _departmentsRepository.GetAll(_user.TenantId);
                var tenantSettings = await _tenantSettingsRepository.GetModelForTenant(_user.TenantId);

                if (_user.TenantClassiFication != 3)
                {

                    if (departments.Count() >= tenantSettings.MaxNumberOfDepartments)
                    {
                        return new Result { Success = false, Message = $"您已经有{departments.Count()}个部门了，不能再添加了哦！" };
                    }

                    //if (departments.Where(x => x.ParentId.Equals(Guid.Empty)).Count() >= tenantSettings.MaxNumberOfBranch)
                    //{
                    //    return new Result { Success = false, Message = $"您已经有{departments.Where(x => x.ParentId.Equals(Guid.Empty)).Count()}个分支机构了，不能再添加了哦！" };
                    //}
                }

                var entity = _mapper.Map<DepartmentsAddViewModel, Departments>(model);
                entity.Id = Guid.NewGuid().ToString();
                entity.TenantId = _user.TenantId;
                entity.CreateTime = DateTime.Now;

                if (null != await _departmentsRepository.GetModel(entity))
                {
                    return new Result { Success = false, Message = "数据已存在！" };
                }

                if (await _departmentsRepository.Add(entity) > 0)
                {
                    return new Result { Success = true, Message = "操作成功！" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"发生异常-{ex.ToString()}");
                return new Result { Success = false, Message = $"操作失败！{ex.Message}" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(DepartmentsUpdateViewModel model)
        {
            try
            {
                if (model.Id.Equals(model.ParentId))
                {
                    return new Result { Success = false, Message = "父级部门不能是自己！" };
                }

                var exist = await _departmentsRepository.GetModel(model.Id);
                if (null == exist)
                {
                    return new Result { Success = false, Message = "未到找部门信息！" };
                }

                if (exist.ParentId.Equals(Guid.Empty))
                {
                    return new Result { Success = false, Message = "当前部门不允许进行修改！" };
                }

                var entity = _mapper.Map<DepartmentsUpdateViewModel, Departments>(model);

                if (null != await _departmentsRepository.GetModel(entity))
                {
                    return new Result { Success = false, Message = "数据已存在！" };
                }

                if (await _departmentsRepository.Update(entity) > 0)
                {
                    return new Result { Success = true, Message = "操作成功！" };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"发生异常{ex.ToString()}");
                return new Result { Success = false, Message = $"操作失败！{ex.Message}" };
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
            var exist = await _departmentsRepository.GetModel(id);
            if (null == exist)
            {
                return new Result { Success = false, Message = "未找到部门信息！" };
            }
            if (exist.ParentId.Equals(Guid.Empty))
            {
                return new Result { Success = false, Message = "根部门不允许删除！" };
            }

            var departments = await _departmentsRepository.GetAll(_user.TenantId);

            if (departments.Where(x => x.ParentId.Equals(id)).Count() > 0)
            {
                return new Result { Success = false, Message = "操作失败：部门有子部门数据！" };
            }

            var users = await _usersRepository.GetAllInDepartment(id);

            if (users.Count() > 0)
            {
                return new Result { Success = false, Message = "操作失败：部门有员工数据！" };
            }

            var result = await _departmentsRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        #region 树型结构 XMSelect TreeSelect 列表

        /// <summary>
        /// 获取父节点下所有的子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<DepartmentsXMSelectViewModel> GetXMSelectChildNodes(DepartmentsXMSelectViewModel parentNode, IEnumerable<DepartmentsXMSelectViewModel> nodes)
        {
            List<DepartmentsXMSelectViewModel> childNodes = new List<DepartmentsXMSelectViewModel>();
            foreach (DepartmentsXMSelectViewModel n in nodes)
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
        /// <param name="parentId"></param>
        private void BuildXMSelectChildNodes(DepartmentsXMSelectViewModel node, IEnumerable<DepartmentsXMSelectViewModel> nodes, string parentId = "")
        {
            List<DepartmentsXMSelectViewModel> children = GetXMSelectChildNodes(node, nodes);
            foreach (DepartmentsXMSelectViewModel child in children)
            {
                if (!string.IsNullOrEmpty(parentId))
                {
                    if (child.Id.Equals(parentId))
                    {
                        child.Selected = true;
                    }
                }
                BuildXMSelectChildNodes(child, nodes);
            }
            node.Children = children;
        }
        #endregion

        #region 树型结构 Layui Tree 列表

        /// <summary>
        /// 获取父节点下所有的子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<DepartmentsTreeViewModel> GetTreeChildNodes(DepartmentsTreeViewModel parentNode, IEnumerable<DepartmentsTreeViewModel> nodes)
        {
            List<DepartmentsTreeViewModel> childNodes = new List<DepartmentsTreeViewModel>();
            foreach (DepartmentsTreeViewModel n in nodes)
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
        private void BuildTreeChildNodes(DepartmentsTreeViewModel node, IEnumerable<DepartmentsTreeViewModel> nodes)
        {
            List<DepartmentsTreeViewModel> children = GetTreeChildNodes(node, nodes);
            foreach (DepartmentsTreeViewModel child in children)
            {
                BuildTreeChildNodes(child, nodes);
            }
            node.Children = children;
        }
        #endregion
    }
}
