namespace Masasamjant
{
    /// <summary>
    /// Represents <see cref="IDisposable"/> that has <see cref="IsDisposed"/> property.
    /// </summary>
    public interface ISupportIsDisposed : IDisposable
    {
        /// <summary>
        /// Gets whether or not instance is disposed.
        /// </summary>
        bool IsDisposed { get; }
    }
}
