using System.Linq.Expressions;

namespace LinqToKql
{
    internal class KqlWhereExpressionVisitor : BaseExpressionVisitor
    {
        private const char DefaultQuote = '\'';
        private readonly Type[] TypesRequireQuotes = [typeof(string), typeof(Guid)];
        private readonly IList<Type> ListTypesRequireQuotes;
        private readonly StringComparison[] CaseInsensitiveStringCompares = [
                            StringComparison.InvariantCultureIgnoreCase,
                            StringComparison.CurrentCultureIgnoreCase,
                            StringComparison.OrdinalIgnoreCase ];
        private readonly StringComparer[] CaseInsensitiveStringComparers;

        private const string PropertyExpressionTypeName = "PropertyExpression";

        private ExpressionType? Modifier;

        public KqlWhereExpressionVisitor()
        {
            ListTypesRequireQuotes = TypesRequireQuotes.Select(type => new [] {
                typeof(IEnumerable<>).MakeGenericType(type),
                type.MakeArrayType(),
                typeof(List<>).MakeGenericType(type)
            }).SelectMany(x => x).ToList();

            CaseInsensitiveStringComparers = CaseInsensitiveStringCompares.Select(x => StringComparer.FromComparison(x)).ToArray();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (TypeEquals(node.Left.GetType(), name: PropertyExpressionTypeName))
            {
                Visit(node.Left);
            }
            else
            {
                kqlAccumulator.Append(ConvertToQueryValue(node.Left));
            }

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
                case ExpressionType.LessThan:
                    kqlAccumulator.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    kqlAccumulator.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    kqlAccumulator.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    kqlAccumulator.Append(" >= ");
                    break;
                case ExpressionType.Add:
                    throw new NotImplementedException();
                case ExpressionType.AddChecked:
                    throw new NotImplementedException();
                case ExpressionType.And:
                    throw new NotImplementedException();
                case ExpressionType.ArrayLength:
                    throw new NotImplementedException();
                case ExpressionType.ArrayIndex:
                    throw new NotImplementedException();
                case ExpressionType.Call:
                    throw new NotImplementedException();
                case ExpressionType.Coalesce:
                    throw new NotImplementedException();
                case ExpressionType.Conditional:
                    throw new NotImplementedException();
                case ExpressionType.Constant:
                    throw new NotImplementedException();
                case ExpressionType.Convert:
                    throw new NotImplementedException();
                case ExpressionType.ConvertChecked:
                    throw new NotImplementedException();
                case ExpressionType.Divide:
                    throw new NotImplementedException();
                case ExpressionType.ExclusiveOr:
                    throw new NotImplementedException();
                case ExpressionType.Invoke:
                    throw new NotImplementedException();
                case ExpressionType.Lambda:
                    throw new NotImplementedException();
                case ExpressionType.LeftShift:
                    throw new NotImplementedException();
                case ExpressionType.ListInit:
                    throw new NotImplementedException();
                case ExpressionType.MemberAccess:
                    throw new NotImplementedException();
                case ExpressionType.MemberInit:
                    throw new NotImplementedException();
                case ExpressionType.Modulo:
                    throw new NotImplementedException();
                case ExpressionType.Multiply:
                    throw new NotImplementedException();
                case ExpressionType.MultiplyChecked:
                    throw new NotImplementedException();
                case ExpressionType.Negate:
                    throw new NotImplementedException();
                case ExpressionType.UnaryPlus:
                    throw new NotImplementedException();
                case ExpressionType.NegateChecked:
                    throw new NotImplementedException();
                case ExpressionType.New:
                    throw new NotImplementedException();
                case ExpressionType.NewArrayInit:
                    throw new NotImplementedException();
                case ExpressionType.NewArrayBounds:
                    throw new NotImplementedException();
                case ExpressionType.Not:
                    throw new NotImplementedException();
                case ExpressionType.Or:
                    throw new NotImplementedException();
                case ExpressionType.Parameter:
                    throw new NotImplementedException();
                case ExpressionType.Power:
                    throw new NotImplementedException();
                case ExpressionType.Quote:
                    throw new NotImplementedException();
                case ExpressionType.RightShift:
                    throw new NotImplementedException();
                case ExpressionType.Subtract:
                    throw new NotImplementedException();
                case ExpressionType.SubtractChecked:
                    throw new NotImplementedException();
                case ExpressionType.TypeAs:
                    throw new NotImplementedException();
                case ExpressionType.TypeIs:
                    throw new NotImplementedException();
                case ExpressionType.Assign:
                    throw new NotImplementedException();
                case ExpressionType.Block:
                    throw new NotImplementedException();
                case ExpressionType.DebugInfo:
                    throw new NotImplementedException();
                case ExpressionType.Decrement:
                    throw new NotImplementedException();
                case ExpressionType.Dynamic:
                    throw new NotImplementedException();
                case ExpressionType.Default:
                    throw new NotImplementedException();
                case ExpressionType.Extension:
                    throw new NotImplementedException();
                case ExpressionType.Goto:
                    throw new NotImplementedException();
                case ExpressionType.Increment:
                    throw new NotImplementedException();
                case ExpressionType.Index:
                    throw new NotImplementedException();
                case ExpressionType.Label:
                    throw new NotImplementedException();
                case ExpressionType.RuntimeVariables:
                    throw new NotImplementedException();
                case ExpressionType.Loop:
                    throw new NotImplementedException();
                case ExpressionType.Switch:
                    throw new NotImplementedException();
                case ExpressionType.Throw:
                    throw new NotImplementedException();
                case ExpressionType.Try:
                    throw new NotImplementedException();
                case ExpressionType.Unbox:
                    throw new NotImplementedException();
                case ExpressionType.AddAssign:
                    throw new NotImplementedException();
                case ExpressionType.AndAssign:
                    throw new NotImplementedException();
                case ExpressionType.DivideAssign:
                    throw new NotImplementedException();
                case ExpressionType.ExclusiveOrAssign:
                    throw new NotImplementedException();
                case ExpressionType.LeftShiftAssign:
                    throw new NotImplementedException();
                case ExpressionType.ModuloAssign:
                    throw new NotImplementedException();
                case ExpressionType.MultiplyAssign:
                    throw new NotImplementedException();
                case ExpressionType.OrAssign:
                    throw new NotImplementedException();
                case ExpressionType.PowerAssign:
                    throw new NotImplementedException();
                case ExpressionType.RightShiftAssign:
                    throw new NotImplementedException();
                case ExpressionType.SubtractAssign:
                    throw new NotImplementedException();
                case ExpressionType.AddAssignChecked:
                    throw new NotImplementedException();
                case ExpressionType.MultiplyAssignChecked:
                    throw new NotImplementedException();
                case ExpressionType.SubtractAssignChecked:
                    throw new NotImplementedException();
                case ExpressionType.PreIncrementAssign:
                    throw new NotImplementedException();
                case ExpressionType.PreDecrementAssign:
                    throw new NotImplementedException();
                case ExpressionType.PostIncrementAssign:
                    throw new NotImplementedException();
                case ExpressionType.PostDecrementAssign:
                    throw new NotImplementedException();
                case ExpressionType.TypeEqual:
                    throw new NotImplementedException();
                case ExpressionType.OnesComplement:
                    throw new NotImplementedException();
                case ExpressionType.IsTrue:
                    throw new NotImplementedException();
                case ExpressionType.IsFalse:
                    throw new NotImplementedException();
            }

