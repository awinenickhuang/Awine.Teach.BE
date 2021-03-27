using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.IdpCenter.ExtensionGrants.SMS
{
    public class SmsContent
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ValidationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ExpiresMinute { get; set; }
    }
}
