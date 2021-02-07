using AutoMapper;
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
    /// 按钮
    /// </summary>
    public class ButtonsService : IButtonsService
    {
        /// <summary>
        /// 按钮
        /// </summary>
        private readonly IButtonsRepository _buttonsRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ButtonsService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="buttonsRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public ButtonsService(IButtonsRepository buttonsRepository, IMapper mapper, ILogger<ButtonsService> logger)
        {
            _buttonsRepository = buttonsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ButtonsViewModel>> GetAll()
        {
            var entities = await _buttonsRepository.GetAll();

            return _mapper.Map<IEnumerable<Buttons>, IEnumerable<ButtonsViewModel>>(entities);
        }

        /// <summary>
        /// 某一模块拥有的按钮列表
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ButtonsViewModel>> GetModuleButtons(string moduleId)
        {
            var entities = await _buttonsRepository.GetModuleButtons(moduleId);

            return _mapper.Map<IEnumerable<Buttons>, IEnumerable<ButtonsViewModel>>(entities);
        }

        /// <summary>
        /// 某一模块拥有的按钮 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IPagedList<ButtonsViewModel>> GetPageList(int page = 1, int limit = 15, string moduleId = "")
        {
            var entities = await _buttonsRepository.GetPageList(page, limit, moduleId);

            return _mapper.Map<IPagedList<Buttons>, IPagedList<ButtonsViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ButtonsAddViewModel model)
        {
            var entity = _mapper.Map<ButtonsAddViewModel, Buttons>(model);

            var existing = await _buttonsRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _buttonsRepository.Add(entity);

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
        public async Task<Result> Update(ButtonsViewModel model)
        {
            var entity = _mapper.Map<ButtonsViewModel, Buttons>(model);

            var existing = await _buttonsRepository.GetModel(entity);

            if (null != existing)
            {
                return new Result { Success = false, Message = "数据已存在" };
            }

            var result = await _buttonsRepository.Update(entity);

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

            var result = await _buttonsRepository.Delete(id);

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
        public async Task<ButtonsViewModel> GetModel(string id)
        {
            var entity = await _buttonsRepository.GetModel(id);
            return _mapper.Map<Buttons, ButtonsViewModel>(entity);
        }
    }
}
