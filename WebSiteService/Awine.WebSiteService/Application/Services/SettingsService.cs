using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.WebSite.Applicaton.Interface;
using Awine.WebSite.Applicaton.ServiceResult;
using Awine.WebSite.Applicaton.ViewModels;
using Awine.WebSite.Domain;
using Awine.WebSite.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Applicaton.Services
{
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<SettingsService> _logger;

        /// <summary>
        /// ISettingsRepository
        /// </summary>
        private readonly ISettingsRepository _settingsRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="settingsRepository"></param>
        public SettingsService(
            IMapper mapper,
            ILogger<SettingsService> logger,
            ISettingsRepository settingsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _settingsRepository = settingsRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<SettingsViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _settingsRepository.GetPageList(page, limit);

            return _mapper.Map<IPagedList<Settings>, IPagedList<SettingsViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SettingsViewModel>> GetAll()
        {
            var entities = await _settingsRepository.GetAll();

            return _mapper.Map<IEnumerable<Settings>, IEnumerable<SettingsViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SettingsViewModel> GetModel(string id)
        {
            var entities = await _settingsRepository.GetModel(id);

            return _mapper.Map<Settings, SettingsViewModel>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(SettingsAddViewModel model)
        {
            var settings = _mapper.Map<SettingsAddViewModel, Settings>(model);

            if (await _settingsRepository.Add(settings) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(SettingsUpdateViewModel model)
        {
            var settings = _mapper.Map<SettingsUpdateViewModel, Settings>(model);

            if (await _settingsRepository.Update(settings) > 0)
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
            var res = await _settingsRepository.Delete(id);

            if (res > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
