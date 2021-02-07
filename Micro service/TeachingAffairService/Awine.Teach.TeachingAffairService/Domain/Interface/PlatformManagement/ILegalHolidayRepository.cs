using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 法定假日
    /// </summary>
    public interface ILegalHolidayRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        Task<IPagedList<LegalHoliday>> GetPageList(int page = 1, int limit = 15, int year = 0);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        Task<IEnumerable<LegalHoliday>> GetAll(int year = 0);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<LegalHoliday> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<LegalHoliday> GetModel(LegalHoliday model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(LegalHoliday model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(LegalHoliday model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
