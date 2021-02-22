using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 相册管理-> 相片管理
    /// </summary>
    public interface IClassPhotoalbumAttachmentRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="photoalbumId"></param>
        /// <returns></returns>
        Task<IPagedList<ClassPhotoalbumAttachment>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string photoalbumId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClassPhotoalbumAttachment> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(ClassPhotoalbumAttachment model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(string id);
    }
}
