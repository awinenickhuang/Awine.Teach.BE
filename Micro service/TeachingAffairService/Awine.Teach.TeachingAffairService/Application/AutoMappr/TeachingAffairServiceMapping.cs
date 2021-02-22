using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.AutoMappr
{
    /// <summary>
    /// 教务服务 -> 实体与视图模型映射配置
    /// </summary>
    public class TeachingAffairServiceMapping : Profile
    {
        /// <summary>
        /// 映射关系配置
        /// </summary>
        public TeachingAffairServiceMapping()
        {
            //营销渠道
            CreateMap<MarketingChannel, MarketingChannelViewModel>().ReverseMap();
            CreateMap<MarketingChannelAddViewModel, MarketingChannel>();
            CreateMap<MarketingChannelUpdateViewModel, MarketingChannel>();
            CreateMap<IPagedList<MarketingChannel>, IPagedList<MarketingChannelViewModel>>().ConvertUsing<PagedListConverter<MarketingChannel, MarketingChannelViewModel>>();

            //咨询记录
            CreateMap<ConsultRecord, ConsultRecordChartViewModel>();
            CreateMap<ConsultRecord, ConsultRecordViewModel>().ReverseMap();
            CreateMap<ConsultRecordAddViewModel, ConsultRecord>();
            CreateMap<ConsultRecordUpdateViewModel, ConsultRecord>();
            CreateMap<IPagedList<ConsultRecord>, IPagedList<ConsultRecordViewModel>>().ConvertUsing<PagedListConverter<ConsultRecord, ConsultRecordViewModel>>();

            //咨询记录 -> 跟踪记录
            CreateMap<CommunicationRecord, CommunicationRecordViewModel>().ReverseMap();
            CreateMap<CommunicationRecordAddViewModel, CommunicationRecord>();
            CreateMap<IPagedList<CommunicationRecord>, IPagedList<CommunicationRecordViewModel>>().ConvertUsing<PagedListConverter<CommunicationRecord, CommunicationRecordViewModel>>();

            //试听记录
            CreateMap<TrialClass, TrialClassViewModel>().ReverseMap();
            CreateMap<IPagedList<TrialClass>, IPagedList<TrialClassViewModel>>().ConvertUsing<PagedListConverter<TrialClass, TrialClassViewModel>>();

            //学生管理
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<StudentAddViewModel, Student>();
            CreateMap<StudentUpdateViewModel, Student>();
            CreateMap<IPagedList<Student>, IPagedList<StudentViewModel>>().ConvertUsing<PagedListConverter<Student, StudentViewModel>>();

            //购买项目
            CreateMap<StudentCourseItem, StudentCourseItemViewModel>().ReverseMap();
            CreateMap<IPagedList<StudentCourseItem>, IPagedList<StudentCourseItemViewModel>>().ConvertUsing<PagedListConverter<StudentCourseItem, StudentCourseItemViewModel>>();

            //订单信息
            CreateMap<StudentCourseOrder, StudentCourseOrderViewModel>().ReverseMap();
            CreateMap<IPagedList<StudentCourseOrder>, IPagedList<StudentCourseOrderViewModel>>().ConvertUsing<PagedListConverter<StudentCourseOrder, StudentCourseOrderViewModel>>();

            //学生考勤
            CreateMap<StudentAttendance, StudentAttendanceViewModel>().ReverseMap();
            CreateMap<IPagedList<StudentAttendance>, IPagedList<StudentAttendanceViewModel>>().ConvertUsing<PagedListConverter<StudentAttendance, StudentAttendanceViewModel>>();

            //学生成长记录
            CreateMap<StudentGrowthRecord, StudentGrowthRecordViewModel>().ReverseMap();
            CreateMap<StudentGrowthRecordAddViewModel, StudentGrowthRecord>();
            CreateMap<StudentGrowthRecordUpdateViewModel, StudentGrowthRecord>();
            CreateMap<IPagedList<StudentGrowthRecord>, IPagedList<StudentGrowthRecordViewModel>>().ConvertUsing<PagedListConverter<StudentGrowthRecord, StudentGrowthRecordViewModel>>();

            //学生成长记录评论
            CreateMap<StudentGrowthRecordComment, StudentGrowthRecordCommentViewModel>().ReverseMap();
            CreateMap<StudentGrowthRecordAddViewModel, StudentGrowthRecordComment>();
            CreateMap<IPagedList<StudentGrowthRecordComment>, IPagedList<StudentGrowthRecordCommentViewModel>>().ConvertUsing<PagedListConverter<StudentGrowthRecordComment, StudentGrowthRecordCommentViewModel>>();

            //课程管理
            CreateMap<Course, CourseViewModel>().ReverseMap();
            CreateMap<CourseAddViewModel, Course>();
            CreateMap<CourseUpdateViewModel, Course>();
            CreateMap<IPagedList<Course>, IPagedList<CourseViewModel>>().ConvertUsing<PagedListConverter<Course, CourseViewModel>>();

            //课程收费方式
            CreateMap<CourseChargeManner, CourseChargeMannerViewModel>().ReverseMap();
            CreateMap<CourseChargeMannerAddViewModel, CourseChargeManner>();
            CreateMap<CourseChargeMannerUpdateViewModel, CourseChargeManner>();
            CreateMap<IPagedList<CourseChargeManner>, IPagedList<CourseChargeMannerViewModel>>().ConvertUsing<PagedListConverter<CourseChargeManner, CourseChargeMannerViewModel>>();

            //教室管理
            CreateMap<ClassRoom, ClassRoomViewModel>().ReverseMap();
            CreateMap<ClassRoomAddViewModel, ClassRoom>();
            CreateMap<ClassRoomUpdateViewModel, ClassRoom>();
            CreateMap<IPagedList<ClassRoom>, IPagedList<ClassRoomViewModel>>().ConvertUsing<PagedListConverter<ClassRoom, ClassRoomViewModel>>();

            //班级管理
            CreateMap<Classes, ClassesViewModel>().ReverseMap();
            CreateMap<ClassesAddViewModel, Classes>();
            CreateMap<ClassesUpdateViewModel, Classes>();
            CreateMap<IPagedList<Classes>, IPagedList<ClassesViewModel>>().ConvertUsing<PagedListConverter<Classes, ClassesViewModel>>();

            //班级相册管理
            CreateMap<ClassPhotoalbum, ClassPhotoalbumViewModel>().ReverseMap();
            CreateMap<ClassPhotoalbumAddViewModel, ClassPhotoalbum>();
            CreateMap<ClassPhotoalbumUpdateViewModel, ClassPhotoalbum>();
            CreateMap<IPagedList<ClassPhotoalbum>, IPagedList<ClassPhotoalbumViewModel>>().ConvertUsing<PagedListConverter<ClassPhotoalbum, ClassPhotoalbumViewModel>>();

            //班级相册 -> 相片管理
            CreateMap<ClassPhotoalbumAttachment, ClassPhotoalbumAttachmentViewModel>().ReverseMap();
            CreateMap<ClassPhotoalbumAttachmentAddViewModel, ClassPhotoalbumAttachment>();
            CreateMap<IPagedList<ClassPhotoalbumAttachment>, IPagedList<ClassPhotoalbumAttachmentViewModel>>().ConvertUsing<PagedListConverter<ClassPhotoalbumAttachment, ClassPhotoalbumAttachmentViewModel>>();

            //班级 -> 排课信息
            CreateMap<CourseSchedule, CourseScheduleViewModel>().ReverseMap();
            CreateMap<CourseScheduleUpdateViewModel, CourseSchedule>();
            CreateMap<IPagedList<CourseSchedule>, IPagedList<CourseScheduleViewModel>>().ConvertUsing<PagedListConverter<CourseSchedule, CourseScheduleViewModel>>();

            //法定假日
            CreateMap<LegalHoliday, LegalHolidayViewModel>().ReverseMap();
            CreateMap<LegalHolidayAddViewModel, LegalHoliday>();
            CreateMap<LegalHolidayUpdateViewModel, LegalHoliday>();
            CreateMap<IPagedList<LegalHoliday>, IPagedList<LegalHolidayViewModel>>().ConvertUsing<PagedListConverter<LegalHoliday, LegalHolidayViewModel>>();

            //支付方式
            CreateMap<PaymentMethod, PaymentMethodViewModel>().ReverseMap();
            CreateMap<PaymentMethodAddViewModel, PaymentMethod>();
            CreateMap<PaymentMethodUpdateViewModel, PaymentMethod>();
            CreateMap<IPagedList<PaymentMethod>, IPagedList<PaymentMethodViewModel>>().ConvertUsing<PagedListConverter<PaymentMethod, PaymentMethodViewModel>>();

            //补课管理
            CreateMap<MakeupMissedLesson, MakeupMissedLessonViewModel>().ReverseMap();
            CreateMap<MakeupMissedLessonAddViewModel, MakeupMissedLesson>();
            CreateMap<MakeupMissedLessonUpdateViewModel, MakeupMissedLesson>();
            CreateMap<IPagedList<MakeupMissedLesson>, IPagedList<MakeupMissedLessonViewModel>>().ConvertUsing<PagedListConverter<MakeupMissedLesson, MakeupMissedLessonViewModel>>();

            //补课学生管理
            CreateMap<MakeupMissedLessonStudent, MakeupMissedLessonStudentViewModel>()
                .ForMember(dest => dest.StudentCourseItemViewModel, options => options.MapFrom(src => src.StudentCourseItem)); ;
            CreateMap<IPagedList<MakeupMissedLessonStudent>, IPagedList<MakeupMissedLessonStudentViewModel>>().ConvertUsing<PagedListConverter<MakeupMissedLessonStudent, MakeupMissedLessonStudentViewModel>>();
        }
    }
}
