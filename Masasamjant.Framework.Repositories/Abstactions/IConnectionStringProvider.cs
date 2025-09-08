namespace Masasamjant.Repositories.Abstactions
{
    /// <summary>
    /// Represents component that provides connection string to data source.
    /// </summary>
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns>A connection string.</returns>
        /// <exception cref="InvalidOperationException">If connection string cannot be provided.</exception>
        string GetConnectionString();
    }
}