            if (TypeEquals(node.Right.GetType(), name: PropertyExpressionTypeName))
            {
                Visit(node.Right);
            }
            else
            {
                kqlAccumulator.Append(ConvertToQueryValue(node.Right));
            }

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
            kqlAccumulator.Append(ConvertToQueryValue(node.Value, node.Type));

            return node;
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
            if (node.Type == typeof(DateTime) && node.Member.Name == "Now")
            {
                kqlAccumulator.Append("now()");
            }
            else
            {
                kqlAccumulator.Append(node.Member.Name);
            }
            
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
                case "Contains":
                    // when node.Object is null, it is a static method (e.g. Enumerable.Contains(source, item))
                    // otherwise it is an instance contains
                    var list = node.Object != null ? node.Object : node.Arguments[0];
                    var operand = node.Object != null ? node.Arguments[0] : node.Arguments[1];
                    var containsCompare = node.Arguments.Count == 3 ? Evaluate<StringComparer>(node.Arguments.Last()) : default(StringComparer);

                    Visit(operand);
                    var eq = CaseInsensitiveStringComparers.Contains(containsCompare) ? "in~" : "in";
                    var ne = CaseInsensitiveStringComparers.Contains(containsCompare) ? "!in~" : "!in";
                    ApplyNotModifier($" {eq} (", $" {ne} (");
                    EnumerateListValues(list);
                    kqlAccumulator.Append(")");

