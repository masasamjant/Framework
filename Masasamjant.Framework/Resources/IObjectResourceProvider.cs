using System.Globalization;

namespace Masasamjant.Resources
{
    /// <summary>
    /// Represents a provider of object resources.
    /// </summary>
    public interface IObjectResourceProvider
    {
        /// <summary>
        /// Gets object resource specified by key using current UI culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The object resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> is <c>null</c>.</exception>
        object? GetObject(string resourceKey, object? defaultValue = null);

        /// <summary>
        /// Gets object resource specified by key using specified culture.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <param name="culture">The culture to use.</param>
        /// <param name="defaultValue">The default value to return if the resource is not found.</param>
        /// <returns>The object resource.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="resourceKey"/> or <paramref name="culture"/> is <c>null</c>.</exception>
        object? GetObject(string resourceKey, CultureInfo culture, object? defaultValue = null);
    }
}
