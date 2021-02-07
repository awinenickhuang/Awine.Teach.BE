using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Domain.Interface
{
    /// <summary>
    /// 咨询记录 -> 跟进记录
    /// </summary>
    public interface ICommunicationRecordRepository
    {
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="consultRecordId"></param>
        /// <returns></returns>
        Task<IPagedList<CommunicationRecord>> GetPageList(int page = 1, int limit = 15, string consultRecordId = "");

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <param name="trackingState"></param>
        /// <returns></returns>
        Task<bool> Add(CommunicationRecord model, int trackingState);
    }
}
