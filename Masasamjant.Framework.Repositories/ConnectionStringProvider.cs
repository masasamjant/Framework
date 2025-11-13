using Masasamjant.Repositories.Abstractions;

namespace Masasamjant.Repositories
{
    /// <summary>
    /// Represents component that provides connection string to data source.
    /// </summary>
    public sealed class ConnectionStringProvider  : IConnectionStringProvider
    {
        private readonly Func<string> getConnectionString;

        /// <summary>
        /// Initializes new instance of the <see cref="ConnectionStringProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to provide.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="connectionString"/> is empty or only whitespace.</exception>
        public ConnectionStringProvider(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "The connection string cannot be empty or only whitespace.");

            this.getConnectionString = () => connectionString;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="ConnectionStringProvider"/> class.
        /// </summary>
        /// <param name="getConnectionString">The function delegate to get connection string.</param>
        public ConnectionStringProvider(Func<string> getConnectionString)
        {
            this.getConnectionString = getConnectionString;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="ConnectionStringProvider"/> class.
        /// </summary>
        /// <param name="connectionStringProvider">The <see cref="IConnectionStringProvider"/> to get connection string.</param>
        public ConnectionStringProvider(IConnectionStringProvider connectionStringProvider)
        {
            this.getConnectionString = connectionStringProvider.GetConnectionString;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns>A connection string.</returns>
        /// <exception cref="InvalidOperationException">If connection string cannot be provided.</exception>
        public string GetConnectionString() 
        {
            string connectionString = string.Empty;

            try
            {
                connectionString = getConnectionString();
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Exception occurred while getting connection string.", exception);
            }

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("The connection string cannot be empty or only whitespace.");

            return connectionString;
        }
    }

}
