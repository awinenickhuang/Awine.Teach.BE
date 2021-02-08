using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.DocumentService.TencentCos
{
    public class DeleteError : CosEntity
    {
        public string Code { get; set; }

        public string Message { get; set; }
    }
}
