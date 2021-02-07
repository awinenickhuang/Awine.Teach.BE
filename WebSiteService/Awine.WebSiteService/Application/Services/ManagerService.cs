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
    /// <summary>
    /// 管理员
    /// </summary>
    public class ManagerService : IManagerService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ManagerService> _logger;

        /// <summary>
        /// IManagerRepository
        /// </summary>
        private readonly IManagerRepository _managerRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="managerRepository"></param>
        public ManagerService(
            IMapper mapper,
            ILogger<ManagerService> logger,
            IManagerRepository managerRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _managerRepository = managerRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<ManagerViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _managerRepository.GetPageList(page, limit);

            return _mapper.Map<IPagedList<Manager>, IPagedList<ManagerViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ManagerViewModel>> GetAll()
        {
            var entities = await _managerRepository.GetAll();

            return _mapper.Map<IEnumerable<Manager>, IEnumerable<ManagerViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ManagerViewModel> GetModel(string id)
        {
            var entities = await _managerRepository.GetModel(id);

            return _mapper.Map<Manager, ManagerViewModel>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result<ManagerViewModel>> GetModel(ManagerLoginViewModel model)
        {
            var entity = await _managerRepository.GetModel(model.Account, model.Password);
            if (null == entity)
            {
                return new Result<ManagerViewModel> { Success = false, Message = "账号或密码错误！" };
            }
            var manager = _mapper.Map<Manager, ManagerViewModel>(entity);

            return new Result<ManagerViewModel> { Success = true, Message = "操作成功！", Data = manager };
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ManagerAddViewModel model)
        {
            var manager = _mapper.Map<ManagerAddViewModel, Manager>(model);

            if (await _managerRepository.Add(manager) > 0)
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
        public async Task<Result> Update(ManagerUpdateViewModel model)
        {
            var articale = _mapper.Map<ManagerUpdateViewModel, Manager>(model);

            if (await _managerRepository.Update(articale) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdatePassword(ManagerUpdatePasswordViewModel model)
        {
            var manager = _mapper.Map<ManagerUpdatePasswordViewModel, Manager>(model);

            if (await _managerRepository.UpdatePassword(manager) > 0)
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
            var res = await _managerRepository.Delete(id);

            if (res > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
