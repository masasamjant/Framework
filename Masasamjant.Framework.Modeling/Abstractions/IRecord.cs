namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model in application that implements <see cref="ISupportCreated"/>, <see cref="ISupportModified"/> and <see cref="ISupportDeleted"/> interfaces.
    /// </summary>
    public interface IRecord : IModel, ISupportCreated, ISupportModified, ISupportDeleted
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
