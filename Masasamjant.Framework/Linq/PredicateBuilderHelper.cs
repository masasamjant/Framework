namespace Masasamjant.Linq
{
    /// <summary>
    /// Provides helper methods to <see cref="IPredicateBuilder{T}"/> interface.
    /// </summary>
    public static class PredicateBuilderHelper
    {
        /// <summary>
        /// Builds and then compiles expression build by specified <see cref="IPredicateBuilder{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the target object.</typeparam>
        /// <param name="predicateBuilder">The <see cref="IPredicateBuilder{T}"/>.</param>
        /// <returns>A <see cref="Func{T, bool}"/> delegate compiled from expression build by <paramref name="predicateBuilder"/>.</returns>
        public static Func<T, bool> BuildAndCompile<T>(this IPredicateBuilder<T> predicateBuilder)
            => predicateBuilder.Build().Compile();

        /// <summary>
        /// Check if predicate builder condition represents AND condition.
        /// </summary>
        /// <param name="condition">The predicate builder condition.</param>
        /// <returns><c>true</c> if logical operator of <paramref name="condition"/> is <see cref="LogicalOperator.And"/>; <c>false</c> otherwise.</returns>
        public static bool IsAndCondition(this PredicateBuilderCondition condition)
            => condition.LogicalOperator == LogicalOperator.And;

        /// <summary>
        /// Check if predicate builder condition represents OR condition.
        /// </summary>
        /// <param name="condition">The predicate builder condition.</param>
        /// <returns><c>true</c> if logical operator of <paramref name="condition"/> is <see cref="LogicalOperator.Or"/>; <c>false</c> otherwise.</returns>
        public static bool IsOrCondition(this PredicateBuilderCondition condition)
            => condition.LogicalOperator == LogicalOperator.Or;

        /// <summary>
        /// Check if predicate builder condition represents no condition.
        /// </summary>
        /// <param name="condition">The predicate builder condition.</param>
        /// <returns><c>true</c> if logical operator of <paramref name="condition"/> is <see cref="LogicalOperator.None"/>; <c>false</c> otherwise.</returns>
        public static bool IsNoneCondition(this PredicateBuilderCondition condition)
            => condition.LogicalOperator == LogicalOperator.None;
    }
}
