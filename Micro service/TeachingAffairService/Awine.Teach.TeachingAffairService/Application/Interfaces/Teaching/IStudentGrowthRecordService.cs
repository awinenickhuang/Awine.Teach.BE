using Awine.Framework.AspNetCore.Model;
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
    /// 学生成长记录
    /// </summary>
    public interface IStudentGrowthRecordService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task<IPagedList<StudentGrowthRecordViewModel>> GetPageList(int page = 1, int limit = 15, string studentId = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StudentGrowthRecordViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(StudentGrowthRecordAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(StudentGrowthRecordUpdateViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);

        /// <summary>
        /// 成长记录数量统计
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        Task<BasicLineChartViewModel> StudentGrowRecordNumberChartReport(string designatedMonth);
    }
}
