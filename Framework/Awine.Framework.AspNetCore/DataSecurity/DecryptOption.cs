using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore.DataSecurity
{
    public class DecryptOption
    {
        public string PrivateKey { get; set; }

        public string PublicKey { get; set; }
    }
}
