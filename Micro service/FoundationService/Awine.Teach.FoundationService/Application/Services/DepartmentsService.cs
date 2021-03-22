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
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        public DepartmentsService(IDepartmentsRepository departmentsRepository,
            IUsersRepository usersRepository,
            IMapper mapper, ILogger<DepartmentsService> logger, ICurrentUser user)
        {
            _departmentsRepository = departmentsRepository;
            _usersRepository = usersRepository;
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
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(DepartmentsAddViewModel model)
        {
            try
            {
                var entity = _mapper.Map<DepartmentsAddViewModel, Departments>(model);
                entity.Id = Guid.NewGuid().ToString();
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
            var users = await _usersRepository.GetAllInDepartment(id);
            if (users.Count() > 0)
            {
                return new Result { Success = false, Message = "操作失败：该部门下有员工数据！" };
            }

            var result = await _departmentsRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
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
    }
}
