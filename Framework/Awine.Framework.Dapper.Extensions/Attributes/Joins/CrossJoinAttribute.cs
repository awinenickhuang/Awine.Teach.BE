namespace Awine.Framework.Dapper.Extensions.Attributes.Joins
{
    /// <inheritdoc />
    /// <summary>
    ///     Generate RIGHT JOIN
    /// </summary>
    public sealed class CrossJoinAttribute : JoinAttributeBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     Constructor
        /// </summary>
        public CrossJoinAttribute()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="tableName">Name of external table</param>
        public CrossJoinAttribute(string tableName)
            : base(tableName, string.Empty, string.Empty, string.Empty, string.Empty, "CROSS JOIN")
        {
        }
    }
}
