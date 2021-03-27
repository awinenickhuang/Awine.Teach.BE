using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.DocumentService.TencentCos
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletedObject : CosEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteMarker { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DeleteMarkerVersionId { get; set; }
    }
}
