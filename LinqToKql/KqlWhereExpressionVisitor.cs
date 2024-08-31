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

            return result.ToString();
        }

        public T Evaluate<T>(Expression expression)
        {
            return (T)Expression.Lambda(expression).Compile().DynamicInvoke();
        }

        #endregion
    }
}
