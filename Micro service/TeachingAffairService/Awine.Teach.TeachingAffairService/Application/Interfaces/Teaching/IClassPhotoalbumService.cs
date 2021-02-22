using Awine.Framework.Core.Collections;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Interfaces
{
    /// <summary>
    /// 班级相册
    /// </summary>
    public interface IClassPhotoalbumService
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IEnumerable<ClassPhotoalbumViewModel>> GetAll(string classId = "", int visibleRange = 0, string startDate = "", string endDate = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="classId"></param>
        /// <param name="visibleRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IPagedList<ClassPhotoalbumViewModel>> GetPageList(int page = 1, int limit = 15, string classId = "", int visibleRange = 0, string startDate = "", string endDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClassPhotoalbumViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ClassPhotoalbumAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(ClassPhotoalbumUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);
    }
}
