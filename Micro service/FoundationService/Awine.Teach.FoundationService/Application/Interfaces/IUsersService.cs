using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 系统账号
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        Task<IEnumerable<UsersViewModel>> GetAll(bool isActive = true, int gender = 0);

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="userName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="tenantId"></param>
        /// <param name="departmentId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IPagedList<UsersViewModel>> GetPageList(int pageIndex = 1, int pageSize = 15, string userName = "", string phoneNumber = "", string tenantId = "", string departmentId = "", string roleId = "");

        /// <summary>
        /// 查询某一部门的所有用户
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="isActive"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        Task<IEnumerable<UsersViewModel>> GetAllInDepartment(string departmentId, bool isActive = true, int gender = 0);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(UsersAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(UsersUpdateViewModel model);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UsersViewModel> GetModel(string id);

        /// <summary>
        /// 获取当前登录用户的详细信息
        /// </summary>
        /// <returns></returns>
        Task<UsersDetailsViewModel> GetModel();

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> ResetPassword(UsersResetPasswordViewModel model);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdatePassword(UsersUpdatePasswordViewModel model);

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> EnableOrDisable(UsersUpdateStatusViewModel model);
    }
}
