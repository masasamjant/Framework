using System.Linq.Expressions;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Represents component that builds predicate expression from specified conditions.
    /// </summary>
    /// <typeparam name="T">The type of the target object.</typeparam>
    public sealed class PredicateBuilder<T> : IPredicateBuilder<T>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PredicateBuilder{T}"/> class without any conditions.
        /// </summary>
        public PredicateBuilder() 
        {
            Conditions = new PredicateBuilderConditionCollection();
        }

        /// <summary>
        /// Initializes new instance of the <see cref="PredicateBuilder{T}"/> class with specified conditions.
        /// </summary>
        /// <param name="conditions"></param>
        public PredicateBuilder(IEnumerable<PredicateBuilderCondition> conditions)
        {
            Conditions = new PredicateBuilderConditionCollection(conditions);
        }

        /// <summary>
        /// Gets the conditions.
        /// </summary>
        public PredicateBuilderConditionCollection Conditions { get; }

        /// <summary>
        /// Builds predicate expression from specified conditions.
        /// </summary>
        /// <returns>A predicate expression.</returns>
        /// <exception cref="InvalidOperationException">If building expression fails.</exception>
        /// <exception cref="NotSupportedException">If conditions contains operators or members that are not supported.</exception>
        public Expression<Func<T, bool>> Build()
        {
            if (Conditions.Count == 0)
                throw new InvalidOperationException("At least one condition must be added.");

            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var currentCondition = Conditions.First();
            ValidateCondition(currentCondition);
            var currentExpression = CreateExpression(parameterExpression, currentCondition);

            foreach (var nextCondition in Conditions.Skip(1))
            {
                // Current condition should not be compared with following conditions. Skipt rest.
                if (currentCondition.LogicalOperator == LogicalOperator.None)
                    break;

                ValidateCondition(nextCondition);

                var nextExpression = CreateExpression(parameterExpression, nextCondition);

                if (currentCondition.LogicalOperator == LogicalOperator.And)
                    currentExpression = Expression.AndAlso(currentExpression, nextExpression);
                else
                    currentExpression = Expression.OrElse(currentExpression, nextExpression);

                currentCondition = nextCondition;
            }

            return Expression.Lambda<Func<T, bool>>(currentExpression, parameterExpression);
        }

        private static void ValidateCondition(PredicateBuilderCondition condition)
        {
            if (condition.EqualityOperator == EqualityOperator.Contains ||
                condition.EqualityOperator == EqualityOperator.StartsWith ||
                condition.EqualityOperator == EqualityOperator.EndsWith)
            {
                if (!condition.MemberType.Equals(typeof(string)))
                    throw new NotSupportedException($"Equality operator '{condition.EqualityOperator}' is supported only with {typeof(string)}.");

                if (condition.MemberValue == null)
                    throw new InvalidOperationException($"Member value must be not null when equality operator is '{condition.EqualityOperator}'.");
            }
        }

        private static BinaryExpression CreateExpression(ParameterExpression parameterExpression, PredicateBuilderCondition condition)
        {
            if (condition.MemberName.Contains('.'))
            {
                var propertyNames = condition.MemberName.Split('.');
                return CreateExpression(parameterExpression, propertyNames, condition);
            }
            else
            {
                var member = typeof(T).GetMember(condition.MemberName).FirstOrDefault();

                if (member == null)
                    throw new InvalidOperationException($"The type '{typeof(T)}' has not member of '{condition.MemberName}'.");

                var memberExpression = Expression.MakeMemberAccess(parameterExpression, member);
                return CreateEqualityExpression(memberExpression, condition.EqualityOperator, condition.MemberValue);
            }
        }

        private static BinaryExpression CreateExpression(ParameterExpression paramterExpression, IEnumerable<string> propertyNames, PredicateBuilderCondition condition)
        {
            Expression memberExpression = paramterExpression;

            foreach (var propertyName in propertyNames)
            {
                try
                {
                    memberExpression = Expression.PropertyOrField(memberExpression, propertyName);
                }
                catch (ArgumentException exception)
                {
                    throw new InvalidOperationException(exception.Message, exception);
                }
            }

            return CreateEqualityExpression((MemberExpression)memberExpression, condition.EqualityOperator, condition.MemberValue);
        }

        private static BinaryExpression CreateEqualityExpression(MemberExpression memberExpression, EqualityOperator equalityOperator, object? value)
        {
            BinaryExpression equalityExpression;
            var constantExpression = Expression.Constant(value);

            switch (equalityOperator)
            {
                case EqualityOperator.Equal:
                    equalityExpression = Expression.Equal(memberExpression, constantExpression);
                    break;
                case EqualityOperator.NotEqual:
                    equalityExpression = Expression.NotEqual(memberExpression, constantExpression);
                    break;
                case EqualityOperator.GreaterThan:
                    equalityExpression = Expression.GreaterThan(memberExpression, constantExpression);
                    break;
                case EqualityOperator.GreaterThanOrEqual:
                    equalityExpression = Expression.GreaterThanOrEqual(memberExpression, constantExpression);
                    break;
                case EqualityOperator.LessThan:
                    equalityExpression = Expression.LessThan(memberExpression, constantExpression);
                    break;
                case EqualityOperator.LessThanOrEqual:
                    equalityExpression = Expression.LessThanOrEqual(memberExpression, constantExpression);
                    break;
                case EqualityOperator.Contains:
                    equalityExpression = CreateStringMethodExpression(memberExpression, value as string, nameof(string.Contains));
                    break;
                case EqualityOperator.StartsWith:
                    equalityExpression = CreateStringMethodExpression(memberExpression, value as string, nameof(string.StartsWith));
                    break;
                case EqualityOperator.EndsWith:
                    equalityExpression = CreateStringMethodExpression(memberExpression, value as string, nameof(string.EndsWith));
                    break;
                default:
                    throw new NotSupportedException($"The equality operator '{equalityOperator}' is not supported.");
            }

            return equalityExpression;
        }

        private static BinaryExpression CreateStringMethodExpression(MemberExpression memberExpression, string? value, string methodName)
        {
            var valueExpression = Expression.Constant(value, typeof(string));
            var trueExpression = Expression.Constant(true, typeof(bool));
            var method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
            if (method == null)
                throw new InvalidOperationException($"The expected '{methodName}' -method not exist on {typeof(string)}");
            var methodCallExpression = Expression.Call(memberExpression, method, valueExpression);
            return Expression.MakeBinary(ExpressionType.Equal, trueExpression, methodCallExpression);
        }
    }
}
