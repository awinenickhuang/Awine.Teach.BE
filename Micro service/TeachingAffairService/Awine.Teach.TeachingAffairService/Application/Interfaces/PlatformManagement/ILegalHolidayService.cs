using Awine.Framework.Core.Collections;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Interfaces
{
    /// <summary>
    /// 法定假日
    /// </summary>
    public interface ILegalHolidayService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        Task<IPagedList<LegalHolidayViewModel>> GetPageList(int page = 1, int limit = 15, int year = 0);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        Task<IEnumerable<LegalHolidayViewModel>> GetAll(int year = 0);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(LegalHolidayAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(LegalHolidayUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
