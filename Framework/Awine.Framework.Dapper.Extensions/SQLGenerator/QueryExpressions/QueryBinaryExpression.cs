using System.Collections.Generic;

namespace Awine.Framework.Dapper.Extensions.SQLGenerator.QueryExpressions
{
    /// <inheritdoc />
    /// <summary>
    /// `Binary` Query Expression
    /// </summary>
    internal class QueryBinaryExpression : QueryExpression
    {
        public QueryBinaryExpression()
        {
            NodeType = QueryExpressionType.Binary;
        }

        public List<QueryExpression> Nodes { get; set; }

        public override string ToString()
        {
            return $"[{base.ToString()} ({string.Join(",", Nodes)})]";
        }
    }
}