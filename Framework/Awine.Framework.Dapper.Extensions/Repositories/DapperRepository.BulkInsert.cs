using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Awine.Framework.Dapper.Extensions.SQLGenerator;
using Dapper;


namespace Awine.Framework.Dapper.Extensions.Repositories
{
    /// <summary>
    /// Base Repository
    /// </summary>
    public partial class DapperRepository<TEntity>
        where TEntity : class
    {
        /// <inheritdoc />
        public virtual int BulkInsert(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            if (SQLGenerator.Provider == SqlProvider.MSSQL)
            {
                int count = 0;
                int totalInstances = instances.Count();

                var properties =
                    (SQLGenerator.IsIdentity
                        ? SQLGenerator.SqlProperties.Where(p => !p.PropertyName.Equals(SQLGenerator.IdentitySqlProperty.PropertyName, StringComparison.OrdinalIgnoreCase))
                        : SQLGenerator.SqlProperties).ToList();

                int exceededTimes = (int)Math.Ceiling(totalInstances * properties.Count / 2100d);
                if (exceededTimes > 1)
                {
                    int maxAllowedInstancesPerBatch = totalInstances / exceededTimes;

                    for (int i = 0; i <= exceededTimes; i++)
                    {
                        var skips = i * maxAllowedInstancesPerBatch;

                        if (skips >= totalInstances)
                            break;

                        var items = instances.Skip(skips).Take(maxAllowedInstancesPerBatch);
                        var msSqlQueryResult = SQLGenerator.GetBulkInsert(items);
                        count += Connection.Execute(msSqlQueryResult.GetSql(), msSqlQueryResult.Param, transaction);
                    }

                    return count;
                }
            }

            var queryResult = SQLGenerator.GetBulkInsert(instances);
            return Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction);
        }

        /// <inheritdoc />
        public virtual async Task<int> BulkInsertAsync(IEnumerable<TEntity> instances, IDbTransaction transaction = null)
        {
            if (SQLGenerator.Provider == SqlProvider.MSSQL)
            {
                int count = 0;
                int totalInstances = instances.Count();

                var properties =
                    (SQLGenerator.IsIdentity
                        ? SQLGenerator.SqlProperties.Where(p => !p.PropertyName.Equals(SQLGenerator.IdentitySqlProperty.PropertyName, StringComparison.OrdinalIgnoreCase))
                        : SQLGenerator.SqlProperties).ToList();

                int exceededTimes = (int)Math.Ceiling(totalInstances * properties.Count / 2100d);
                if (exceededTimes > 1)
                {
                    int maxAllowedInstancesPerBatch = totalInstances / exceededTimes;

                    for (int i = 0; i <= exceededTimes; i++)
                    {
                        var skips = i * maxAllowedInstancesPerBatch;

                        if (skips >= totalInstances)
                            break;

                        var items = instances.Skip(i * maxAllowedInstancesPerBatch).Take(maxAllowedInstancesPerBatch);
                        var msSqlQueryResult = SQLGenerator.GetBulkInsert(items);
                        count += await Connection.ExecuteAsync(msSqlQueryResult.GetSql(), msSqlQueryResult.Param, transaction);
                    }

                    return count;
                }
            }

            var queryResult = SQLGenerator.GetBulkInsert(instances);
            return await Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction).ConfigureAwait(false);
        }
    }
}
