namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model that is identified by <typeparamref name="TIdentifier"/>.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public interface IEntity<TIdentifier> : IModel where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        TIdentifier Identifier { get; }
    }
}
