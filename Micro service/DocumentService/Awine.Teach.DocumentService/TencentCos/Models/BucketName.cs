using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.DocumentService.TencentCos
{
    /// <summary>
    /// 
    /// </summary>
    public class BucketName
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketName"></param>
        public BucketName(string bucketName)
        {
            (Name, AppId) = Parse(bucketName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="appId"></param>
        public BucketName(string name, string appId)
        {
            Name = name;
            AppId = appId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}-{AppId}";
        }

        /// <summary>
        /// Parse a string to <see cref="BucketName"/>
        /// </summary>
        /// <param name="bucketName">{Name}-{AppId}</param>
        /// <returns>(AppId, Name)</returns>
        public static (string Name, string AppId) Parse(string bucketName)
        {
            int pos = bucketName.LastIndexOf('-');

            if (pos > 0)
            {
                var name = bucketName.Substring(0, pos);
                var appId = bucketName.Substring(pos + 1);
                return (name, appId);
            }

            return (bucketName, "");
        }
    }
}
