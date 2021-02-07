using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 角色声明信息
    /// </summary>
    public interface IRolesClaimsRepository
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<RolesClaims>> GetAll(string roleId);
    }
}
