using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.ViewModels
{
    /// <summary>
    /// 班级 -> 排课信息 -> 生成排课数据 -> 视图模型
    /// </summary>
    public class CourseScheduleAddViewModel
    {
        /// <summary>
        /// 班级标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "班级标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "班级标识不正确")]
        public string ClassId { get; set; }

        /// <summary>
        /// 课程标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "课程标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "课程标识不正确")]
        public string CourseId { get; set; }

        /// <summary>
        /// 教师标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "教师标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "教师标识不正确")]
        public string TeacherId { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [Required(ErrorMessage = "教师姓名必填"), MaxLength(6, ErrorMessage = "教师姓名太长啦"), MinLength(2, ErrorMessage = "教师姓名太短啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "教师姓名必须是中文")]
        public string TeacherName { get; set; }

        /// <summary>
        /// 教室标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "教室标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "教室标识不正确")]
        public string ClassRoomId { get; set; }

        /// <summary>
        /// 排课方式 1-每周循环 2-每天排课 3-单次排课 4-隔天排课
        /// </summary>
        [Required(ErrorMessage = "排课方式必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "排课方式只能输入正整数")]
        public int RepeatedWay { get; set; } = 1;

        /// <summary>
        /// 开课日期
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "开课日期必填")]
        [StringLength(32, ErrorMessage = "开课日期长度为10-32个字符", MinimumLength = 10)]
        public string ClassOpeningDate { get; set; }

        /// <summary>
        /// 间隔天数
        /// </summary>
        public int DaysBetween { get; set; } = 1;

        /// <summary>
        /// 是否排除掉法定节假日
        /// </summary>
        [Required(ErrorMessage = "是否排除掉法定节假日必填")]
        public bool ExclusionRule { get; set; } = false;

        /// <summary>
        /// 每周循环 -> 选中的周数据 
        /// </summary>
        public List<int> WeekDays { get; set; } = new List<int>();

        /// <summary>
        /// 课节数据集合
        /// </summary>
        public ClassTime ClassTime { get; set; } = new ClassTime();

        /// <summary>
        /// 结束方式
        /// </summary>
        public SescheduleEndWay SescheduleEndWay { get; set; } = new SescheduleEndWay();
    }

    /// <summary>
    /// 每节课开始上课及结束上课时间
    /// </summary>
    public class ClassTime
    {
        /// <summary>
        /// 开始几点
        /// </summary>
        [Required(ErrorMessage = "课节开始时间必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "课节开始时间只能输入正整数")]
        public int StartHours { get; set; } = 0;

        /// <summary>
        /// 开始几分
        /// </summary>
        [Required(ErrorMessage = "课节开始分钟必填")]
        [RegularExpression(@"^\+?[0-9][0-9]*$", ErrorMessage = "课节开始分钟只能输入整数")]
        public int StartMinutes { get; set; } = 0;

        /// <summary>
        /// 结束几点
        /// </summary>
        [Required(ErrorMessage = "课节结束时间必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "课节结束时间只能输入正整数")]
        public int EndHours { get; set; } = 0;

        /// <summary>
        /// 结几分
        /// </summary>
        [Required(ErrorMessage = "课节结束分钟必填")]
        [RegularExpression(@"^\+?[0-9][0-9]*$", ErrorMessage = "课节结束分钟只能输入整数")]
        public int EndMinutes { get; set; } = 0;
    }

    /// <summary>
    /// 课节数据 -> 每天支持排一到多节课 -> TO DO 暂未使用
    /// </summary>
    public class ClassesNumberEveryDay
    {
        /// <summary>
        /// 课节数据集合
        /// </summary>
        public List<ClassTime> ClassTimes { get; set; } = new List<ClassTime>();
    }

    /// <summary>
    /// 结束方式
    /// </summary>
    public class SescheduleEndWay
    {
        /// <summary>
        /// 结束方式 1-按总课节数 2-按结束日期
        /// </summary>
        public int SescheduleEndWayNumber { get; set; } = 1;

        /// <summary>
        /// 按总课节数
        /// </summary>
        public int SescheduleEndWaySessionsNumber { get; set; } = 0;

        /// <summary>
        /// 按结束日期
        /// </summary>
        public string SescheduleEndWayClassEndDate { get; set; } = "1983-01-01";
    }
}
