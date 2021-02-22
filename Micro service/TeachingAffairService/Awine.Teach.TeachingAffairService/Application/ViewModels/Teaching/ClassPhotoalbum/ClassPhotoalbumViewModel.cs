using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 班级相册 -> 视图模型
    /// </summary>
    public class ClassPhotoalbumViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 相册名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverPhoto { get; set; }

        /// <summary>
        /// 相册描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 可见范围 1-仅机构可见 2-机构及学生可见 3-完全公开
        /// </summary>
        public int VisibleRange { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
