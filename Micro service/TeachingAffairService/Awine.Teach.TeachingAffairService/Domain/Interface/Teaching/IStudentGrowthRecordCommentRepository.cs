using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 学生成长记录 -> 评论
    /// </summary>
    public interface IStudentGrowthRecordCommentRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentGrowthRecordId"></param>
        /// <returns></returns>
        Task<IPagedList<StudentGrowthRecordComment>> GetPageList(int page = 1, int limit = 15, string studentGrowthRecordId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentGrowthRecordComment> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> Add(StudentGrowthRecordComment model);
    }
}
