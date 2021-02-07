using Awine.Framework.Core.Collections;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Infrastructure.Repository
{
    /// <summary>
    /// 学生
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<StudentRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public StudentRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<StudentRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 所有学生数据
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Student>> GetAll(string tenantId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_student WHERE IsDeleted=0 AND TenantId=@TenantId ORDER BY CreateTime DESC ");
                return await connection.QueryAsync<Student>(sqlStr.ToString(), new { TenantId = tenantId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="learningProcess"></param>
        /// <returns></returns>
        public async Task<IPagedList<Student>> GetPageList(int page = 1, int limit = 15, string tenantId = "", string name = "", int gender = 0, string phoneNumber = "", int learningProcess = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_ea_student WHERE IsDeleted=0 ");

                if (!string.IsNullOrEmpty(tenantId))
                {
                    sqlStr.Append(" AND TenantId=@TenantId ");
                    parameters.Add("@TenantId", tenantId);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    sqlStr.Append(" and Name Like @Name");
                    parameters.Add("@Name", "%" + name + "%");
                }

                if (gender > 0)
                {
                    sqlStr.Append(" AND Gender=@Gender ");
                    parameters.Add("@Gender", gender);
                }

                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    sqlStr.Append(" AND PhoneNumber Like @PhoneNumber ");
                    parameters.Add("@PhoneNumber", "%" + phoneNumber + "%");
                }

                if (learningProcess > 0)
                {
                    sqlStr.Append(" AND LearningProcess=@LearningProcess ");
                    parameters.Add("LearningProcess", learningProcess);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<Student>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Student> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_student WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<Student>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Student> GetModel(Student model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_ea_student WHERE (Name=@Name OR PhoneNumber=@PhoneNumber) AND TenantId=@TenantId ");
                if (!string.IsNullOrEmpty(model.Id))
                {
                    sqlStr.Append(" AND Id!=@Id ");
                }
                return await connection.QueryFirstOrDefaultAsync<Student>(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<int> Add(Student student)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" (Id,Name,Gender,Age,IDNumber,PhoneNumber,Address,NoteInformation,LearningProcess,TenantId,IsDeleted,CreateTime) VALUES (@Id,@Name,@Gender,@Age,@IDNumber,@PhoneNumber,@Address,@NoteInformation,@LearningProcess,@TenantId,@IsDeleted,@CreateTime)");
                return await connection.ExecuteAsync(sqlStr.ToString(), student, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Student model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" UPDATE t_ea_student SET `Gender`=@Gender,IDNumber=@IDNumber,PhoneNumber=@PhoneNumber,Address=@Address,NoteInformation=@NoteInformation WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                string sqlStr = "DELETE FROM t_ea_student WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 学生报名 -> 新生报名
        /// </summary>
        /// <param name="student"></param>
        /// <param name="studentCourseItem"></param>
        /// <param name="studentCourseOrder"></param>
        /// <returns></returns>
        public async Task<bool> Registration(Student student, StudentCourseItem studentCourseItem, StudentCourseOrder studentCourseOrder)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //写入学生信息
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO t_ea_student ");
                        sqlStr.Append(" (Id,Name,Gender,Age,IDNumber,PhoneNumber,Address,NoteInformation,LearningProcess,TenantId,IsDeleted,CreateTime) VALUES (@Id,@Name,@Gender,@Age,@IDNumber,@PhoneNumber,@Address,@NoteInformation,@LearningProcess,@TenantId,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), student, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入订单项目
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO t_ea_student_course_item ");
                        sqlStr.Append("(Id,StudentId,StudentName,Gender,CourseId,CourseName,ClassesId,ClassesName,PurchaseQuantity,ConsumedQuantity,RemainingNumber,ChargeManner,CourseDuration,TotalPrice,UnitPrice,LearningProcess,TenantId,IsDeleted,CreateTime)");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append("(@Id,@StudentId,@StudentName,@Gender,@CourseId,@CourseName,@ClassesId,@ClassesName,@PurchaseQuantity,@ConsumedQuantity,@RemainingNumber,@ChargeManner,@CourseDuration,@TotalPrice,@UnitPrice,@LearningProcess,@TenantId,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentCourseItem, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入订单信息
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO t_ea_student_course_order ");
                        sqlStr.Append(" (Id,StudentId,StudentCourseItemId,CourseId,CourseName,ReceivableAmount,DiscountAmount,RealityAmount,PaymentMethodId,PaymentMethodName,NoteInformation,OperatorId,OperatorName,SalesStaffId,SalesStaffName,MarketingChannelId,MarketingChannelName,PurchaseQuantity,ChargeManner,CourseDuration,TotalPrice,UnitPrice,TenantId,IsDeleted,CreateTime) VALUES (@Id,@StudentId,@StudentCourseItemId,@CourseId,@CourseName,@ReceivableAmount,@DiscountAmount,@RealityAmount,@PaymentMethodId,@PaymentMethodName,@NoteInformation,@OperatorId,@OperatorName,@SalesStaffId,@SalesStaffName,@MarketingChannelId,@MarketingChannelName,@PurchaseQuantity,@ChargeManner,@CourseDuration,@TotalPrice,@UnitPrice,@TenantId,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentCourseOrder, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //更新咨询记录状态为 - 已成交
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_market_consult_record SET `TrackingState`=6 WHERE Id=@Id ");
                        await connection.ExecuteAsync(sqlStr.ToString(), new { Id = student.Id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"学生报名时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 学生报名 -> 学生扩科
        /// </summary>
        /// <param name="studentCourseItem"></param>
        /// <param name="studentCourseOrder"></param>
        /// <returns></returns>
        public async Task<bool> IncreaseLearningCourses(StudentCourseItem studentCourseItem, StudentCourseOrder studentCourseOrder)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //写入订单项目
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO t_ea_student_course_item ");
                        sqlStr.Append("(Id,StudentId,StudentName,Gender,CourseId,CourseName,ClassesId,ClassesName,PurchaseQuantity,ConsumedQuantity,RemainingNumber,ChargeManner,CourseDuration,TotalPrice,UnitPrice,LearningProcess,TenantId,IsDeleted,CreateTime)");
                        sqlStr.Append(" VALUES ");
                        sqlStr.Append("(@Id,@StudentId,@StudentName,@Gender,@CourseId,@CourseName,@ClassesId,@ClassesName,@PurchaseQuantity,@ConsumedQuantity,@RemainingNumber,@ChargeManner,@CourseDuration,@TotalPrice,@UnitPrice,@LearningProcess,@TenantId,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentCourseItem, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入订单信息
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO t_ea_student_course_order ");
                        sqlStr.Append(" (Id,StudentId,StudentCourseItemId,CourseId,CourseName,ReceivableAmount,DiscountAmount,RealityAmount,PaymentMethodId,PaymentMethodName,NoteInformation,OperatorId,OperatorName,SalesStaffId,SalesStaffName,MarketingChannelId,MarketingChannelName,PurchaseQuantity,ChargeManner,CourseDuration,TotalPrice,UnitPrice,TenantId,IsDeleted,CreateTime) VALUES (@Id,@StudentId,@StudentCourseItemId,@CourseId,@CourseName,@ReceivableAmount,@DiscountAmount,@RealityAmount,@PaymentMethodId,@PaymentMethodName,@NoteInformation,@OperatorId,@OperatorName,@SalesStaffId,@SalesStaffName,@MarketingChannelId,@MarketingChannelName,@PurchaseQuantity,@ChargeManner,@CourseDuration,@TotalPrice,@UnitPrice,@TenantId,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentCourseOrder, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"学生报名-扩科-时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 学生报名 -> 缴费续费
        /// </summary>
        /// <param name="studentCourseItem"></param>
        /// <param name="studentCourseOrder"></param>
        /// <returns></returns>
        public async Task<bool> ContinueTopaytuition(StudentCourseItem studentCourseItem, StudentCourseOrder studentCourseOrder)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        StringBuilder sqlStr = new StringBuilder();

                        //更新订单项目
                        sqlStr.Clear();
                        sqlStr.Append(" UPDATE t_ea_student_course_item ");
                        sqlStr.Append("SET RemainingNumber=@RemainingNumber,PurchaseQuantity=@PurchaseQuantity WHERE Id=@Id");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentCourseItem, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        //写入订单信息
                        sqlStr.Clear();
                        sqlStr.Append(" INSERT INTO t_ea_student_course_order ");
                        sqlStr.Append(" (Id,StudentId,StudentCourseItemId,CourseId,CourseName,ReceivableAmount,DiscountAmount,RealityAmount,PaymentMethodId,PaymentMethodName,NoteInformation,OperatorId,OperatorName,SalesStaffId,SalesStaffName,MarketingChannelId,MarketingChannelName,PurchaseQuantity,ChargeManner,CourseDuration,TotalPrice,UnitPrice,TenantId,IsDeleted,CreateTime) VALUES (@Id,@StudentId,@StudentCourseItemId,@CourseId,@CourseName,@ReceivableAmount,@DiscountAmount,@RealityAmount,@PaymentMethodId,@PaymentMethodName,@NoteInformation,@OperatorId,@OperatorName,@SalesStaffId,@SalesStaffName,@MarketingChannelId,@MarketingChannelName,@PurchaseQuantity,@ChargeManner,@CourseDuration,@TotalPrice,@UnitPrice,@TenantId,@IsDeleted,@CreateTime)");
                        await connection.ExecuteAsync(sqlStr.ToString(), studentCourseOrder, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"学生报名-缴费续费-时发生异常-{ex.Message}");
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
