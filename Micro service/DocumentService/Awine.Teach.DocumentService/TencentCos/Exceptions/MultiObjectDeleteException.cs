using System.Collections.Generic;

namespace Awine.Teach.DocumentService.TencentCos
{
    public class MultiObjectDeleteException : CosServiceException
    {
        public List<DeleteError> Errors { get; set; } = new List<DeleteError>();

        public List<DeletedObject> DeletedObjects { get; set; } = new List<DeletedObject>();
    }
}
