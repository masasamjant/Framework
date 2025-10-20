namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents provider that computes hash from string.
    /// </summary>
    public interface IStringHashProvider
    {
        /// <summary>
        /// Gets the name of algorithm.
        /// </summary>
        string Algorithm { get; }

        /// <summary>
        /// Create hash from specified string value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A hash string or empty, if value of <paramref name="value"/> is empty.</returns>
        string CreateHash(string value);

        /// <summary>
        /// Create hash from specified string value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A hash string or empty, if value of <paramref name="value"/> is empty.</returns>
        Task<string> CreateHashAsync(string value);
    }
}
