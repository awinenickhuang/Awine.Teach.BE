using Awine.Framework.Core.Collections;
using Awine.Teach.FinancialService.Application.ServiceResult;
using Awine.Teach.FinancialService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FinancialService.Application.Interfaces
{
    /// <summary>
    /// 财务收支项目
    /// </summary>
    public interface IFinancialItemService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<IPagedList<FinancialItemViewModel>> GetPageList(int page = 1, int limit = 15, int status = 0);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<IEnumerable<FinancialItemViewModel>> GetAll(int status = 0);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FinancialItemViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(FinancialItemAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(FinancialItemUpdateViewModel model);

        /// <summary>
        /// 更新 -> 启用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateStatus(FinancialItemUpdateStatusViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
