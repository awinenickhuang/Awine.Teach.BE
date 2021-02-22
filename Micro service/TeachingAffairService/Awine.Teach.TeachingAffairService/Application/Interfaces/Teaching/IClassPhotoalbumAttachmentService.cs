using Awine.Framework.Core.Collections;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Interfaces
{
    /// <summary>
    /// 相册管理-> 相片管理
    /// </summary>
    public interface IClassPhotoalbumAttachmentService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="photoalbumId"></param>
        /// <returns></returns>
        Task<IPagedList<ClassPhotoalbumAttachmentViewModel>> GetPageList(int page = 1, int limit = 15, string photoalbumId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClassPhotoalbumAttachmentViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ClassPhotoalbumAttachmentAddViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
