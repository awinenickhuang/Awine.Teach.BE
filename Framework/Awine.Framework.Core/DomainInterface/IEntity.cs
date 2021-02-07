using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Core.DomainInterface
{
    public interface IEntity
    {
        /// <summary>
        /// 实体Id
        /// </summary>
        string Id { get; set; }
    }
}
