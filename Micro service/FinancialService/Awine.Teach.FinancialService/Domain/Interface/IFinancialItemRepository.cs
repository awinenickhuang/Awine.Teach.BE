using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FinancialService.Domain.Interface
{
    /// <summary>
    /// 财务收支项目
    /// </summary>
    public interface IFinancialItemRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<IPagedList<FinancialItem>> GetPageList(int page = 1, int limit = 15, string tenantId = "", int status = 0);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<IEnumerable<FinancialItem>> GetAll(string tenantId = "", int status = 0);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FinancialItem> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<FinancialItem> GetModel(FinancialItem model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(FinancialItem model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(FinancialItem model);

        /// <summary>
        /// 更新 -> 启用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateStatus(FinancialItem model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
