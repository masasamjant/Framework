namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents entity that implements <see cref="ISupportCreated"/> and <see cref="ISupportModified"/> interfaces.
    /// </summary>
    public interface IRecord : IModel, ISupportCreated, ISupportModified
    {
    }
}
