using Masasamjant.Collections;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Represents collection of <see cref="PredicateBuilderCondition"/> conditions.
    /// </summary>
    public sealed class PredicateBuilderConditionCollection : Collection<PredicateBuilderCondition>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PredicateBuilderConditionCollection"/> class.
        /// </summary>
        public PredicateBuilderConditionCollection()
            : base()
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="PredicateBuilderConditionCollection"/> class.
        /// </summary>
        /// <param name="conditions">The initial conditions.</param>
        public PredicateBuilderConditionCollection(IEnumerable<PredicateBuilderCondition> conditions)
            : this()
        {
            AddRange(conditions);
        }

        /// <summary>
        /// Add specified <see cref="PredicateBuilderCondition"/> to collection. If already contains <paramref name="condition"/>, 
        /// then condition is not added.
        /// </summary>
        /// <param name="condition">The condition to add.</param>
        public override void Add(PredicateBuilderCondition condition)
        {
            if (!Contains(condition))
                base.Add(condition);
        }

        /// <summary>
        /// Add specified <see cref="PredicateBuilderCondition"/>s to collection. Only adds those conditions from <paramref name="conditions"/> 
        /// that are not already in collection.
        /// </summary>
        /// <param name="conditions">The conditions to add.</param>
        public void AddRange(IEnumerable<PredicateBuilderCondition> conditions)
        {
            foreach (var condition in conditions)
                Add(condition);
        }

        /// <summary>
        /// Check if collection contains specified <see cref="PredicateBuilderCondition"/>.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <returns><c>true</c> if contains <paramref name="condition"/>; <c>false</c> otherwise.</returns>
        public override bool Contains(PredicateBuilderCondition condition)
        {
            return base.Contains(condition);
        }

        /// <summary>
        /// Remove specified <see cref="PredicateBuilderCondition"/> from collection.
        /// </summary>
        /// <param name="condition">The condition to remove.</param>
        /// <returns><c>true</c> if <paramref name="condition"/> removed; <c>false</c> otherwise.</returns>
        public override bool Remove(PredicateBuilderCondition condition)
        {
            return base.Remove(condition);
        }
    }
}
