namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents entity that implements <see cref="ISupportCreated"/> and <see cref="ISupportModified"/> interfaces.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public interface IRecord<TIdentifier> : IEntity<TIdentifier>, ISupportCreated, ISupportModified
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
    }
}
