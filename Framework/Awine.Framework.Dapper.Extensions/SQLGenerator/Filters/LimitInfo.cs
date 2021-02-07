namespace Awine.Framework.Dapper.Extensions.SQLGenerator.Filters
{
    /// <summary>
    /// Limit settings
    /// </summary>
    public class LimitInfo
    {
        /// <summary>
        /// The limit; Should be greater than 0.
        /// </summary>
        public uint Limit { get; set; }

        /// <summary>
        /// The offset (optional); Used for pagination
        /// </summary>
        public uint? Offset { get; set; }

        /// <summary>
        /// If true, will be used for all queries
        /// </summary>
        public bool Permanent { get; set; }
    }
}
