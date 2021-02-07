﻿using Awine.Framework.Core.Collections;
using Awine.WebSite.Applicaton.ServiceResult;
using Awine.WebSite.Applicaton.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Applicaton.Interface
{
    /// <summary>
    /// 管理员
    /// </summary>
    public interface IManagerService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IPagedList<ManagerViewModel>> GetPageList(int page = 1, int limit = 15);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ManagerViewModel>> GetAll();

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ManagerViewModel> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result<ManagerViewModel>> GetModel(ManagerLoginViewModel model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ManagerAddViewModel model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(ManagerUpdateViewModel model);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdatePassword(ManagerUpdatePasswordViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
