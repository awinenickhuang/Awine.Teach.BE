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
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="tenantsRepository"></param>
        /// <param name="industryRepository"></param>
        public TenantsService(
            IMapper mapper,
            ILogger<TenantsService> logger,
            ICurrentUser user,
            ITenantsRepository tenantsRepository,
            IIndustryCategoryRepository industryCategoryRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _tenantsRepository = tenantsRepository;
            _industryCategoryRepository = industryCategoryRepository;
        }

        /// <summary>
        /// 全部数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TenantsViewModel>> GetAll()
        {
            var entities = await _tenantsRepository.GetAll(_user.TenantId);

            return _mapper.Map<IEnumerable<Tenants>, IEnumerable<TenantsViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<TenantsViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _tenantsRepository.GetPageList(page, limit, _user.TenantId);

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
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(TenantsAddViewModel model)
        {
            var currentTenant = await _tenantsRepository.GetModel(_user.TenantId);
            if (null == currentTenant)
            {
                return new Result { Success = false, Message = "当前租户信息解析出错！" };
            }

            var tenants = await _tenantsRepository.GetAll(_user.TenantId);
            if (tenants.Count() >= currentTenant.NumberOfBranches)
            {
                return new Result { Success = false, Message = $"抱歉，您当前只能有{currentTenant.NumberOfBranches}个分支机构！" };
            }

            var industry = await _industryCategoryRepository.GetModel(model.IndustryId);

            if (null == industry)
            {
                return new Result { Success = false, Message = "未找到所属行业信息！" };
            }

            var entity = _mapper.Map<TenantsAddViewModel, Tenants>(model);

            if (null != await _tenantsRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            entity.IndustryName = industry.Name;
            entity.ParentId = _user.TenantId;

            if (await _tenantsRepository.Add(entity) > 0)
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

                if (null != await _tenantsRepository.GetModel(entity))
                {
                    return new Result { Success = false, Message = "数据已存在！" };
                }

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
        /// 更新 -> 租户类型 1-免费 2-试用 3-付费（VIP）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdateClassiFication(TenantsUpdateClassiFicationViewModel model)
        {
            var exist = await _tenantsRepository.GetModel(model.Id);

            if (null == exist)
            {
                return new Result { Success = false, Message = "数据不存在！" };
            }

            var entity = new Tenants()
            {
                Id = model.Id,
                ClassiFication = model.ClassiFication
            };

            var result = await _tenantsRepository.UpdateClassiFication(entity);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
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
                return new Result { Success = false, Message = "数据不存在！" };
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

        /// <summary>
        /// 更新 -> 允许添加的分支机构个数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdateNumberOfBranches(TenantsUpdateNumberOfBranchesViewModel model)
        {
            var exist = await _tenantsRepository.GetModel(model.Id);

            if (null == exist)
            {
                return new Result { Success = false, Message = "数据不存在！" };
            }

            var entity = new Tenants()
            {
                Id = model.Id,
                NumberOfBranches = model.NumberOfBranches
            };

            var result = await _tenantsRepository.UpdateNumberOfBranches(entity);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
