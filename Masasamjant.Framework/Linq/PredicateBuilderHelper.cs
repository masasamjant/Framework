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
    }
}
