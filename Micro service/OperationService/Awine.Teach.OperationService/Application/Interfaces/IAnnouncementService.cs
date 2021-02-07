using Awine.Framework.Core.Collections;
using Awine.Teach.OperationService.Application.ServiceResult;
using Awine.Teach.OperationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.OperationService.Application.Interfaces
{
    /// <summary>
    /// 平台公告
    /// </summary>
    public interface IAnnouncementService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IPagedList<AnnouncementViewModel>> GetPageList(int page = 1, int limit = 15);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AnnouncementViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(AnnouncementAddViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
