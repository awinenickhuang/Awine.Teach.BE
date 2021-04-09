using AutoMapper;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using Awine.Teach.FoundationService.Domain.Interface;
using Awine.Teach.FoundationService.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// SaaS版本包括的系统模块
    /// </summary>
    public class SaaSVersionOwnedModuleService : ISaaSVersionOwnedModuleService
    {
        /// <summary>
        /// SaaS版本包括的系统模块
        /// </summary>
        private readonly ISaaSVersionOwnedModuleRepository _applicationVersionOwnedModuleRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<SaaSVersionOwnedModuleService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="applicationVersionOwnedModuleRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public SaaSVersionOwnedModuleService(ISaaSVersionOwnedModuleRepository applicationVersionOwnedModuleRepository, IMapper mapper, ILogger<SaaSVersionOwnedModuleService> logger)
        {
            _applicationVersionOwnedModuleRepository = applicationVersionOwnedModuleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 设置SaaS版本包括的模块信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public async Task<Result> SaveAppVersionOwnedModules(SaaSVersionOwnedModuleAddViewModel module)
        {
            IList<SaaSVersionOwnedModule> modules = new List<SaaSVersionOwnedModule>();
            foreach (var model in module.SaaSVersionOwnedModules)
            {
                var addModule = new SaaSVersionOwnedModule()
                {
                    Id = Guid.NewGuid().ToString(),
                    SaaSVersionId = module.SaaSVersionId,
                    ModuleId = model.ModuleId,
                    IsDeleted = false,
                    CreateTime = DateTime.Now
                };
                modules.Add(addModule);
            }
            var result = await _applicationVersionOwnedModuleRepository.SaveAppVersionOwnedModules(module.SaaSVersionId, modules);
            if (result)
            {
                return new Result() { Success = true, Message = "操作成功" };
            }
            return new Result() { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 查询SaaS版本包括的模块信息
        /// </summary>
        /// <param name="saaSVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SaaSVersionOwnedModuleViewModel>> GetAppVersionOwnedModules(string saaSVersionId)
        {
            var list = await _applicationVersionOwnedModuleRepository.GetAppVersionOwnedModules(saaSVersionId);
            return _mapper.Map<IEnumerable<SaaSVersionOwnedModule>, IEnumerable<SaaSVersionOwnedModuleViewModel>>(list);
        }
    }
}
