namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model in application that implements <see cref="ISupportCreated"/> and <see cref="ISupportModified"/> interfaces.
    /// </summary>
    public interface IRecord : IModel, ISupportCreated, ISupportModified
    { }

    /// <summary>
    /// Represents <see cref="IRecord"/> that is identified by <typeparamref name="TIdentifier"/>.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public interface IRecord<TIdentifier> : IRecord, IModel<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
    }
}
