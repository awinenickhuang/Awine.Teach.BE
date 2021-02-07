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
        public bool BulkUpdate(IEnumerable<TEntity> instances)
        {
            return BulkUpdate(instances, null);
        }

        /// <inheritdoc />
        public bool BulkUpdate(IEnumerable<TEntity> instances, IDbTransaction transaction)
        {
            if (SQLGenerator.Provider == SqlProvider.MSSQL)
            {
                int count = 0;
                int totalInstances = instances.Count();

                var properties = SQLGenerator.SqlProperties.ToList();

                int exceededTimes = (int) Math.Ceiling(totalInstances * properties.Count / 2100d);
                if (exceededTimes > 1)
                {
                    int maxAllowedInstancesPerBatch = totalInstances / exceededTimes;

                    for (int i = 0; i <= exceededTimes; i++)
                    {
                        var items = instances.Skip(i * maxAllowedInstancesPerBatch).Take(maxAllowedInstancesPerBatch);
                        var msSqlQueryResult = SQLGenerator.GetBulkUpdate(items);
                        count += Connection.Execute(msSqlQueryResult.GetSql(), msSqlQueryResult.Param, transaction);
                    }

                    return count > 0;
                }
            }

            var queryResult = SQLGenerator.GetBulkUpdate(instances);
            var result = Connection.Execute(queryResult.GetSql(), queryResult.Param, transaction) > 0;
            return result;
        }

        /// <inheritdoc />
        public Task<bool> BulkUpdateAsync(IEnumerable<TEntity> instances)
        {
            return BulkUpdateAsync(instances, null);
        }

        /// <inheritdoc />
        public async Task<bool> BulkUpdateAsync(IEnumerable<TEntity> instances, IDbTransaction transaction)
        {
            if (SQLGenerator.Provider == SqlProvider.MSSQL)
            {
                int count = 0;
                int totalInstances = instances.Count();

                var properties = SQLGenerator.SqlProperties.ToList();

                int exceededTimes = (int) Math.Ceiling(totalInstances * properties.Count / 2100d);
                if (exceededTimes > 1)
                {
                    int maxAllowedInstancesPerBatch = totalInstances / exceededTimes;

                    for (int i = 0; i <= exceededTimes; i++)
                    {
                        var items = instances.Skip(i * maxAllowedInstancesPerBatch).Take(maxAllowedInstancesPerBatch);
                        var msSqlQueryResult = SQLGenerator.GetBulkUpdate(items);
                        count += await Connection.ExecuteAsync(msSqlQueryResult.GetSql(), msSqlQueryResult.Param, transaction);
                    }

                    return count > 0;
                }
            }

            var queryResult = SQLGenerator.GetBulkUpdate(instances);
            var result = await Connection.ExecuteAsync(queryResult.GetSql(), queryResult.Param, transaction) > 0;
            return result;
        }
    }
}
