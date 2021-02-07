using Awine.Framework.Dapper.Extensions.Config;
using Awine.Framework.Dapper.Extensions.SQLGenerator;
using Awine.Framework.Dapper.Extensions.SQLGenerator.Filters;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Awine.Framework.Dapper.Extensions.Repositories
{
    /// <summary>
    ///     Base ReadOnlyRepository
    /// </summary>
    public partial class ReadOnlyDapperRepository<TEntity> : IReadOnlyDapperRepository<TEntity>
       where TEntity : class
    {
        /// <inheritdoc />
        public IDbConnection Connection { get; set; }

        /// <inheritdoc />
        public FilterData FilterData { get; set; }

        /// <inheritdoc />
        public ISQLGenerator<TEntity> SQLGenerator { get; }

        /// <summary>
        ///     Constructor
        /// </summary>
        public ReadOnlyDapperRepository(IDbConnection connection)
        {
            Connection = connection;
            FilterData = new FilterData();
            SQLGenerator = new SQLGenerator<TEntity>();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public ReadOnlyDapperRepository(IDbConnection connection, ISQLGenerator<TEntity> sqlGenerator)
        {
            Connection = connection;
            FilterData = new FilterData();
            SQLGenerator = sqlGenerator;
        }

        private static string GetProperty(Expression expression, Type type)
        {
            var field = (MemberExpression)expression;

            var prop = type.GetProperty(field.Member.Name);
            var declaringType = type.GetTypeInfo();
            var tableAttribute = declaringType.GetCustomAttribute<TableAttribute>();
            var tableName = AwineDapperConfig.TablePrefix + (tableAttribute != null ? tableAttribute.Name : declaringType.Name);

            if (prop == null || prop.GetCustomAttribute<NotMappedAttribute>() != null)
                return string.Empty;

            var name = prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name;
            return $"{tableName}.{name}";
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Connection.Dispose();
            Connection = null;
            if (FilterData == null) return;
            FilterData.LimitInfo = null;
            if (FilterData.OrderInfo != null)
            {
                FilterData.OrderInfo.Columns.Clear();
                FilterData.OrderInfo.Columns = null;
                FilterData.OrderInfo = null;
            }

            if (FilterData.SelectInfo != null)
            {
                FilterData.SelectInfo.Columns.Clear();
                FilterData.SelectInfo.Columns = null;
                FilterData.SelectInfo = null;
            }

            FilterData = null;
        }
    }
}
