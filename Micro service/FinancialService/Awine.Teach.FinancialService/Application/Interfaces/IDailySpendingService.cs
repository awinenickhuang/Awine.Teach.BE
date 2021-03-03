using Awine.Framework.AspNetCore.Model;
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
    /// 日常支出
    /// </summary>
    public interface IDailySpendingService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        Task<IPagedList<DailySpendingViewModel>> GetPageList(int page = 1, int limit = 15, string beginDate = "", string finishDate = "", string financialItemId = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="financialItemId"></param>
        /// <returns></returns>
        Task<IEnumerable<DailySpendingViewModel>> GetAll(string financialItemId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(DailySpendingAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(DailySpendingUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);

        /// <summary>
        /// 各项目常开销统计
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<PieChartViewModel> SpendingReport(string date);
    }
}
