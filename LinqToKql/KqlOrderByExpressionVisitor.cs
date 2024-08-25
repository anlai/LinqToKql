using System.Linq.Expressions;

namespace LinqToKql
{
    internal class KqlOrderByExpressionVisitor : BaseExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            this.kqlAccumulator.Append(node.Member.Name);

            return node;
        }
    }
}
