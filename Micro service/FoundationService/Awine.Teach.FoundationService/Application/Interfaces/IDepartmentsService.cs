using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ServiceResult;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 部门
    /// </summary>
    public interface IDepartmentsService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IPagedList<DepartmentsViewModel>> GetPageList(int pageIndex = 1, int pageSize = 15, string tenantId = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        Task<IEnumerable<DepartmentsViewModel>> GetAll(string tenantId = "");

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DepartmentsViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(DepartmentsAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(DepartmentsUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
