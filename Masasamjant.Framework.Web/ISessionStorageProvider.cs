namespace Masasamjant.Web
{
    /// <summary>
    /// Represents a component that provides an implementation of <see cref="ISessionStorage"/>.
    /// </summary>
    public interface ISessionStorageProvider
    {
        /// <summary>
        /// Gets the <see cref="ISessionStorage"/> implementation.
        /// </summary>
        /// <returns>The <see cref="ISessionStorage"/> implementation.</returns>
        /// <exception cref="InvalidOperationException">If the <see cref="ISessionStorage"/> implementation cannot be retrieved.</exception>
        ISessionStorage GetSessionStorage();
    }
}