                    break;
                case "Equals":
                    // static class comparison
                    if (node.Object == null)
                    {
                        Visit(node.Arguments[0]);
                        var eqStaticCompare = node.Arguments.Count == 3 ? Evaluate<StringComparison>(node.Arguments.Last()) : default(StringComparison);
                        var equalsStaticNe = CaseInsensitiveStringCompares.Contains(eqStaticCompare) ? "!~" : "!=";
                        var equalsStaticEq = CaseInsensitiveStringCompares.Contains(eqStaticCompare) ? "=~" : "==";
                        ApplyNotModifier($" {equalsStaticEq} ", $" {equalsStaticNe} ");
                        Visit(node.Arguments[1]);
                    }
                    // instnace comparison
                    else
                    {
                        Visit(node.Object as MemberExpression);

                        var eqCompare = node.Arguments.Count == 2 ? Evaluate<StringComparison>(node.Arguments.Last()) : default(StringComparison);
                        var equalsEq = CaseInsensitiveStringCompares.Contains(eqCompare) ? "=~" : "==";
                        var equalsNe = CaseInsensitiveStringCompares.Contains(eqCompare) ? "!~" : "!=";
                        ApplyNotModifier($" {equalsEq} ", $" {equalsNe} ");

                        Visit(node.Arguments.First());
                    }
                    break;
                case "Parse":
                    kqlAccumulator.Append(ConvertToQueryValue(node));
                    break;
            }

            return node;
        }

        /// <summary>
        /// Enumerates the values of an array
        /// </summary>
        /// <param name="expr"></param>
        /// <exception cref="NotSupportedException"></exception>
        private void EnumerateListValues(Expression expr)
        {
            var compiledList = Evaluate<System.Collections.IEnumerable>(expr);

            var values = compiledList.Cast<string>().ToList();

            var results = string.Join(", ", values.Select(x => ConvertToQueryValue(x, expr.Type) ));

            kqlAccumulator.Append(results);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            kqlAccumulator.Append(ConvertToQueryValue(node));

            return node;
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

        #region Helper Methods

        public void ApplyNotModifier(string ifNoModifier, string ifHasModifier)
        {
            kqlAccumulator.Append(
                Modifier.HasValue && Modifier.Value == ExpressionType.Not ?
                    ifHasModifier :
                    ifNoModifier
                );

            Modifier = null;
        }

        private string ConvertToQueryValue(Expression expression)
        {
            var value = EvaluateToString(expression);
            return ConvertToQueryValue(value, expression.Type);
        }

        private string ConvertToQueryValue(object value, Type type, char? quoteChar = null)
        {
            var requiresQuotes = TypesRequireQuotes.Contains(type) || ListTypesRequireQuotes.Contains(type);
            var quote = quoteChar ?? DefaultQuote;

            return requiresQuotes ?
                $"{quote}{value.ToString()}{quote}" :
                $"{value.ToString()}";
        }

        private bool TypeEquals(Type srcType, Type? type = null, string? name = null)
        {
            if (type == null && string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("type and name parameter cannot both be null");
            }

            if (type != null)
            {
                return srcType == type;
            }
    
            return srcType.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        public string EvaluateToString(Expression expression)
        {
            var baseMethod = typeof(KqlWhereExpressionVisitor).GetMethod(nameof(Evaluate));
            var genericMethod = baseMethod.MakeGenericMethod(expression.Type);
            var result = genericMethod.Invoke(this, new object[] { expression });

            if (expression.Type == typeof(DateTime))
            {
                var dt = (DateTime)result;
                if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0)
                {

                    return $"datetime({dt.ToString("yyyy-MM-dd")})";
                }

                return $"datetime({dt.ToString("yyyy-MM-dd HH:mm:ss")})";
            }

            return result.ToString();
        }

        public T Evaluate<T>(Expression expression)
        {
            return (T)Expression.Lambda(expression).Compile().DynamicInvoke();
        }

        #endregion
    }
}
