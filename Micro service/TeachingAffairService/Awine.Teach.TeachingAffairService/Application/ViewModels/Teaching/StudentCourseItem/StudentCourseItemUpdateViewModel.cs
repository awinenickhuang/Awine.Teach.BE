using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 更新购买项目（报读课程）分配班级信息 -> 视图模型
    /// </summary>
    public class StudentCourseItemUpdateViewModel
    {
        /// <summary>
        /// 学生与班级信息
        /// </summary>
        public IList<StudentCourseItemAssignViewModel> StudentCourseItemAssignViewModel { get; set; } = new List<StudentCourseItemAssignViewModel>();
    }
}
