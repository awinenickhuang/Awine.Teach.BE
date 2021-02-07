using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 学生选课信息 -> 购买项目
    /// </summary>
    public class StudentCourseItemService : IStudentCourseItemService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ClassesService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 报读课程
        /// </summary>
        private readonly IStudentCourseItemRepository _studentCourseItemRepository;

        /// <summary>
        /// 班级信息
        /// </summary>
        private readonly IClassesRepository _classesRepository;

        /// <summary>
        /// 课节信息
        /// </summary>
        private ICourseScheduleRepository _classCourseScheduleRepository;

        /// <summary>
        /// 构造 -> 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="studentCourseItemRepository"></param>
        /// <param name="classesRepository"></param>
        /// <param name="classCourseScheduleRepository"></param>
        public StudentCourseItemService(
            IMapper mapper,
            ILogger<ClassesService> logger,
            ICurrentUser user,
            IStudentCourseItemRepository studentCourseItemRepository,
            IClassesRepository classesRepository,
            ICourseScheduleRepository classCourseScheduleRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _studentCourseItemRepository = studentCourseItemRepository;
            _classesRepository = classesRepository;
            _classCourseScheduleRepository = classCourseScheduleRepository;
        }

        /// <summary>
        /// 所有数据 -> 不分页列表
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StudentCourseItemViewModel>> GetAll(string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0)
        {
            var entities = await _studentCourseItemRepository.GetAll(_user.TenantId, studentId, courseId, classesId, studentOrderId, learningProcess);

            return _mapper.Map<IEnumerable<StudentCourseItem>, IEnumerable<StudentCourseItemViewModel>>(entities);
        }

        /// <summary>
        /// 所有数据 -> 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="classesId"></param>
        /// <param name="studentOrderId"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        public async Task<IPagedList<StudentCourseItemViewModel>> GetPageList(int page = 1, int limit = 15, string studentId = "", string courseId = "", string classesId = "", string studentOrderId = "", int learningProcess = 0)
        {
            var entities = await _studentCourseItemRepository.GetPageList(page, limit, _user.TenantId, studentId, courseId, classesId, studentOrderId, learningProcess);

            return _mapper.Map<IPagedList<StudentCourseItem>, IPagedList<StudentCourseItemViewModel>>(entities);
        }

        /// <summary>
        /// 把学生添加进班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> AddStudentsIntoClass(StudentCourseItemUpdateViewModel model)
        {
            if (model.StudentCourseItemAssignViewModel.Count <= 0)
            {
                return new Result { Success = false, Message = "不能提交空数据！" };
            }

            //本次添加学生时选中的班级
            var existClasses = await _classesRepository.GetModel(model.StudentCourseItemAssignViewModel[0].ClassesId);
            if (null == existClasses)
            {
                return new Result { Success = false, Message = "未找到班级信息！" };
            }

            //查一下这个班级已经有多少人了
            var existStudents = await _studentCourseItemRepository.GetAll(classesId: model.StudentCourseItemAssignViewModel[0].ClassesId);
            int existStudentCount = existStudents.Count();
            //本次添加多少人
            int currentStudentCount = model.StudentCourseItemAssignViewModel.Count;
            if (existClasses.TypeOfClass == 2)
            {
                if ((existStudentCount + currentStudentCount) > 1)
                {
                    return new Result { Success = false, Message = "一对一班级只允许添加一个学生！" };
                }
            }
            else
            {
                //还可以添加多少人
                if (currentStudentCount > (existClasses.ClassSize - existStudentCount))
                {
                    return new Result { Success = false, Message = $"[{existClasses.Name}]班级容量为[{existClasses.ClassSize}]人，已有[{existStudentCount}]人，本次添加[{currentStudentCount}]人，人数超限，添加失败！" };
                }
            }

            var studentCourseItems = new List<StudentCourseItem>();
            foreach (var item in model.StudentCourseItemAssignViewModel)
            {
                studentCourseItems.Add(new StudentCourseItem()
                {
                    Id = item.Id,
                    ClassesId = item.ClassesId,
                    ClassesName = existClasses.Name,
                    LearningProcess = 2
                });
            }

            int ownedStudents = existClasses.OwnedStudents + model.StudentCourseItemAssignViewModel.Count;

            var classes = new Classes()
            {
                Id = existClasses.Id,
                OwnedStudents = ownedStudents
            };

            if (await _studentCourseItemRepository.UpdateStudentsClassInformation(studentCourseItems, classes))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 把学生从班级中移除
        /// 移除操作不会改变学生的出勤记录或课时使用情况等信息。
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> RemoveStudentFromClass(StudentCourseItemUpdateViewModel model)
        {
            if (model.StudentCourseItemAssignViewModel.Count <= 0)
            {
                return new Result { Success = false, Message = "不能提交空数据！" };
            }

            var classesId = model.StudentCourseItemAssignViewModel[0].ClassesId;

            var existClasses = await _classesRepository.GetModel(classesId);
            if (null == existClasses)
            {
                return new Result { Success = false, Message = "未找到班级信息！" };
            }

            var studentCourseItems = new List<StudentCourseItem>();
            foreach (var item in model.StudentCourseItemAssignViewModel)
            {
                var courseItem = new StudentCourseItem()
                {
                    Id = item.Id,
                    ClassesId = Guid.Empty.ToString()
                };
                courseItem.ClassesName = "待分班";
                courseItem.LearningProcess = 1;
                studentCourseItems.Add(courseItem);
            }

            int ownedStudents = existClasses.OwnedStudents - model.StudentCourseItemAssignViewModel.Count;
            if (ownedStudents <= 0)
            {
                ownedStudents = 0;
            }
            var classes = new Classes()
            {
                Id = classesId,
                OwnedStudents = ownedStudents
            };

            if (await _studentCourseItemRepository.UpdateStudentsClassInformation(studentCourseItems, classes))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新报读课程的学习进度
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> UpdateLearningProcess(UpdateLearningProcessViewModel model)
        {
            var item = await _studentCourseItemRepository.GetModel(model.Id);

            if (null == item)
            {
                return new Result { Success = false, Message = "未找到报读课程信息！" };
            }

            //结课毕业时要检查数据吗？还是交给老师自己决定？
            //if (item.RemainingNumber > 0)
            //{
            //    return new Result { Success = false, Message = "报读课程有未消耗的数量！" };
            //}

            var process = new StudentCourseItem()
            {
                Id = model.Id,
                LearningProcess = model.LearningProcess
            };

            if (await _studentCourseItemRepository.UpdateLearningProcess(process) > 0)
            {
                //以下情况发送事件异步完成接下来的步骤
                switch (model.LearningProcess)
                {
                    case 1:// 1-在校
                        break;
                    case 2:// 2-停课
                        break;
                    case 3:// 3-退费
                        //TO DO
                        break;
                    case 4:// 4-毕业
                        //TO DO 如果进度为 - 结课毕业 则检查学生所有报读课程是否都已经结课毕业 ，如果是，则更新学习的学习进度为 - 结课毕业
                        break;
                    default:
                        break;

                }
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
