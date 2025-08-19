using System.Linq.Expressions;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Represents component that builds predicate expression from specified conditions.
    /// </summary>
    /// <typeparam name="T">The type of the target object.</typeparam>
    public interface IPredicateBuilder<T>
    {
        /// <summary>
        /// Gets the conditions.
        /// </summary>
        PredicateBuilderConditionCollection Conditions { get; }

        /// <summary>
        /// Builds predicate expression from specified conditions.
        /// </summary>
        /// <returns>A predicate expression.</returns>
        /// <exception cref="InvalidOperationException">If building expression fails.</exception>
        /// <exception cref="NotSupportedException">If conditions contains operators or members that are not supported.</exception>
        Expression<Func<T, bool>> Build();
    }
}
