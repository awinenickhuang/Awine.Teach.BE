using AutoMapper;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Awine.Teach.FoundationService.Domain.Interface;
using Awine.Teach.FoundationService.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 行政区域划分
    /// </summary>
    public class AdministrativeDivisionsService : IAdministrativeDivisionsService
    {
        /// <summary>
        /// 行政区域划分
        /// </summary>
        private readonly IAdministrativeDivisionsRepository _administrativeDivisionsRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<AdministrativeDivisionsService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="administrativeDivisionsRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public AdministrativeDivisionsService(IAdministrativeDivisionsRepository administrativeDivisionsRepository, IMapper mapper, ILogger<AdministrativeDivisionsService> logger)
        {
            _administrativeDivisionsRepository = administrativeDivisionsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 取所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AdministrativeDivisionsViewModel>> GetAll()
        {
            var entities = await _administrativeDivisionsRepository.GetAll();
            return _mapper.Map<IEnumerable<AdministrativeDivisions>, IEnumerable<AdministrativeDivisionsViewModel>>(entities);
        }

        /// <summary>
        /// 获取下级区域数据
        /// </summary>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AdministrativeDivisionsViewModel>> GetSubordinateRegionalism(int parentCode)
        {
            var entities = await _administrativeDivisionsRepository.GetSubordinateRegionalism(parentCode);
            return _mapper.Map<IEnumerable<AdministrativeDivisions>, IEnumerable<AdministrativeDivisionsViewModel>>(entities);
        }
    }
}
