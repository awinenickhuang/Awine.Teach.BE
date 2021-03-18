using Awine.Framework.Core.Collections;
using Awine.Teach.FoundationService.Application.ServiceResult;
using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 应用版本
    /// </summary>
    public interface IApplicationVersionService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        Task<IPagedList<ApplicationVersionViewModel>> GetPageList(int pageIndex = 1, int pageSize = 15, string name = "", string identifying = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="name"></param>
        /// <param name="identifying"></param>
        /// <returns></returns>
        Task<IEnumerable<ApplicationVersionViewModel>> GetAll(string name = "", string identifying = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ApplicationVersionAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(ApplicationVersionUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApplicationVersionViewModel> GetModel(string id);
    }
}
