using System.Data;
using Awine.Framework.Dapper.Extensions.SQLGenerator;

namespace Awine.Framework.Dapper.Extensions.Repositories
{
    /// <summary>
    ///     Base Repository
    /// </summary>
    public partial class DapperRepository<TEntity> : ReadOnlyDapperRepository<TEntity>, IDapperRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DapperRepository(IDbConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DapperRepository(IDbConnection connection, ISQLGenerator<TEntity> SQLGenerator)
            : base(connection, SQLGenerator)
        {
        }
    }
}
