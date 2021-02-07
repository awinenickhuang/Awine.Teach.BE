using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Awine.Framework.Dapper.Extensions.Options
{
    public class MySQLProviderOptions
    {
        public string ConnectionString { get; set; }

        public int CommandTimeOut { get; set; } = 3000;
    }
}
