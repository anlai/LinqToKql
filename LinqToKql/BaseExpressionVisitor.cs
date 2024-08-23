using System.Linq.Expressions;
using System.Text;

namespace LinqToKql
{
    internal abstract class BaseExpressionVisitor : ExpressionVisitor
    {
        protected StringBuilder kqlAccumulator;

        public string Translate(Expression expr)
        {
            kqlAccumulator = new StringBuilder();

            Visit(expr);

            return kqlAccumulator.ToString();
        }
    }
}
