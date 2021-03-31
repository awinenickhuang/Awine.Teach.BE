using Awine.IdpCenter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.IRepositories
{
    public interface ISmsRecordRepository
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> InsertAsync(SmsRecord model);

        /// <summary>
        /// 按手机号查询验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<SmsRecord> GetAsync(string phone);
    }
}
