using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 班级相册
    /// </summary>
    public interface IClassPhotoalbumRepository
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IEnumerable<ClassPhotoalbum>> GetAll(string tenantId = "", string classId = "", int visibleRange = 0, string startDate = "", string endDate = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IPagedList<ClassPhotoalbum>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string classId = "", int visibleRange = 0, string startDate = "", string endDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClassPhotoalbum> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ClassPhotoalbum> GetModel(ClassPhotoalbum model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(ClassPhotoalbum model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(ClassPhotoalbum model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);
    }
}
