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
    /// 学生
    /// </summary>
    public class StudentService : IStudentService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<StudentService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// 正式学生
        /// </summary>
        private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// 学生报读课程 - 购买项目
        /// </summary>
        private readonly IStudentCourseItemRepository _studentCourseItemRepository;

        /// <summary>
        /// 班级信息
        /// </summary>
        private readonly IClassesRepository _classesRepository;

        /// <summary>
        /// 课程信息
        /// </summary>
        private readonly ICourseRepository _courseRepository;

        /// <summary>
        /// 课程定价标准
        /// </summary>
        private readonly ICourseChargeMannerRepository _courseChargeMannerRepository;

        /// <summary>
        /// 支付方式
        /// </summary>
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        /// <summary>
        /// 营销渠道
        /// </summary>
        private readonly IMarketingChannelRepository _marketingChannelRepository;

        /// <summary>
        /// 构造函数 - 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="studentRepository"></param>
        /// <param name="studentCourseItemRepository"></param>
        /// <param name="classesRepository"></param>
        /// <param name="courseRepository"></param>
        /// <param name="courseChargeMannerRepository"></param>
        /// <param name="paymentMethodRepository"></param>
        /// <param name="marketingChannelRepository"></param>
        public StudentService(
            IMapper mapper,
            ILogger<StudentService> logger,
            ICurrentUser user,
            IStudentRepository studentRepository,
            IStudentCourseItemRepository studentCourseItemRepository,
            IClassesRepository classesRepository,
            ICourseRepository courseRepository,
            ICourseChargeMannerRepository courseChargeMannerRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IMarketingChannelRepository marketingChannelRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _studentRepository = studentRepository;
            _studentCourseItemRepository = studentCourseItemRepository;
            _classesRepository = classesRepository;
            _courseRepository = courseRepository;
            _courseChargeMannerRepository = courseChargeMannerRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _marketingChannelRepository = marketingChannelRepository;
        }

        /// <summary>
        /// 所有学生数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StudentViewModel>> GetAll()
        {
            var entities = await _studentRepository.GetAll(_user.TenantId);

            return _mapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        public async Task<IPagedList<StudentViewModel>> GetPageList(int page = 1, int limit = 15, string name = "", int gender = 0, string phoneNumber = "", int learningProcess = 0)
        {
            var entities = await _studentRepository.GetPageList(page, limit, _user.TenantId, name, gender, phoneNumber, learningProcess);

            return _mapper.Map<IPagedList<Student>, IPagedList<StudentViewModel>>(entities);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(StudentAddViewModel model)
        {
            var student = _mapper.Map<StudentAddViewModel, Student>(model);
            student.TenantId = _user.TenantId;
            var existing = await _studentRepository.GetModel(student);
            if (null != existing)
            {
                return new Result { Success = false, Message = "姓名或电话已存在，请检查学生信息！" };
            }
            if (await _studentRepository.Add(student) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(StudentUpdateViewModel model)
        {
            var student = _mapper.Map<StudentUpdateViewModel, Student>(model);

            if (null == await _studentRepository.GetModel(student.Id))
            {
                return new Result { Success = false, Message = "未找到学生信息！" };
            }

            if (await _studentRepository.Update(student) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 取一个学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StudentViewModel> GetModel(string id)
        {
            var entity = await _studentRepository.GetModel(id);
            return _mapper.Map<Student, StudentViewModel>(entity);
        }

        /// <summary>
        /// 学生报名 -> 新生报名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Registration(StudentRegistrationViewModel model)
        {
            //学生信息
            var student = new Student()
            {
                Id = model.StudentId,
                Name = model.Name,
                Gender = model.Gender,
                Age = model.Age,
                IDNumber = model.IDNumber,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                NoteInformation = model.NoteInformation,
                TenantId = _user.TenantId
            };

            //添加租户标识
            student.TenantId = _user.TenantId;

            //新生报名时检查在读学生库避免重复报名
            var student_repeating = await _studentRepository.GetModel(model.StudentId);
            if (null != student_repeating)
            {
                return new Result { Success = false, Message = "报名信息重复！" };
            }

            //订单项目信息
            var studentOrderItem = new StudentCourseItem()
            {
                StudentId = student.Id,
                StudentName = student.Name,
                Gender = student.Gender,
                ClassesId = Guid.Empty.ToString(),
                ClassesName = "待分班",
                PurchaseQuantity = model.PurchaseQuantity,
                ConsumedQuantity = 0,
                RemainingNumber = model.PurchaseQuantity,
                TenantId = _user.TenantId
            };

            //验证课程信息
            var selectedCourse = await _courseRepository.GetModel(model.CourseId);
            if (null == selectedCourse)
            {
                return new Result { Success = false, Message = "未找到选报的课程信息！" };
            }

            studentOrderItem.CourseId = model.CourseId;
            studentOrderItem.CourseName = selectedCourse.Name;

            //订单信息
            var studentCourseOrder = new StudentCourseOrder()
            {
                StudentId = student.Id,
                StudentCourseItemId = studentOrderItem.Id,
                CourseId = model.CourseId,
                CourseName = selectedCourse.Name,
                ReceivableAmount = model.ReceivableAmount,
                DiscountAmount = model.DiscountAmount,
                RealityAmount = model.RealityAmount,
                OperatorId = _user.UserId,
                OperatorName = _user.Name,
                PurchaseQuantity = model.PurchaseQuantity,
                NoteInformation = model.NoteInformation,
                TenantId = _user.TenantId
            };

            //验证收费方式
            var selectedChargemanner = await _courseChargeMannerRepository.GetModel(model.ChargeMannerId);
            if (null == selectedChargemanner)
            {
                return new Result { Success = false, Message = "未找到选报课程的定价标准信息！" };
            }

            //验证学生选课记录
            var chargeMannerDesc = "按课时收费";

            if (selectedChargemanner.ChargeManner == 2)
            {
                chargeMannerDesc = "按月收费";
            }

            var orderItem = await _studentCourseItemRepository.GetAll(tenantId: _user.TenantId, studentId: model.StudentId, courseId: model.CourseId, chargeManner: selectedChargemanner.ChargeManner);

            if (orderItem.Where(x => x.LearningProcess == 1 || x.LearningProcess == 2 || x.LearningProcess == 3).Count() > 0)
            {
                return new Result { Success = false, Message = $"学生已报读定价方式为[{chargeMannerDesc}]的[{selectedCourse.Name}]课程，请进行缴费续费操作！" };
            }

            //记录学生报名时选报课程的定价标准信息
            studentOrderItem.ChargeManner = selectedChargemanner.ChargeManner;
            studentOrderItem.CourseDuration = selectedChargemanner.CourseDuration;
            studentOrderItem.TotalPrice = selectedChargemanner.TotalPrice;

            studentCourseOrder.ChargeManner = selectedChargemanner.ChargeManner;
            studentCourseOrder.CourseDuration = selectedChargemanner.CourseDuration;
            studentCourseOrder.TotalPrice = selectedChargemanner.TotalPrice;

            if (selectedChargemanner.ChargeManner == 1)
            {
                studentOrderItem.UnitPrice = selectedChargemanner.ChargeUnitPriceClassHour;
                studentCourseOrder.UnitPrice = selectedChargemanner.ChargeUnitPriceClassHour;
            }
            else if (selectedChargemanner.ChargeManner == 2)
            {
                studentOrderItem.UnitPrice = selectedChargemanner.ChargeUnitPriceMonth;
                studentCourseOrder.UnitPrice = selectedChargemanner.ChargeUnitPriceMonth;
            }
            else
            {
                return new Result { Success = false, Message = "课程定价标准不正确 - 只支持按课时或按月收费！" };
            }

            //支付方式
            var selectedPaymentMethod = await _paymentMethodRepository.GetModel(model.PaymentMethodId);
            if (null == selectedPaymentMethod)
            {
                return new Result { Success = false, Message = "未找到支付方式信息！" };
            }

            studentCourseOrder.PaymentMethodId = model.PaymentMethodId;
            studentCourseOrder.PaymentMethodName = selectedPaymentMethod.Name;

            //业绩归属
            studentCourseOrder.SalesStaffId = model.SalesStaffId;
            studentCourseOrder.SalesStaffName = model.SalesStaffName;

            //销售渠道
            var marketingChannel = await _marketingChannelRepository.GetModel(model.MarketingChannelId);

            if (null == marketingChannel)
            {
                return new Result { Success = false, Message = "未找到销售渠道信息！" };
            }

            studentCourseOrder.MarketingChannelId = model.MarketingChannelId;
            studentCourseOrder.MarketingChannelName = marketingChannel.Name;

            //提交报名信息
            if (await _studentRepository.Registration(student, studentOrderItem, studentCourseOrder))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 学生报名 -> 学生扩科
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> IncreaseLearningCourses(StudentIncreaseLearningCoursesViewModel model)
        {
            var student = await _studentRepository.GetModel(model.StudentId);

            if (null == student)
            {
                return new Result { Success = false, Message = "未找到学生信息！" };
            }

            //初始化报读课程
            var studentOrderItem = new StudentCourseItem()
            {
                StudentId = student.Id,
                StudentName = student.Name,
                Gender = student.Gender,
                ClassesId = Guid.Empty.ToString(),
                ClassesName = "待分班",
                PurchaseQuantity = model.PurchaseQuantity,
                ConsumedQuantity = 0,
                RemainingNumber = model.PurchaseQuantity,
                TenantId = _user.TenantId
            };

            //验证课程信息
            var selectedCourse = await _courseRepository.GetModel(model.CourseId);
            if (null == selectedCourse)
            {
                return new Result { Success = false, Message = "未找到选报的课程信息！" };
            }

            studentOrderItem.CourseId = model.CourseId;
            studentOrderItem.CourseName = selectedCourse.Name;

            //初始化订单信息
            var studentCourseOrder = new StudentCourseOrder()
            {
                StudentId = student.Id,
                StudentCourseItemId = studentOrderItem.Id,
                CourseId = model.CourseId,
                CourseName = selectedCourse.Name,
                ReceivableAmount = model.ReceivableAmount,
                DiscountAmount = model.DiscountAmount,
                RealityAmount = model.RealityAmount,
                OperatorId = _user.UserId,
                OperatorName = _user.Name,
                PurchaseQuantity = model.PurchaseQuantity,
                NoteInformation = model.NoteInformation,
                TenantId = _user.TenantId
            };

            //验证收费方式
            var selectedChargemanner = await _courseChargeMannerRepository.GetModel(model.ChargeMannerId);
            if (null == selectedChargemanner)
            {
                return new Result { Success = false, Message = "未找到选报课程的定价标准信息！" };
            }

            //验证学生选课记录
            var chargeMannerDesc = "按课时收费";

            if (selectedChargemanner.ChargeManner == 2)
            {
                chargeMannerDesc = "按月收费";
            }

            var orderItem = await _studentCourseItemRepository.GetAll(tenantId: _user.TenantId, studentId: model.StudentId, courseId: model.CourseId, chargeManner: selectedChargemanner.ChargeManner);

            if (orderItem.Where(x => x.LearningProcess == 1 || x.LearningProcess == 2 || x.LearningProcess == 3).Count() > 0)
            {
                return new Result { Success = false, Message = $"学生已报读定价方式为[{chargeMannerDesc}]的[{selectedCourse.Name}]课程，请进行缴费续费操作！" };
            }

            //记录学生报名时选报课程的定价标准信息
            studentOrderItem.ChargeManner = selectedChargemanner.ChargeManner;
            studentOrderItem.CourseDuration = selectedChargemanner.CourseDuration;
            studentOrderItem.TotalPrice = selectedChargemanner.TotalPrice;

            studentCourseOrder.ChargeManner = selectedChargemanner.ChargeManner;
            studentCourseOrder.CourseDuration = selectedChargemanner.CourseDuration;
            studentCourseOrder.TotalPrice = selectedChargemanner.TotalPrice;

            if (selectedChargemanner.ChargeManner == 1)
            {
                studentOrderItem.UnitPrice = selectedChargemanner.ChargeUnitPriceClassHour;
                studentCourseOrder.UnitPrice = selectedChargemanner.ChargeUnitPriceClassHour;
            }
            else if (selectedChargemanner.ChargeManner == 2)
            {
                studentOrderItem.UnitPrice = selectedChargemanner.ChargeUnitPriceMonth;
                studentCourseOrder.UnitPrice = selectedChargemanner.ChargeUnitPriceMonth;
            }
            else
            {
                return new Result { Success = false, Message = "课程定价标准不正确 - 只支持按课时或按月收费！" };
            }

            //支付方式
            var selectedPaymentMethod = await _paymentMethodRepository.GetModel(model.PaymentMethodId);
            if (null == selectedPaymentMethod)
            {
                return new Result { Success = false, Message = "未找到支付方式信息！" };
            }

            studentCourseOrder.PaymentMethodId = model.PaymentMethodId;
            studentCourseOrder.PaymentMethodName = selectedPaymentMethod.Name;

            //业绩归属
            studentCourseOrder.SalesStaffId = model.SalesStaffId;
            studentCourseOrder.SalesStaffName = model.SalesStaffName;

            //销售渠道
            var marketingChannel = await _marketingChannelRepository.GetModel(model.MarketingChannelId);

            if (null == marketingChannel)
            {
                return new Result { Success = false, Message = "未找到销售渠道信息！" };
            }

            studentCourseOrder.MarketingChannelId = model.MarketingChannelId;
            studentCourseOrder.MarketingChannelName = marketingChannel.Name;

            //提交报名信息
            if (await _studentRepository.IncreaseLearningCourses(studentOrderItem, studentCourseOrder))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 学生报名 -> 缴费续费
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> ContinueTopaytuition(StudentSupplementViewModel model)
        {
            //检查学生信息
            var supplementStudent = await _studentRepository.GetModel(model.StudentId);
            if (null == supplementStudent)
            {
                return new Result { Success = false, Message = "未找到学生信息！" };
            }

            //检查学生报读选课记录
            var existingStudentOrderItem = await _studentCourseItemRepository.GetModel(model.OrderItemId);
            if (null == existingStudentOrderItem)
            {
                return new Result { Success = false, Message = "未找到学生报读课程信息！" };
            }

            if (existingStudentOrderItem.LearningProcess == 2)
            {
                return new Result { Success = false, Message = "缴费项目已毕业 ，不支持缴费操作！" };
            }

            //初始化订单ID
            var studentOrderId = Guid.NewGuid().ToString();

            //项目信息
            var studentOrderItem = new StudentCourseItem()
            {
                TenantId = _user.TenantId,
                StudentId = supplementStudent.Id,
                StudentName = supplementStudent.Name,
                Gender = supplementStudent.Gender
            };

            //订单信息
            var studentCourseOrder = new StudentCourseOrder()
            {
                TenantId = _user.TenantId,
                StudentId = supplementStudent.Id,
                StudentCourseItemId = studentOrderItem.Id,
                CourseId = existingStudentOrderItem.CourseId,
                CourseName = existingStudentOrderItem.CourseName,
                OperatorId = _user.UserId,
                OperatorName = _user.Name,
                PurchaseQuantity = model.PurchaseQuantity,
                DiscountAmount = model.DiscountAmount,
                RealityAmount = model.RealityAmount,
                ReceivableAmount = model.ReceivableAmount,
                ChargeManner = existingStudentOrderItem.ChargeManner,
                NoteInformation = model.NoteInformation
            };

            //记录学生报名时选报课程的定价标准信息
            studentCourseOrder.ChargeManner = existingStudentOrderItem.ChargeManner;
            studentCourseOrder.CourseDuration = existingStudentOrderItem.CourseDuration;
            studentCourseOrder.TotalPrice = existingStudentOrderItem.TotalPrice;
            studentCourseOrder.UnitPrice = existingStudentOrderItem.UnitPrice;

            //支付方式
            var selectedPaymentMethod = await _paymentMethodRepository.GetModel(model.PaymentMethodId);
            if (null == selectedPaymentMethod)
            {
                return new Result { Success = false, Message = "未找到支付方式信息！" };
            }
            studentCourseOrder.PaymentMethodId = model.PaymentMethodId;
            studentCourseOrder.PaymentMethodName = selectedPaymentMethod.Name;

            //业绩归属
            studentCourseOrder.SalesStaffId = model.SalesStaffId;
            studentCourseOrder.SalesStaffName = model.SalesStaffName;

            //销售渠道
            var marketingChannel = await _marketingChannelRepository.GetModel(model.MarketingChannelId);
            if (null == marketingChannel)
            {
                return new Result { Success = false, Message = "未找到销售渠道信息！" };
            }
            studentCourseOrder.MarketingChannelId = model.MarketingChannelId;
            studentCourseOrder.MarketingChannelName = marketingChannel.Name;

            //购买总量 = 购买总量 + 本次购买量
            existingStudentOrderItem.PurchaseQuantity = existingStudentOrderItem.PurchaseQuantity + model.PurchaseQuantity;
            //剩余量 = 剩余量 + 本次购买量
            existingStudentOrderItem.RemainingNumber = existingStudentOrderItem.RemainingNumber + model.PurchaseQuantity;
            //该报读课程与订订单建立关联
            studentCourseOrder.StudentCourseItemId = existingStudentOrderItem.Id;
            //提交
            if (await _studentRepository.ContinueTopaytuition(existingStudentOrderItem, studentCourseOrder))
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
