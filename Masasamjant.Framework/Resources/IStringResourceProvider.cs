using System.Globalization;

namespace Masasamjant.Resources
{
    /// <summary>
    /// Represents a provider of string resources.
    /// </summary>
    public interface IStringResourceProvider
    {
        /// <summary>
        /// Gets string resource specified by key using current UI culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The string resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> is <c>null</c>.</exception>
        string? GetString(string resourceKey, string? defaultValue = null);

        /// <summary>
        /// Gets string resource specified by key using specified culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="culture">The culture to use.</param>
        /// <param name="defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The string resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> or <paramref name="culture"/> is <c>null</c>.</exception>
        string? GetString(string resourceKey, CultureInfo culture, string? defaultValue = null);
    }
}
