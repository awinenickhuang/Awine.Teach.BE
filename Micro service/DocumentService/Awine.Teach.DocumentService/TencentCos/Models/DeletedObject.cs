using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.DocumentService.TencentCos
{
    public class DeletedObject : CosEntity
    {
        public bool DeleteMarker { get; set; }

        public string DeleteMarkerVersionId { get; set; }
    }
}
