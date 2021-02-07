using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FinancialService.Domain.Interface
{
    /// <summary>
    /// 日常支出
    /// </summary>
    public interface IDailySpendingRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        Task<IPagedList<DailySpending>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string beginDate = "", string finishDate = "", string financialItemId = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        Task<IEnumerable<DailySpending>> GetAll(string tenantId = "", string financialItemId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(DailySpending model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(DailySpending model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
