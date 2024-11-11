namespace Masasamjant.Repositories
{
    /// <summary>
    /// Represents provider of connection string.
    /// </summary>
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns>A connection string.</returns>
        string GetConnectionString();
    }
}
