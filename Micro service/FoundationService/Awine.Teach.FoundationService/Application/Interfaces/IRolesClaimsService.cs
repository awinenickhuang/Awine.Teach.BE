using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 角色声明信息
    /// </summary>
    public interface IRolesClaimsService
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<RolesClaimsViewModel>> GetAll(string roleId);
    }
}
