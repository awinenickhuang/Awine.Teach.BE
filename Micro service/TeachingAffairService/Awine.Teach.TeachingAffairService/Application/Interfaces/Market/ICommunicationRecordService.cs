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
    /// 咨询记录 -> 跟进记录
    /// </summary>
    public interface ICommunicationRecordService
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="consultRecordId"></param>
        /// <returns></returns>
        Task<IPagedList<CommunicationRecordViewModel>> GetPageList(int page = 1, int limit = 15, string consultRecordId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> Add(CommunicationRecordAddViewModel model);
    }
}
