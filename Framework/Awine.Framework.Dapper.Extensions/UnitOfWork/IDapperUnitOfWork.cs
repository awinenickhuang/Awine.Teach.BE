using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Dapper.Extensions.UnitOfWork
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IDapperUnitOfWork : IDisposable
    {
        void Commit();
    }
}
