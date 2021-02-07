using AutoMapper;
using Awine.Teach.FoundationService.Application.Interfaces;
using Awine.Teach.FoundationService.Application.ViewModels;
using Awine.Teach.FoundationService.Domain;
using Awine.Teach.FoundationService.Domain.Interface;
using Awine.Teach.FoundationService.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Services
{
    /// <summary>
    /// 角色声明信息
    /// </summary>
    public class RolesClaimsService : IRolesClaimsService
    {
        /// <summary>
        /// 角色声明
        /// </summary>
        private readonly IRolesClaimsRepository _rolesClaimsRepository;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<RolesClaimsService> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="rolesClaimsRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public RolesClaimsService(IRolesClaimsRepository rolesClaimsRepository, IMapper mapper, ILogger<RolesClaimsService> logger)
        {
            _rolesClaimsRepository = rolesClaimsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RolesClaimsViewModel>> GetAll(string roleId)
        {
            var entities = await _rolesClaimsRepository.GetAll(roleId);

            return _mapper.Map<IEnumerable<RolesClaims>, IEnumerable<RolesClaimsViewModel>>(entities);
        }
    }
}
