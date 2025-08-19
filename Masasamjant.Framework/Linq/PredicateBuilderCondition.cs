namespace Masasamjant.Linq
{
    /// <summary>
    /// Represents condition of the <see cref="PredicateBuilder"/>.
    /// </summary>
    public sealed class PredicateBuilderCondition : IEquatable<PredicateBuilderCondition>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PredicateBuilderCondition"/> class.
        /// </summary>
        /// <param name="memberName">The name of condition member.</param>
        /// <param name="memberType">The type of the condition member.</param>
        /// <param name="memberValue">The condition value to compare to.</param>
        /// <param name="equalityOperator">The logical operator applied between conditions.</param>
        /// <param name="logicalOperator">The condition comparison operator.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="memberName"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="memberValue"/> is not <c>null</c> and its type is not <paramref name="memberType"/>.</exception>
        public PredicateBuilderCondition(string memberName, Type memberType, object? memberValue, EqualityOperator equalityOperator, LogicalOperator logicalOperator)
        {
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName), "The member name cannot be empty or only whitespace.");

            if (memberValue != null && !memberType.Equals(memberValue.GetType()))
                throw new ArgumentException("The type of member value must be equal to member type.", nameof(memberValue));

            EqualityOperator = Enum.IsDefined(equalityOperator) ? equalityOperator : EqualityOperator.Equal;
            LogicalOperator = Enum.IsDefined(logicalOperator) ? logicalOperator : LogicalOperator.None;
            MemberName = memberName;
            MemberType = memberType;
            MemberValue = memberValue;
        }

        /// <summary>
        /// Gets the logical operator applied between conditions.
        /// </summary>
        public LogicalOperator LogicalOperator { get; }

        /// <summary>
        /// Gets the name of condition member.
        /// </summary>
        public string MemberName { get; }

        /// <summary>
        /// Gets the condition value to compare to.
        /// </summary>
        public object? MemberValue { get; }

        /// <summary>
        /// Gets the type of the condition member.
        /// </summary>
        public Type MemberType { get; }

        /// <summary>
        /// Gets the condition comparison operator.
        /// </summary>
        public EqualityOperator EqualityOperator { get; }

        /// <summary>
        /// Check if other <see cref="PredicateBuilderCondition"/> is equal to this.
        /// </summary>
        /// <param name="other">The other <see cref="PredicateBuilderCondition"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(PredicateBuilderCondition? other) 
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other)) 
                return true;

            return LogicalOperator == other.LogicalOperator &&
                EqualityOperator == other.EqualityOperator &&
                MemberType.Equals(other.MemberType) &&
                MemberName == other.MemberName &&
                Equals(MemberValue, other.MemberValue);
        }

        /// <summary>
        /// Check if object instance is <see cref="PredicateBuilderCondition"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="PredicateBuilderCondition"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as PredicateBuilderCondition);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            int code = HashCode.Combine(LogicalOperator, EqualityOperator, MemberType, MemberName);
            if (MemberValue != null)
                code ^= MemberValue.GetHashCode();
            return code;
        }
    }
}
