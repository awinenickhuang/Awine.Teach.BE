using Awine.Framework.Dapper.Extensions.SQLGenerator;

namespace Awine.Framework.Dapper.Extensions.Config
{
    /// <summary>
    /// This class is used to support dependency injection
    /// </summary>
    public static class AwineDapperConfig
    {
        /// <summary>
        /// Type Sql provider
        /// </summary>
        public static SqlProvider SqlProvider { get; set; }

        /// <summary>
        /// Use quotation marks for TableName and ColumnName
        /// </summary>
        public static bool UseQuotationMarks { get; set; }

        /// <summary>
        /// Prefix for tables
        /// </summary>
        public static string TablePrefix { get; set; }
    }
}
