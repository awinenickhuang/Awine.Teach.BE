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
    /// 应用版本对应的系统模块
    /// </summary>
    public class ApplicationVersionOwnedModuleService : IApplicationVersionOwnedModuleService
    {
        /// <summary>
        /// 应用版本对应的系统模块
        /// </summary>
        private readonly IApplicationVersionOwnedModuleRepository _applicationVersionOwnedModuleRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ApplicationVersionOwnedModuleService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="applicationVersionOwnedModuleRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ApplicationVersionOwnedModuleService(IApplicationVersionOwnedModuleRepository applicationVersionOwnedModuleRepository, IMapper mapper, ILogger<ApplicationVersionOwnedModuleService> logger)
        {
            _applicationVersionOwnedModuleRepository = applicationVersionOwnedModuleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 设置应用版本包括的模块信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public async Task<Result> SaveAppVersionOwnedModules(ApplicationVersionOwnedModuleAddViewModel module)
        {
            IList<ApplicationVersionOwnedModule> modules = new List<ApplicationVersionOwnedModule>();
            foreach (var model in module.AppVersionOwnedModules)
            {
                var addModule = new ApplicationVersionOwnedModule()
                {
                    Id = Guid.NewGuid().ToString(),
                    AppVersionId = module.AppVersionId,
                    ModuleId = model.ModuleId,
                    IsDeleted = false,
                    CreateTime = DateTime.Now
                };
                modules.Add(addModule);
            }
            var result = await _applicationVersionOwnedModuleRepository.SaveAppVersionOwnedModules(module.AppVersionId, modules);
            if (result)
            {
                return new Result() { Success = true, Message = "操作成功" };
            }
            return new Result() { Success = false, Message = "操作失败" };
        }

        /// <summary>
        /// 查询应用版本包括的模块信息
        /// </summary>
        /// <param name="appVersionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApplicationVersionOwnedModuleViewModel>> GetAppVersionOwnedModules(string appVersionId)
        {
            var list = await _applicationVersionOwnedModuleRepository.GetAppVersionOwnedModules(appVersionId);
            return _mapper.Map<IEnumerable<ApplicationVersionOwnedModule>, IEnumerable<ApplicationVersionOwnedModuleViewModel>>(list);
        }
    }
}
