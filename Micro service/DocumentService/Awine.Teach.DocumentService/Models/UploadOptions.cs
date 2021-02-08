using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.DocumentService.Models
{
    public class UploadOptions
    {
        public long MaxLength { get; set; }

        public List<string> SupportedExtensions { get; } = new List<string>();

        public bool IsOverrideEnabled { get; set; }
    }

    public class CosUploadOptions : UploadOptions
    {
        public string CosStorageUri { get; set; }
    }

    public class FileUploadOptions : UploadOptions
    {
        public string FileStoragePath { get; set; }
    }
}
