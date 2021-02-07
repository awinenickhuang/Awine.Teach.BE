using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Framework.Core.Exceptions
{
    /// <summary>
    ///  Exception type for domain exceptions
    /// </summary>
    public class AwineDomainException : System.Exception
    {
        public AwineDomainException()
        { }

        public AwineDomainException(string message)
            : base(message)
        { }

        public AwineDomainException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
