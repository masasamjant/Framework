using System.Globalization;

namespace Masasamjant.Resources
{
    /// <summary>
    /// Represents a provider of stream resources.
    /// </summary>
    public interface IStreamResourceProvider
    {
        /// <summary>
        /// Gets the stream resource for the specified resource name using current UI culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns>The stream resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> is <c>null</c>.</exception>
        Stream? GetStream(string resourceKey);

        /// <summary>
        /// Gets the stream resource for the specified resource name using the specified culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="culture">The culture to use.</param>
        /// <returns>The stream resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> or <paramref name="culture"/> is <c>null</c>.</exception>
        Stream? GetStream(string resourceKey, CultureInfo culture);
    }
}
