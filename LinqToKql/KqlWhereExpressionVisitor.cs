using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToKql
{
    internal class KqlWhereExpressionVisitor : BaseExpressionVisitor
    {
        private const char DefaultQuote = '\'';
        private ExpressionType? Modifier;

        protected override Expression VisitBinary(BinaryExpression node)
        {
            this.Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    kqlAccumulator.Append(" == ");
                    break;
                case ExpressionType.NotEqual:
                    kqlAccumulator.Append(" != ");
                    break;
                case ExpressionType.OrElse:
                    kqlAccumulator.Append(" or ");
                    break;
                case ExpressionType.AndAlso:
                    kqlAccumulator.Append(" and ");
                    break;
            }

            this.Visit(node.Right);

            return node;
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            throw new NotImplementedException();
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type == typeof(string))
            {
                this.kqlAccumulator.Append($"{DefaultQuote}{node.Value.ToString()}{DefaultQuote}");
            }

            return node;

            //return base.VisitConstant(node);
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitDefault(DefaultExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            throw new NotImplementedException();
        }

        protected override ElementInit VisitElementInit(ElementInit node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitExtension(Expression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            throw new NotImplementedException();
        }

        //[return: NotNullIfNotNull("node")]
        //protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
        //{
        //    return base.VisitLabelTarget(node);
        //}

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (node.Body.NodeType == ExpressionType.Not)
            {
                Modifier = ExpressionType.Not;
                Visit(((UnaryExpression)node.Body).Operand);
            }
            else
            {
                Visit(node.Body);
            }

            return node;
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            this.kqlAccumulator.Append(node.Member.Name);
            return node;
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            throw new NotImplementedException();
        }

        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            throw new NotImplementedException();
        }

        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            throw new NotImplementedException();
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            switch (node.Method.Name)
            {
                case "Equals":
                    Visit(node.Object as MemberExpression);

                    // in equals, second parameter is the StringComparison
                    if (node.Arguments.Count == 2)
                    {
                        var compare = (StringComparison)(node.Arguments.Last() as ConstantExpression).Value;
                        if (new StringComparison[] { 
                            StringComparison.InvariantCultureIgnoreCase, 
                            StringComparison.InvariantCultureIgnoreCase, 
                            StringComparison.OrdinalIgnoreCase }.Contains(compare))
                        {
                            if (Modifier.HasValue && Modifier.Value == ExpressionType.Not)
                            {
                                kqlAccumulator.Append(" !~ ");
                            }
                            else
                            {
                                kqlAccumulator.Append(" =~ ");
                            }
                        }
                        else
                        {
                            if (Modifier.HasValue && Modifier.Value == ExpressionType.Not)
                            {
                                kqlAccumulator.Append(" != ");
                            }
                            else
                            {
                                kqlAccumulator.Append(" == ");
                            }
                        }
                    }
                    else
                    {
                        if (Modifier.HasValue && Modifier.Value == ExpressionType.Not)
                        {
                            kqlAccumulator.Append(" != ");
                            Modifier = null;
                        }
                        else
                        {
                            kqlAccumulator.Append(" == ");
                        }

                        
                    }

                    Visit(node.Arguments.First());
                    break;
            }

            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            throw new NotImplementedException();
        }

        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitTry(TryExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            throw new NotImplementedException();
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            Visit(node.Operand);
            return node;
        }
    }
}
