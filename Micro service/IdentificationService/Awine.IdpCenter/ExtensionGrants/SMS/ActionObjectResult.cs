using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.ExtensionGrants.SMS
{
    public class ActionObjectResult
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public int MessageType { get; set; }

    }

    public class ActionObjectResult<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public int MessageType { get; set; }
    }

    public class ActionObjectResult<T, T2>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public T2 Statistics { get; set; }

        public int MessageType { get; set; }
    }
}
