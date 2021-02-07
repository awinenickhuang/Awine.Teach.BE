using System;
using System.Linq;
using System.Linq.Expressions;
using Awine.Framework.Dapper.Extensions.SQLGenerator.Filters;

namespace Awine.Framework.Dapper.Extensions.Repositories
{
    /// <summary>
    ///     Base Repository
    /// </summary>
    public partial class ReadOnlyDapperRepository<TEntity>
        where TEntity : class
    {
        /// <inheritdoc />
        public virtual IReadOnlyDapperRepository<TEntity> SetSelect<T>(Expression<Func<T, object>> expr, bool permanent)
        {
            if (FilterData.SelectInfo == null)
            {
                FilterData.SelectInfo = new SelectInfo();
            }

            FilterData.SelectInfo.Permanent = permanent;

            var type = typeof(T);
            if (expr.Body.NodeType == ExpressionType.Lambda)
            {
                var lambdaUnary = expr.Body as UnaryExpression;
                var expression = lambdaUnary.Operand as MemberExpression;
                var prop = GetProperty(expression, type);
                FilterData.SelectInfo.Columns.Add(prop);
            }
            else
            {
                var cols = (expr.Body as NewExpression)?.Arguments;
                foreach (var expression in cols)
                {
                    var prop = GetProperty(expression, type);
                    if (string.IsNullOrEmpty(prop))
                        continue;

                    FilterData.SelectInfo.Columns.Add(prop);
                }
            }

            return this;
        }

        /// <inheritdoc />
        public virtual IReadOnlyDapperRepository<TEntity> SetSelect(params string[] customSelect)
        {
            if (FilterData.SelectInfo == null)
            {
                FilterData.SelectInfo = new SelectInfo();
            }

            FilterData.SelectInfo.Columns = customSelect.ToList();

            return this;
        }

        /// <inheritdoc />
        public virtual IReadOnlyDapperRepository<TEntity> SetSelect(Expression<Func<TEntity, object>> expr)
        {
            return SetSelect(expr, false);
        }

        /// <inheritdoc />
        public virtual IReadOnlyDapperRepository<TEntity> SetSelect<T>(Expression<Func<T, object>> expr)
        {
            return SetSelect(expr, false);
        }
    }
}
