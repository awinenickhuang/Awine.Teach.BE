using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
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
    /// 收款方式
    /// </summary>
    public class PaymentMethodService : IPaymentMethodService
    {
        /// <summary>
        /// IPaymentMethodRepository
        /// </summary>
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<PaymentMethodService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="paymentMethodRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper,
            ILogger<PaymentMethodService> logger)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IPagedList<PaymentMethodViewModel>> GetPageList(int page = 1, int limit = 15)
        {
            var entities = await _paymentMethodRepository.GetPageList(page, limit);

            return _mapper.Map<IPagedList<PaymentMethod>, IPagedList<PaymentMethodViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PaymentMethodViewModel>> GetAll()
        {
            var entities = await _paymentMethodRepository.GetAll();

            return _mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<PaymentMethodViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PaymentMethodViewModel> GetModel(string id)
        {
            var entity = await _paymentMethodRepository.GetModel(id);

            return _mapper.Map<PaymentMethod, PaymentMethodViewModel>(entity);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(PaymentMethodAddViewModel model)
        {
            var entity = _mapper.Map<PaymentMethodAddViewModel, PaymentMethod>(model);

            if (null != await _paymentMethodRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _paymentMethodRepository.Add(entity) > 0)
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
        public async Task<Result> Update(PaymentMethodUpdateViewModel model)
        {
            var entity = _mapper.Map<PaymentMethodUpdateViewModel, PaymentMethod>(model);

            if (null != await _paymentMethodRepository.GetModel(entity))
            {
                return new Result { Success = false, Message = "数据已存在！" };
            }

            if (await _paymentMethodRepository.Update(entity) > 0)
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
            var result = await _paymentMethodRepository.Delete(id);

            if (result > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
