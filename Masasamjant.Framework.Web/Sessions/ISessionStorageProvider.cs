namespace Masasamjant.Web.Sessions
{
    /// <summary>
    /// Represents provider of <see cref="ISessionStorage"/>.
    /// </summary>
    public interface ISessionStorageProvider
    {
        /// <summary>
        /// Gets session storage.
        /// </summary>
        /// <returns>A <see cref="ISessionStorage"/>.</returns>
        /// <exception cref="InvalidOperationException">If session storage is not available.</exception>
        ISessionStorage GetSessionStorage();
    }
}
