using System.ComponentModel.DataAnnotations.Schema;

namespace LinqToKql
{
    /// <summary>
    /// IQueryable to KQL extension methods
    /// </summary>
    public static class Kql
    {
        /// <summary>
        /// Create an empty queryable to build the expression from
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Empty IQueryable<T></returns>
        public static IQueryable<T> Create<T>() where T : class
        {
            return Enumerable.Empty<T>().AsQueryable();
        }

        /// <summary>
        /// Take the built expression and convert to KQL statement
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="humanReadable">Whether to add newlines to make it more readable</param>
        /// <returns>KQL string</returns>
        public static string ToKql<T>(this IQueryable<T> query, bool humanReadable = false) 
        {
            var exp = query.Expression;

            var visitor = new KqlExpressionVisitor();
            var result = visitor.Translate(exp);

            var tableName = GetTableName<T>();

            return $"{tableName}{result}";
        }

        private static string GetTableName<T>()
        {
            var attr = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
            if (attr != null)
            {
                return attr.Name;
            }

            return typeof(T).Name;
        }
    }
}
