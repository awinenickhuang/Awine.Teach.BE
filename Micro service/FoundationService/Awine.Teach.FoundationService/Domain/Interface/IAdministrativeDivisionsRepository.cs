﻿using Awine.Teach.FoundationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.FoundationService.Domain.Interface
{
    /// <summary>
    /// 行政区域划分
    /// </summary>
    public interface IAdministrativeDivisionsRepository
    {
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AdministrativeDivisions>> GetAll();

        /// <summary>
        /// 获取下级区域数据
        /// </summary>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        Task<IEnumerable<AdministrativeDivisions>> GetSubordinateRegionalism(int parentCode);
    }
}
