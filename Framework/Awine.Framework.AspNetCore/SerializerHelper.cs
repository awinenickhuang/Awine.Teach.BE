using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Framework.AspNetCore
{
    /// <summary>
    /// 序列化
    /// </summary>
    public class SerializerHelper
    {
        /// <summary>
        /// JsonConverter
        /// </summary>
        /// <returns></returns>
        public static JsonConverter JsonTimeConverter()
        {
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"
            };
            return timeConverter;
        }
    }
}
