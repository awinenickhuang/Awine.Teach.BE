using Awine.IdpCenter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.IRepositories
{
    /// <summary>
    /// 自定义身份管理库 -> 系统用户 -> 机构
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 按ID查询
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        Task<User> GetBySubjectId(string subjectId);

        /// <summary>
        /// 按账号查询
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<User> GetByAccount(string account);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(User model);
    }
}
