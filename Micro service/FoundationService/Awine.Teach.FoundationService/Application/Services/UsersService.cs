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
using Awine.Framework.Core.Cryptography;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 系统账号
    /// </summary>
    public class UsersService : IUsersService
    {
        /// <summary>
        /// AspnetUser
        /// </summary>
        private readonly IUsersRepository _usersRepository;

        /// <summary>
        /// IPlatformTenantRepository
        /// </summary>
        private readonly ITenantsRepository _tenantsRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<UsersService> _logger;

        /// <summary>
        /// 当前用户
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="usersRepository"></param>
        /// <param name="tenantsRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        public UsersService(IUsersRepository usersRepository,
            ITenantsRepository tenantsRepository,
            IMapper mapper, ILogger<UsersService> logger, ICurrentUser user)
        {
            _usersRepository = usersRepository;
            _tenantsRepository = tenantsRepository;
            _mapper = mapper;
            _logger = logger;
            _user = user;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UsersViewModel>> GetAll(bool isActive = true, int gender = 0)
        {
            var entities = await _usersRepository.GetAll(_user.TenantId, isActive, gender);
            return _mapper.Map<IEnumerable<Users>, IEnumerable<UsersViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="userName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="tenantId"></param>
        /// <param name="departmentId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IPagedList<UsersViewModel>> GetPageList(int page = 1, int limit = 15, string userName = "", string phoneNumber = "", string tenantId = "", string departmentId = "", string roleId = "")
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                tenantId = _user.TenantId;
            }
            var entities = await _usersRepository.GetPageList(page, limit, userName, phoneNumber, tenantId, departmentId, roleId);
            return _mapper.Map<IPagedList<Users>, IPagedList<UsersViewModel>>(entities);
        }

        /// <summary>
        /// 查询某一部门的所有用户
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UsersViewModel>> GetAllInDepartment(string departmentId, bool isActive = true, int gender = 0)
        {
            var entities = await _usersRepository.GetAllInDepartment(departmentId, isActive, gender);

            return _mapper.Map<IEnumerable<Users>, IEnumerable<UsersViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(UsersAddViewModel model)
        {
            var entity = _mapper.Map<UsersAddViewModel, Users>(model);

            var globalModel = await _usersRepository.GetGlobalModel(entity);

            if (null != globalModel)
            {
                return new Result { Success = false, Message = "你的账号或手机号码已被占用" };
            }

            var tenantModel = await _usersRepository.GetModel(entity);

            if (null != tenantModel)
            {
                return new Result { Success = false, Message = "该用户已添加" };
            }

            entity.Id = Guid.NewGuid().ToString();
            entity.PasswordHash = PasswordManager.HashPassword(entity.PasswordHash);
            entity.NormalizedUserName = model.Account.ToLower();

            if (string.IsNullOrEmpty(entity.TenantId))
            {
                entity.TenantId = _user.TenantId;
            }

            var result = await _usersRepository.Add(entity);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 取一数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UsersViewModel> GetModel(string id)
        {
            var entities = await _usersRepository.GetModel(id);
            return _mapper.Map<Users, UsersViewModel>(entities);
        }

        /// <summary>
        /// 获取当前登录用户的详细信息
        /// </summary>
        /// <returns></returns>
        public async Task<UsersDetailsViewModel> GetModel()
        {
            var entity = await _usersRepository.GetDetailModel(_user.UserId);
            var model = new UsersDetailsViewModel()
            {
                UserName = entity.UserName,
                Account = entity.Account,
                PhoneNumber = entity.PhoneNumber,
                Gender = entity.Gender,
                TenantName = entity.PlatformTenant.Name,
                VIPExpirationTime = entity.PlatformTenant.VIPExpirationTime,
                IndustryName = entity.PlatformTenant.IndustryName,
                ClassiFication = entity.PlatformTenant.ClassiFication,
                RoleName = entity.AspnetRole.Name,
                DepartmentName = entity.Department.Name
            };
            return model;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(UsersUpdateViewModel model)
        {
            var entity = _mapper.Map<UsersUpdateViewModel, Users>(model);

            var globalModel = await _usersRepository.GetGlobalModel(entity);

            if (null != globalModel)
            {
                return new Result { Success = false, Message = "你的账号或手机号码已被占用" };
            }

            var tenantModel = await _usersRepository.GetModel(entity);

            if (null != tenantModel)
            {
                return new Result { Success = false, Message = "该用户已添加" };
            }

            var result = await _usersRepository.Update(entity);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> ResetPassword(UsersResetPasswordViewModel model)
        {
            var aspnetuser = await _usersRepository.GetModel(model.Id);
            if (null == aspnetuser)
            {
                return new Result { Success = false, Message = "未找到用户信息" };
            }

            var hashedPassword = PasswordManager.HashPassword(model.Password);
            var result = await _usersRepository.ChangePassword(model.Id, hashedPassword);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdatePassword(UsersUpdatePasswordViewModel model)
        {
            var aspnetuser = await _usersRepository.GetModel(_user.UserId);
            if (null == aspnetuser)
            {
                return new Result { Success = false, Message = "未找到用户信息" };
            }
            ;
            if (!PasswordManager.VerifyHashedPassword(aspnetuser.PasswordHash, model.OriginalPassword))
            {
                return new Result { Success = false, Message = "当前密码输入错误" };
            }

            var hashedPassword = PasswordManager.HashPassword(model.Password);
            var result = await _usersRepository.ChangePassword(aspnetuser.Id, hashedPassword);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功，请使用新密码！" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> EnableOrDisable(UsersUpdateStatusViewModel model)
        {
            var exist = await _usersRepository.GetModel(model.Id);
            if (null == exist)
            {
                return new Result { Success = false, Message = "未找到用户信息" };
            }
            if (exist.IsActive)
            {
                exist.IsActive = false;
            }
            else
            {
                exist.IsActive = true;
            }
            var result = await _usersRepository.ChangeActive(exist);
            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功" };
            }
            return new Result { Success = false, Message = "操作失败" };
        }
    }
}
