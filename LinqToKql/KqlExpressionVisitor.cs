using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace LinqToKql
{
    internal class KqlExpressionVisitor : ExpressionVisitor
    {
        private StringBuilder kqlAccumulator;

        public string Translate(Expression expression)
        {
            this.kqlAccumulator = new StringBuilder();
            this.Visit(expression);
            return this.kqlAccumulator.ToString();
        }

        //public override bool Equals(object? obj)
        //{
        //    return base.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        //public override string? ToString()
        //{
        //    return base.ToString();
        //}

        //[return: NotNullIfNotNull("node")]
        //public override Expression? Visit(Expression? node)
        //{
        //    return base.Visit(node);
        //}

        protected override Expression VisitBinary(BinaryExpression node)
        {
            this.Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    this.kqlAccumulator.Append(" == ");
                    break;
                case ExpressionType.OrElse:
                    this.kqlAccumulator.Append(" or ");
                    break;
                case ExpressionType.AndAlso:
                    this.kqlAccumulator.Append(" and ");
                    break;
            }

            this.Visit(node.Right);

            return node;
        }

        //protected override Expression VisitBlock(BlockExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override CatchBlock VisitCatchBlock(CatchBlock node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitConditional(ConditionalExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        protected override Expression VisitConstant(ConstantExpression node)
        {

            if (node.Type == typeof(string))
            {
                this.kqlAccumulator.Append($"\"{node.Value.ToString()}\"");
            }

            return node;
        }

        //protected override Expression VisitDebugInfo(DebugInfoExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitDefault(DefaultExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitDynamic(DynamicExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override ElementInit VisitElementInit(ElementInit node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitExtension(Expression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitGoto(GotoExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitIndex(IndexExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitInvocation(InvocationExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitLabel(LabelExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //[return: NotNullIfNotNull("node")]
        //protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitLambda<T>(Expression<T> node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitListInit(ListInitExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitLoop(LoopExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        protected override Expression VisitMember(MemberExpression node)
        {
            this.kqlAccumulator.Append(node.Member.Name);

            return node;
        }

        //protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override MemberBinding VisitMemberBinding(MemberBinding node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitMemberInit(MemberInitExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        //{
        //    throw new NotImplementedException();
        //}

        ////protected override Expression VisitMethodCall(MethodCallExpression node)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //protected override Expression VisitNew(NewExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitNewArray(NewArrayExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitParameter(ParameterExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitSwitch(SwitchExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override SwitchCase VisitSwitchCase(SwitchCase node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitTry(TryExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override Expression VisitUnary(UnaryExpression node)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
