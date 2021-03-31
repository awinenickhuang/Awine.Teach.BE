using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Framework.Core.SMSVerificationCode
{
    public class SmsOptions
    {
        public string Url { get; set; }
        public string SignatureCode { get; set; }

        public int ExpiresMinute { get; set; }
    }
}
