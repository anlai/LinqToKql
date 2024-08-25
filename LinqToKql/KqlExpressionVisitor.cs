using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace LinqToKql
{
    internal class KqlExpressionVisitor : ExpressionVisitor
    {

        private StringBuilder kqlAccumulator;

        private string LastMethodCalled;

        public string Translate(Expression expression)
        {
            kqlAccumulator = new StringBuilder();

            Visit(expression);

            return kqlAccumulator.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {

            if (node.Arguments.Count > 1)
            {
                Visit(node.Arguments[0]);
            }

            var whereVisitor = new KqlWhereExpressionVisitor();
            var orderByVisitor = new KqlOrderByExpressionVisitor();
            
            switch (node.Method.Name)
            {
                case "OrderBy":
                    kqlAccumulator.Append($" | sort {orderByVisitor.Translate(node.Arguments[1])} asc");
                    break;
                case "ThenBy":
                    kqlAccumulator.Append($", {orderByVisitor.Translate(node.Arguments[1])} asc");
                    break;
                case "OrderByDescending":
                    kqlAccumulator.Append($" | sort {orderByVisitor.Translate(node.Arguments[1])} desc");
                    break;
                case "ThenByDescending":
                    kqlAccumulator.Append($", {orderByVisitor.Translate(node.Arguments[1])} desc");
                    break;
                case "Where":
                    kqlAccumulator.Append($" | where {whereVisitor.Translate(node.Arguments[1])}");
                    break;
                default:
                    throw new NotImplementedException($"method call for {node.Method.Name} is not supported");
            }

            LastMethodCalled = node.Method.Name;
            return node;
        }
    }
}
//    internal class KqlExpressionVisitor : ExpressionVisitor
//    {
//        private StringBuilder kqlAccumulator;

//        private const char DefaultQuote = '\'';
//        public string Translate(Expression expression)
//        {
//            this.kqlAccumulator = new StringBuilder();
//            this.Visit(expression);
//            return this.kqlAccumulator.ToString();
//        }

//        //public override bool Equals(object? obj)
//        //{
//        //    return base.Equals(obj);
//        //}

//        //public override int GetHashCode()
//        //{
//        //    return base.GetHashCode();
//        //}

//        //public override string? ToString()
//        //{
//        //    return base.ToString();
//        //}

//        [return: NotNullIfNotNull("node")]
//        public override Expression? Visit(Expression? node)
//        {
//            var method = node as MethodCallExpression;
//            if (method != null)
//            {

//                //switch(method.Method.Name)
//                //{
//                //    case "OrderBy":
//                //        this.kqlAccumulator.Append(" | order ");
//                //        break;
//                //    case "Where":
//                //        this.kqlAccumulator.Append(" | where ");
//                //        break;
//                //}

//                Visit(method.Arguments[0]);
//            }

//            return base.Visit(node);
//        }

//        protected override Expression VisitBinary(BinaryExpression node)
//        {
//            this.Visit(node.Left);

//            switch (node.NodeType)
//            {
//                case ExpressionType.Equal:
//                    this.kqlAccumulator.Append(" == ");
//                    break;
//                case ExpressionType.OrElse:
//                    this.kqlAccumulator.Append(" or ");
//                    break;
//                case ExpressionType.AndAlso:
//                    this.kqlAccumulator.Append(" and ");
//                    break;
//            }

//            this.Visit(node.Right);

//            return node;
//        }

//        //protected override Expression VisitBlock(BlockExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override CatchBlock VisitCatchBlock(CatchBlock node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitConditional(ConditionalExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        protected override Expression VisitConstant(ConstantExpression node)
//        {

//            if (node.Type == typeof(string))
//            {
//                this.kqlAccumulator.Append($"{DefaultQuote}{node.Value.ToString()}{DefaultQuote}");
//            }

//            return node;
//        }

//        //protected override Expression VisitDebugInfo(DebugInfoExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitDefault(DefaultExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitDynamic(DynamicExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override ElementInit VisitElementInit(ElementInit node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitExtension(Expression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitGoto(GotoExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitIndex(IndexExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitInvocation(InvocationExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitLabel(LabelExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //[return: NotNullIfNotNull("node")]
//        //protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        protected override Expression VisitLambda<T>(Expression<T> node)
//        {
//            Visit(node.Body);

//            return node;

//            //throw new NotImplementedException();
//        }

//        //protected override Expression VisitListInit(ListInitExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitLoop(LoopExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        protected override Expression VisitMember(MemberExpression node)
//        {
//            this.kqlAccumulator.Append(node.Member.Name);

//            return node;
//        }

//        //protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override MemberBinding VisitMemberBinding(MemberBinding node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitMemberInit(MemberInitExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        protected override Expression VisitMethodCall(MethodCallExpression node)
//        {
//            switch (node.Method.Name)
//            {
//                case "OrderBy":
//                    this.kqlAccumulator.Append(" | sort ");
//                    Visit(node.Arguments[1]);
//                    break;
//                case "Where":
//                    this.kqlAccumulator.Append(" | where ");

//                    Visit(node.Arguments[1]);

//                    //Visit(node.Arguments[1]);
//                    break;

//                case "Contains":
//                    throw new NotImplementedException();
//                    break;
//                case "Equals":

//                    //var memberExpression = node.Object as MemberExpression;
//                    //Visit(memberExpression);

//                    //Visit(node.Arguments.First());

//                    break;
//                default:
//                    throw new NotImplementedException($"method call for {node.Method.Name} is not supported");
//            }

//            return node;
//        }

//        //protected override Expression VisitNew(NewExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitNewArray(NewArrayExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitParameter(ParameterExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitSwitch(SwitchExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override SwitchCase VisitSwitchCase(SwitchCase node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitTry(TryExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        //protected override Expression VisitTypeBinary(TypeBinaryExpression node)
//        //{
//        //    throw new NotImplementedException();
//        //}

//        protected override Expression VisitUnary(UnaryExpression node)
//        {
//            return Visit(node.Operand);
//        }
//    }
//}
