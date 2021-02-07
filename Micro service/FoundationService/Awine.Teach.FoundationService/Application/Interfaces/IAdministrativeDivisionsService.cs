using Awine.Teach.FoundationService.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Application.Interfaces
{
    /// <summary>
    /// 行政区域划分
    /// </summary>
    public interface IAdministrativeDivisionsService
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AdministrativeDivisionsViewModel>> GetAll();

        /// <summary>
        /// 获取下级区域数据
        /// </summary>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        Task<IEnumerable<AdministrativeDivisionsViewModel>> GetSubordinateRegionalism(int parentCode);
    }
}
