using System.Text.Json.Serialization;

namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model in application that is identified by <typeparamref name="TIdentifier"/>.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public abstract class Entity<TIdentifier> : Model, IEntity<TIdentifier> where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        [JsonInclude]
        public TIdentifier Identifier { get; protected set; } = new TIdentifier();

        /// <summary>
        /// Check if object instance is entity with same type and identifier.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is same type of entity with equal identifier; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is Entity<TIdentifier> other)
            {
                return GetType().Equals(other.GetType()) && Identifier.Equals(other.Identifier);
            }

            return false;
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Identifier);
        }
    }
}
