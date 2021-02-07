using Awine.Framework.Dapper.Extensions.Options;
using Awine.Framework.Identity;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.Services;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Awine.Teach.TeachingAffairService.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Awine.Teach.TeachingAffairService.Application.Extensions
{
    /// <summary>
    /// TeachingAffair Service ServiceCollection Extensions
    /// </summary>
    public static class TeachingAffairServiceExtensions
    {
        /// <summary>
        /// Add TeachingAffair Service MySQLProvider
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbProviderOptionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddTeachingAffairServiceMySQLProvider(this IServiceCollection services, Action<MySQLProviderOptions> dbProviderOptionsAction = null)
        {
            var options = new MySQLProviderOptions();
            services.AddSingleton(options);
            dbProviderOptionsAction?.Invoke(options);
            return services;
        }

        /// <summary>
        /// Add TeachingAffair Service Application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddTeachingAffairServiceApplication(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddOptions();

            services.Add(ServiceDescriptor.Singleton<IMarketingChannelRepository, MarketingChannelRepository>());
            services.Add(ServiceDescriptor.Singleton<IMarketingChannelService, MarketingChannelService>());

            services.Add(ServiceDescriptor.Singleton<IConsultRecordRepository, ConsultRecordRepository>());
            services.Add(ServiceDescriptor.Singleton<IConsultRecordService, ConsultRecordService>());

            services.Add(ServiceDescriptor.Singleton<ICommunicationRecordRepository, CommunicationRecordRepository>());
            services.Add(ServiceDescriptor.Singleton<ICommunicationRecordService, CommunicationRecordService>());

            services.Add(ServiceDescriptor.Singleton<ITrialClassRepository, TrialClassRepository>());
            services.Add(ServiceDescriptor.Singleton<ITrialClassService, TrialClassService>());

            services.Add(ServiceDescriptor.Singleton<IStudentRepository, StudentRepository>());
            services.Add(ServiceDescriptor.Singleton<IStudentService, StudentService>());

            services.Add(ServiceDescriptor.Singleton<IStudentCourseItemRepository, StudentCourseItemRepository>());
            services.Add(ServiceDescriptor.Singleton<IStudentCourseItemService, StudentCourseItemService>());

            services.Add(ServiceDescriptor.Singleton<IStudentCourseOrderRepository, StudentCourseOrderRepository>());
            services.Add(ServiceDescriptor.Singleton<IStudentCourseOrderService, StudentCourseOrderService>());

            services.Add(ServiceDescriptor.Singleton<IStudentAttendanceRepository, StudentAttendanceRepository>());
            services.Add(ServiceDescriptor.Singleton<IStudentAttendanceService, StudentAttendanceService>());

            services.Add(ServiceDescriptor.Singleton<ICourseRepository, CourseRepository>());
            services.Add(ServiceDescriptor.Singleton<ICourseService, CourseService>());

            services.Add(ServiceDescriptor.Singleton<ICourseChargeMannerRepository, CourseChargeMannerRepository>());
            services.Add(ServiceDescriptor.Singleton<ICourseChargeMannerService, CourseChargeMannerService>());

            services.Add(ServiceDescriptor.Singleton<IClassRoomRepository, ClassRoomRepository>());
            services.Add(ServiceDescriptor.Singleton<IClassRoomService, ClassRoomService>());

            services.Add(ServiceDescriptor.Singleton<IClassesRepository, ClassesRepository>());
            services.Add(ServiceDescriptor.Singleton<IClassesService, ClassesService>());

            services.Add(ServiceDescriptor.Singleton<ICourseScheduleRepository, CourseScheduleRepository>());
            services.Add(ServiceDescriptor.Singleton<ICourseScheduleService, CourseScheduleService>());

            services.Add(ServiceDescriptor.Singleton<IMakeupMissedLessonRepository, MakeupMissedLessonRepository>());
            services.Add(ServiceDescriptor.Singleton<IMakeupMissedLessonService, MakeupMissedLessonService>());

            services.Add(ServiceDescriptor.Singleton<IStudentGrowthRecordRepository, StudentGrowthRecordRepository>());
            services.Add(ServiceDescriptor.Singleton<IStudentGrowthRecordService, StudentGrowthRecordService>());

            services.Add(ServiceDescriptor.Singleton<IStudentGrowthRecordCommentRepository, StudentGrowthRecordCommentRepository>());
            services.Add(ServiceDescriptor.Singleton<IStudentGrowthRecordCommentService, StudentGrowthRecordCommentService>());

            // Identity
            services.Add(ServiceDescriptor.Singleton<ICurrentUser, CurrentUser>());

            services.Add(ServiceDescriptor.Singleton<ILegalHolidayRepository, LegalHolidayRepository>());
            services.Add(ServiceDescriptor.Singleton<ILegalHolidayService, LegalHolidayService>());

            services.Add(ServiceDescriptor.Singleton<IPaymentMethodRepository, PaymentMethodRepository>());
            services.Add(ServiceDescriptor.Singleton<IPaymentMethodService, PaymentMethodService>());

            return services;
        }
    }
}
