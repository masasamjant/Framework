namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents <see cref="IModel"/> that implements <see cref="ISupportCreated"/>, <see cref="ISupportModified"/> and <see cref="ISupportDeleted"/> interfaces.
    /// </summary>
    public interface IRecord : IModel, ISupportCreated, ISupportModified, ISupportDeleted
    {
    }
}
