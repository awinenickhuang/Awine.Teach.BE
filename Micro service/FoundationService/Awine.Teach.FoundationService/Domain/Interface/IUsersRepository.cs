using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 系统账号
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        Task<IEnumerable<Users>> GetAll(string tenantId, bool isActive = true, int gender = 0);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="userName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="tenantId"></param>
        /// <param name="departmentId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IPagedList<Users>> GetPageList(int page = 1, int limit = 15, string userName = "", string phoneNumber = "", string tenantId = "", string departmentId = "", string roleId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(Users model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(Users model);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Users> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Users> GetDetailModel(string id);

        /// <summary>
        /// 取一条数据 -> 全局查询 ->账号或手机号不能重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Users> GetGlobalModel(Users model);

        /// <summary>
        /// 取一条数据 -> 当前租户查询 -> 姓名不能重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Users> GetModel(Users model);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="account"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<Users> GetModel(string account = "", string phoneNumber = "");

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<int> ChangePassword(string id, string password);

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> ChangeActive(Users model);

        /// <summary>
        /// 查询某一部门的所有用户
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        Task<IEnumerable<Users>> GetAllInDepartment(string departmentId, bool isActive = true, int gender = 0);

        /// <summary>
        /// 查询某一角色的所有用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<Users>> GetAllInRole(string roleId);
    }
}
