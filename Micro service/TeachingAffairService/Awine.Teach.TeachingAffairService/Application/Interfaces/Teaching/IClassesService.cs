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
    /// 班级管理
    /// </summary>
    public interface IClassesService
    {
        /// <summary>
        /// 所有数据
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        Task<IEnumerable<ClassesViewModel>> GetAll(string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "");

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="courseId"></param>
        /// <param name="recruitStatus"></param>
        /// <param name="typeOfClass"></param>
        /// <param name="beginDate"></param>
        /// <param name="finishDate"></param>
        /// <returns></returns>
        Task<IPagedList<ClassesViewModel>> GetPageList(int page = 1, int limit = 15, string name = "", string courseId = "", int recruitStatus = 0, int typeOfClass = 0, string beginDate = "", string finishDate = "");

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClassesViewModel> GetModel(string id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(ClassesAddViewModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Update(ClassesUpdateViewModel model);

        /// <summary>
        /// 更新班级招生状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> UpdateRecruitStatus(ClassesUpdateRecruitStatusViewModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(string id);

        /// <summary>
        /// 开班数量统计
        /// </summary>
        /// <param name="designatedMonth"></param>
        /// <returns></returns>
        Task<BasicLineChartViewModel> ClassNumberChartReport(string designatedMonth);
    }
}
