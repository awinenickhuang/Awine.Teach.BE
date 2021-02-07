using Awine.Framework.Core.Collections;
using Awine.Teach.TeachingAffairService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 学生成长记录
    /// </summary>
    public interface IStudentGrowthRecordRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<IPagedList<StudentGrowthRecord>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string studentId = "");

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<IEnumerable<StudentGrowthRecord>> GetAll(string tenantId = "", string studentId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentGrowthRecord> GetModel(string id);

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<StudentGrowthRecord> GetModel(StudentGrowthRecord model);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(StudentGrowthRecord model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Update(StudentGrowthRecord model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);
    }
}
