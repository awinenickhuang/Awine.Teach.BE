using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 法定假日
    /// </summary>
    public class LegalHolidayService : ILegalHolidayService
    {
        /// <summary>
        /// ILegalHolidayRepository
        /// </summary>
        private readonly ILegalHolidayRepository _legalHolidayRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<LegalHolidayService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="legalHolidayRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public LegalHolidayService(ILegalHolidayRepository legalHolidayRepository,
            IMapper mapper,
            ILogger<LegalHolidayService> logger)
        {
            _legalHolidayRepository = legalHolidayRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<IPagedList<LegalHolidayViewModel>> GetPageList(int page = 1, int limit = 15, int year = 0)
        {
            var entities = await _legalHolidayRepository.GetPageList(page, limit, year);

            return _mapper.Map<IPagedList<LegalHoliday>, IPagedList<LegalHolidayViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LegalHolidayViewModel>> GetAll(int year = 0)
        {
            var entities = await _legalHolidayRepository.GetAll(year);

            return _mapper.Map<IEnumerable<LegalHoliday>, IEnumerable<LegalHolidayViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(LegalHolidayAddViewModel model)
        {
            var entity = _mapper.Map<LegalHolidayAddViewModel, LegalHoliday>(model);

            if (null != await _legalHolidayRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _legalHolidayRepository.Add(entity) > 0)
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
        public async Task<Result> Update(LegalHolidayUpdateViewModel model)
        {
            var entity = _mapper.Map<LegalHolidayUpdateViewModel, LegalHoliday>(model);

            if (null != await _legalHolidayRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _legalHolidayRepository.Update(entity) > 0)
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
            var result = await _legalHolidayRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
